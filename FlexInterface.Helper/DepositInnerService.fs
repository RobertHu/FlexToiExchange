module FlexInterface.Helper.DepositInnerService
open System
open FlexInterface.Common
let AddDesc(businessType: BusinessTypeEnum,data: DepositData) =
    
    let getFromAccountName_CustomerAliasCode_CustomerName() =
        data.BankName,data.SourceCustomerCode,data.SourceCustomerName

    match businessType with
    |BusinessTypeEnum.Transfer ->
        data.FormatType <- DepositFormatEnum.Third
        let tscc,tscn,scc,scn = data.Transfer.SourceCustomerCode,data.Transfer.SourceCustomerName,data.SourceCustomerCode,data.SourceCustomerName
        data.Desc5 <- sprintf "由戶口 %s %s 轉入戶口 %s %s" tscc tscn scc scn
        data.Desc6 <- data.Desc5
        data.Desc7 <- ""
        data.Desc8 <- sprintf "，%s" data.TransactionNo
    |_ -> 
        match data.SourceCurrency = data.TargetCurrency with
        |true ->
            data.FormatType <- DepositFormatEnum.First
            if businessType= BusinessTypeEnum.Withdraw then
               let fromAccountName,toCustomerAliasCode,customerName = getFromAccountName_CustomerAliasCode_CustomerName()
               data.Desc5 <-  sprintf "%s - 付 %s %s" fromAccountName toCustomerAliasCode customerName
               data.Desc6 <- data.Desc5
               let ad,tn = data.AccountingDesription,data.TransactionNo
               data.Desc7 <- sprintf "%s，%s" ad tn
               data.Desc8 <-ad
            else
                let fromAccountName,toCustomerAliasCode,customerName = getFromAccountName_CustomerAliasCode_CustomerName()
                let ad,transactionCode = data.AccountingDesription,data.TransactionNo
                data.Desc5 <- sprintf "%s - 來款 %s %s" fromAccountName toCustomerAliasCode customerName
                data.Desc7 <- sprintf "%s，%s" ad transactionCode
                data.Desc8 <- data.Desc7
                data.Desc6 <- data.Desc5
        |_ ->
            data.FormatType <- DepositFormatEnum.Second
            data.MonthlyRate <- float (Math.Max(data.SourceAmount/data.TargetAmount , data.TargetAmount/data.SourceAmount))
            let fmr = formatMoney "F4" (data.MonthlyRate)
            let fdtr = formatMoney "F4" (data.DayTradeRate)

            match businessType with
            |BusinessTypeEnum.Deposit ->
                let fromAccountName,toCustomerCode,toCustomerName = getFromAccountName_CustomerAliasCode_CustomerName()
                data.Desc5 <- sprintf "%s- 來款 %s %s" fromAccountName toCustomerCode toCustomerName
                data.Desc6 <- data.Desc5
                let cn,fdm = data.TargetCurrencyName, formatMoney "n2" data.TargetAmount 
                data.Desc7 <- sprintf "%s%s@%s" cn fdm fmr
                data.Desc8 <- sprintf "%s%s之匯率差額 (@%s-%s)" cn fdm fmr fdtr
            |_ -> 
                let fromAccountName,toCustomerAliasCode,customerName = getFromAccountName_CustomerAliasCode_CustomerName()
                data.Desc5 <- sprintf "%s - 付 %s %s" fromAccountName toCustomerAliasCode customerName
                data.Desc6 <- data.Desc5
                let ad,tn = data.AccountingDesription,data.TransactionNo
                let cn,fdm = data.SourceCurrencyName,formatMoney "n2" (Math.Abs(data.SourceAmount))
                data.Desc7 <- sprintf "%s@%s, %s" ad fmr tn
                data.Desc8 <- sprintf "%s@%s" ad fmr
                data.Desc9 <- sprintf "%s%s之匯率差額 (@%s-%s)，%s" cn fdm fmr fdtr tn
    

