module FlexInterface.Helper.FloatingPlManager

open System
open System.IO
open FlexInterface.Common
open FlexInterface.Helper.FloatingPlFormater


let ProcessHelper(item: FloatingPLData, index, fs: FileStream) =
    let voucherNo = getPLVouchNo(index)
    let voucherType = "HVHV"
    let documentType= "I"
    let  creditAccountNo = ref ""
    let getprice (ask: string) (bid: string) =
        match ask.IsNullOrEmpty() with
        |true ->
            match bid.IsNullOrEmpty() with
            |true -> ""
            |false -> bid
        |false ->
            match bid.IsNullOrEmpty() with
            |true -> ask
            |false -> sprintf "%s,%s" ask bid

    match item.OriginAmount >= 0m with
    | true -> creditAccountNo := item.FromMt4LoginID
    | false -> creditAccountNo := item.ToMt4LoginID

    let getOppositeAccountNo() = getReverseValue !creditAccountNo (item.FromMt4LoginID, item.ToMt4LoginID)

    match item.FromTradeDay = item.ToTradeDay with
    | true -> 
        creditAccountNo := getOppositeAccountNo()
    | false -> ()


    let p1 =
       {
            toTradeDay = item.ToTradeDay;
            voucherType = voucherType;
            voucherNo = voucherNo;
            voucherDate = item.ToTradeDay;
            mappingCode = !creditAccountNo;
            desc3Or4 = "*".ToFormatStr(10,false);
            documentType = documentType;
            documentDate = item.ToTradeDay;
            currencyCode = item.CurrencyCode;
            originAmount = item.OriginAmount;
            exchangeRate = item.ExchangeRate;
            equvAmount = item.EquvAmount;
            d_cSign ="C" ;
            productName = item.ProductName;
            monthlyChangeRate = item.MonthlyChangeRate;
            tradingAmount = item.TradingAmount;
            productPrice = getprice (item.AskPrice) (item.BidPrice)
       }

    
    let p2 = {p1 with  d_cSign = "D" ; mappingCode = getOppositeAccountNo()}

    let line1 = formatLine p1
    let line2= formatLine p2

    writeToFile(line1,fs,false)
    writeToFile(line2,fs,true)


let Process(item: FloatingPLData, index, fs: FileStream) =
    ProcessHelper(item.LastFloatingPLData,index,fs)
    ProcessHelper(item,index,fs)