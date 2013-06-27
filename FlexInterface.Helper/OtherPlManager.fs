module FlexInterface.Helper.OtherPLManager

open System
open System.IO
open FlexInterface.Common
open FlexInterface.Helper.OtherFormater


let private fill data =
    let x1,x2,x3,x4,x5,x6,x7,x8,x9,x10,x11,x12,x13,x14,x15,x16,x17,x18,x19= data
    {
        fromTradeDay = x1;
        toTradeDay= x2;
        voucherType= x3;
        voucherNo = x4 ;
        voucherDate = x5;
        mappingCode = x6;
        CustomerNo = x7;
        CustomerName = x8;
        documentType = x9;
        documentDate = x10;
        documentDueDate = x11;
        currencyCode = x12;
        originAmount = x13;
        exchangeRate = x14;
        equvAmount = x15;
        d_cSign = x16;
        productName = x17;
        businessTypeDesc= x18;
        isIncomeOrExpenseDesc= x19
    }

type OthePLInfo =
    |InterestInfo of string * string * string
    |StorageInfo of string * string * string
    |OtherInfo of string * string * string
   
let isOriginAmountPositive(amount: decimal) = amount >= 0m

let isInterestPL(plType: BusinessTypeEnum) = plType = BusinessTypeEnum.InterestPL
let isStoragePL(plType: BusinessTypeEnum) = plType = BusinessTypeEnum.StoragePL

let getInfo (item : FlexInterface.Common.InterestStorageLevyCommisionPLData) =
    let mutable defaultDueDateStr = generateBlankspace(10)
    match item.Type with
    | BusinessTypeEnum.InterestPL 
    | BusinessTypeEnum.StoragePL ->
        let mutable incomeOrExpenseDesc=""
        let mutable fromMt4ID= ""
        match isOriginAmountPositive(item.OriginAmount) with
        |true ->
            incomeOrExpenseDesc <- "支出"
            fromMt4ID <- item.ToMt4LoginID
            match isInterestPL(item.Type) with
            |true -> defaultDueDateStr <- item.ToTradeDay.ToFormateStr()
            | _  ->()
        |false ->
            incomeOrExpenseDesc <- "收入"
            fromMt4ID <- item.FromMt4LoginID
        match isInterestPL(item.Type) with
        | true ->  InterestInfo(incomeOrExpenseDesc,fromMt4ID,defaultDueDateStr)
        | _  -> StorageInfo(incomeOrExpenseDesc,fromMt4ID,defaultDueDateStr)

    | _ ->
        OtherInfo("收入",item.FromMt4LoginID,defaultDueDateStr)

let getTypeDescText businessType =
    match businessType with
    | BusinessTypeEnum.InterestPL -> "未平倉利息"
    | BusinessTypeEnum.StoragePL -> "未平倉倉租" 
    | _ ->  "交易手續費"
   
let Process(item :FlexInterface.Common.InterestStorageLevyCommisionPLData,index,fs: FileStream) =
    let voucherNo = getPLVouchNo(index)
    let voucherType = "HVHV"
    let documentType= "I"
    let fromAccountNo = ref ""
    let documentDueDateStr = ref (generateBlankspace(10))
    let isIncomeOrExpenseDesc= ref ""
    let  businessTypeDesc = getTypeDescText (item.Type)

    let setValue(desc,fromMt4ID,dueDateStr) =
       isIncomeOrExpenseDesc := desc
       fromAccountNo := fromMt4ID
       documentDueDateStr := dueDateStr

    match getInfo(item) with
    | InterestInfo(desc,fromMt4ID,dueDateStr) ->
        setValue(desc,fromMt4ID,dueDateStr)
    | StorageInfo(desc,fromMt4ID,dueDateStr) -> 
        setValue(desc,fromMt4ID,dueDateStr)
    | OtherInfo(desc,fromMt4ID,dueDateStr)  -> 
        setValue(desc,fromMt4ID,dueDateStr)

    let p1 =(
             item.FromTradeDay,
             item.ToTradeDay,
             voucherType,
             voucherNo,
             item.ToTradeDay,
             !fromAccountNo ,
             item.CustomerCode,
             item.CustomerName,
             documentType,
             item.ToTradeDay,
             !documentDueDateStr ,
             item.CurrencyCode,
             item.OriginAmount,
             item.ExchangeRate,
             item.EquvAmount,
             "D",
             item.ProductName,
             businessTypeDesc,
             !isIncomeOrExpenseDesc
            )
    let line1parameter= fill p1
   
    fromAccountNo := getReverseValue !fromAccountNo (item.FromMt4LoginID,item.ToMt4LoginID)

    documentDueDateStr := getReverseValue !documentDueDateStr (generateBlankspace 10 , item.ToTradeDay.ToFormateStr())

    let line2parameter= { line1parameter with mappingCode = !fromAccountNo; documentDueDate= !documentDueDateStr; d_cSign= "C"}

    let line1 = formatLine line1parameter
    let line2 = formatLine line2parameter

    writeToFile(line1,fs,false)
    writeToFile(line2,fs,true)
        

