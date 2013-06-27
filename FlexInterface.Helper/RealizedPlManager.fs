module FlexInterface.Helper.RealizedPlManager
open FlexInterface.Helper.RealizedPLFormater
open FlexInterface.Common
open System
open System.IO
let private fillParameter data =
    let x1,x2,x3,x4,x5,x6,x7,x8,x9,x10,x11,x12,x13,x14,x15 = data
    {
        fromTradeDay = x1;
        toTradeDay = x2;
        voucherType = x3;
        voucherNo = x4;
        voucherDate = x5;
        mappingCode = x6;
        documentType = x7;
        documentDate = x8;
        currencyCode = x9;
        originAmount = x10;
        exchangeRate = x11;
        equvAmount = x12;
        d_cSign = x13;
        productName = x14;
        monthlyChangeRate = x15
    }
        


let Process(item: RealizedPLData,index, fs: FileStream) =
    let voucherNo = getPLVouchNo(index)
    let voucherType = "HVHV"
    let documentType= "I"
    let mutable debitAccountNo = ""
    match item.OriginAmount >= 0m with
    | true -> debitAccountNo <- item.ToMt4LoginID
    | false -> debitAccountNo <- item.FromMt4LoginID

    let p1 = (
              item.FromTradeDay,
              item.ToTradeDay,
              voucherType,
              voucherNo,
              item.ToTradeDay,
              debitAccountNo,
              documentType,
              item.ToTradeDay,
              item.CurrencyCode,
              item.OriginAmount,
              item.ExchangeRate,
              item.EquvAmount,
              "D",
              item.ProductName,
              item.MonthlyChangeRate
              )
    let line1Parameter = fillParameter(p1)

    debitAccountNo <- getReverseValue debitAccountNo (item.FromMt4LoginID,item.ToMt4LoginID)

    let line2Parameter = {line1Parameter with  mappingCode= debitAccountNo ; d_cSign = "C" }
    let line1 = formatLine(line1Parameter)
    let line2 = formatLine(line2Parameter)
    writeToFile(line1,fs,false)
    writeToFile(line2,fs,true)
