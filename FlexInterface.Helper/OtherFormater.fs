module private FlexInterface.Helper.OtherFormater
open FlexInterface.Common
open System.Text
open System

type internal OtherPlParameter  = 
    {
        fromTradeDay: DateTime;
        toTradeDay: DateTime;
        voucherType: string;
        voucherNo : string ;
        voucherDate: DateTime;
        mappingCode: string;
        CustomerNo: string;
        CustomerName:string;
        documentType: string;
        documentDate: DateTime;
        documentDueDate: string;
        currencyCode: string;
        originAmount: decimal;
        exchangeRate: decimal;
        equvAmount: decimal;
        d_cSign: string;
        productName: string;
        businessTypeDesc:string;
        isIncomeOrExpenseDesc: string
    }

let internal formatLine (parameter: OtherPlParameter) =
    let sb = new StringBuilder()
    let append (c: string) = sb.Append(c) |> ignore
    append(parameter.voucherType)
    append(parameter.voucherNo)
    append(parameter.voucherDate.ToFormateStr())
    append(parameter.mappingCode.ToFormatStr(10,false))
    append(parameter.CustomerNo.ToFormatStr(10,false))
    append(parameter.documentType.ToFormatStr(1))
    append(parameter.documentDate.ToFormateStr())
    append(parameter.documentDueDate)
    append(parameter.currencyCode.ToFormatStr(3))
    append(parameter.originAmount.ToThousandSeparatedString(15))
    append(parameter.exchangeRate.ToFormatStr("12:F5"))
    append(parameter.equvAmount.ToThousandSeparatedString(15))
    append(parameter.d_cSign.ToFormatStr(1))
    let mutable desc5 = ""
    let b1 , b2 = parameter.businessTypeDesc , parameter.isIncomeOrExpenseDesc
    let s1,s2,s3=
        parameter.fromTradeDay.ToFormateStr(),
        parameter.toTradeDay.ToFormateStr(),
        parameter.productName
    desc5 <- sprintf "%s-%s孖展%s%s(%s)" s1 s2 b1 b2 s3
        
    append(desc5.ToFormatStr(60,false))  
    sb.ToString()