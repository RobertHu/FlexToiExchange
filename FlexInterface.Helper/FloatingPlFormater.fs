module private  FlexInterface.Helper.FloatingPlFormater
open FlexInterface.Common
open System
open System.Text

type floatingParameter =
    {
        toTradeDay: DateTime;
        voucherType: string;
        voucherNo : string ;
        voucherDate: DateTime;
        mappingCode: string;
        desc3Or4: string;
        documentType: string;
        documentDate: DateTime;
        currencyCode: string;
        originAmount: decimal;
        exchangeRate: decimal;
        equvAmount: decimal;
        d_cSign: string;
        productName: string;
        monthlyChangeRate: decimal;
        tradingAmount: decimal;
        productPrice: string
    }


let formatLine (parameter: floatingParameter) =
    let sb = new StringBuilder()
    let append (c: string) = sb.Append(c) |> ignore
    append(parameter.voucherType)
    append(parameter.voucherNo)
    append(parameter.voucherDate.ToFormateStr())
    append(parameter.mappingCode.ToFormatStr(10,false))
    append(parameter.desc3Or4)
    append(parameter.documentType.ToFormatStr(1))
    append(parameter.documentDate.ToFormateStr())
    append(generateBlankspace(10))
    append(parameter.currencyCode.ToFormatStr(3))
    append(parameter.originAmount.ToThousandSeparatedString(15))
    append(parameter.exchangeRate.ToFormatStr("12:F5"))
    append(parameter.equvAmount.ToThousandSeparatedString(15))
    append(parameter.d_cSign.ToFormatStr(1))
    let mutable ownedOrGainText = ""
    match parameter.originAmount >= 0m with
    |true ->  ownedOrGainText <- "客存"
    |false -> ownedOrGainText <- "客欠"



    let s1,s2,s3,s4,s5,s6 =
        (
            parameter.toTradeDay.ToFormateStr(),
            ownedOrGainText,
            parameter.tradingAmount.ToFormatStr("0:F2"),
            parameter.productName,
            parameter.monthlyChangeRate.ToFormatStr("0:F5"),
            parameter.productPrice
        )
    let desc5 = sprintf "截至%s孖展%s%s%s%s@%s" s1 s2 s3 s4 s5 s6
    append(desc5.ToFormatStr(60,false))
    sb.ToString()