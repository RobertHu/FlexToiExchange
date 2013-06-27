module private FlexInterface.Helper.RealizedPLFormater

open FlexInterface.Common
open System
open System.Text

type internal realizedParameter  = 
    {
        fromTradeDay: DateTime;
        toTradeDay: DateTime;
        voucherType: string;
        voucherNo : string ;
        voucherDate: DateTime;
        mappingCode: string;
        documentType: string;
        documentDate: DateTime;
        currencyCode: string;
        originAmount: decimal;
        exchangeRate: decimal;
        equvAmount: decimal;
        d_cSign: string;
        productName: string;
        monthlyChangeRate: decimal
    }



let internal formatLine (parameter: realizedParameter) = 
    let sb = new StringBuilder()
    let append (c: string) = sb.Append(c) |> ignore
    append(parameter.voucherType)
    append(parameter.voucherNo)
    append(parameter.voucherDate.ToFormateStr())
    append(parameter.mappingCode.ToFormatStr(10,false))

    append("*".ToString().ToFormatStr(10,false))

    append(parameter.documentType.ToFormatStr(1))
    append(parameter.documentDate.ToFormateStr())

    append(generateBlankspace(10))

    append(parameter.currencyCode.ToFormatStr(3))

    append(parameter.originAmount.ToThousandSeparatedString(15))
    append(parameter.exchangeRate.ToFormatStr("12:F5"))
    append(parameter.equvAmount.ToThousandSeparatedString(15))

    append(parameter.d_cSign.ToFormatStr(1))

    let isStartOrEnd = isTheStartOrEndOfTheMonth (parameter.fromTradeDay) (parameter.toTradeDay)
    let mutable desc5 = ""
    match isStartOrEnd with
    | None ->
        let s1,s2,s3,s4,s5,s6 =
            parameter.fromTradeDay.ToFormateStr(),
            parameter.toTradeDay.ToFormateStr(),
            parameter.productName,
            parameter.currencyCode,
            parameter.originAmount.ToThousandSeparatedString(0),
            parameter.monthlyChangeRate.ToFormatStr("0:F5")
        desc5 <- sprintf "由%s至%s孖展客%s交易平倉盈虧%s%s@%s" s1 s2 s3 s4 s5 s6

    | Some(year, month) ->
        let d1,d2,s1,s2,s3,s4 = 
            year,
            month,
            parameter.productName,
            parameter.currencyCode,
            parameter.originAmount.ToThousandSeparatedString(0),
            parameter.monthlyChangeRate.ToFormatStr("0:F5")
        desc5 <- sprintf "%d/ %d月份孖展客%s交易平倉盈虧%s%s@%s" d1 d2 s1 s2 s3 s4
    
    append(desc5.ToFormatStr(60,false))  
    sb.ToString()


    