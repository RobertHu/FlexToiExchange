module FlexInterface.Helper.FlexPersistence
open FlexInterface.Common
open System.IO
open System

let private WithdrawType = "PV"
let private DepositType = "RV"
let private TransferType = "HV"


let private DepositTypeOrVISATypeVoucherNo = getVouchNo(1)



type FormatParameter =
    {voucherType: string;voucherNo: string
    ;voucherDate: DateTime;accountNo: string;fileBeginDate: DateTime;
    fileEndDate: DateTime;currency: string; amountOrigin: decimal;currencyRate: decimal;
    amount: decimal;debicOrCrebit: string;desc1: string;desc2: string;customerCode: string
    }


let fillParameter (data)=
    let x1,x2,x3,x4,x5,x6,x7,x8,x9,x10,x11,x12,x13,x14 = data  
    {voucherType=x1;voucherNo=x2;voucherDate=x3;accountNo=x4;
    fileBeginDate=x5;fileEndDate=x6;currency=x7;amountOrigin=x8;currencyRate=x9;
    amount=x10;debicOrCrebit=x11;desc1= x12;desc2=x13;customerCode=x14}


let formateLine (p: FormatParameter) =
    let sb =new System.Text.StringBuilder()
    let placeHolder =" "
    let append (c: string) = sb.Append(c)|>ignore
    append(p.voucherType)
    append(p.voucherNo)
    append(p.voucherDate.ToFormateStr())
    append(p.accountNo.ToFormatStr(10,false))
    
    append(p.customerCode.ToFormatStr(10,false))
    append(placeHolder)
    append(p.fileBeginDate.ToFormateStr())
    append(p.fileEndDate.ToFormateStr())
    append(p.currency.ToFormatStr(3))
    
    append(p.amountOrigin.ToFormatStr("15:F2"))
    
    append(p.currencyRate.ToFormatStr("12:F5"))
    
    append(p.amount.ToFormatStr("15:F2"))
    append(p.debicOrCrebit.ToFormatStr(1))
    
    append(p.desc1.ToFormatStr(60,false))
  
    append(p.desc2.ToFormatStr(60,false))
    
    sb.ToString()





let PersiteDesposit (data: DepositData[],fs: FileStream, startIndex: int,businessType: BusinessTypeEnum) =
    data
    |> Array.iteri (fun i m ->
        
        let mutable dm = Math.Abs(m.SourceCurrencyRate* m.SourceAmount)
        let mutable cm = Math.Abs(m.TargetCurrencyRate * m.TargetAmount)
        let mutable line1 = ""
        let mutable line2 = ""
        let mutable line3 = ""
        let p1 = fillParameter(WithdrawType,getVouchNo(startIndex + i),m.UpdateTime,m.TargetMt4,m.UpdateTime,
                                      m.UpdateTime,m.TargetCurrencyName,Math.Abs(m.TargetAmount),m.TargetCurrencyRate,cm,CrebitSymbol,m.Desc5,m.Desc7,
                                      m.SourceCustomerCode)
        let p2 ={p1 with accountNo= m.MT4Code;currency= m.SourceCurrencyName;amountOrigin= Math.Abs(m.SourceAmount);currencyRate= m.SourceCurrencyRate;
                                 amount= dm;debicOrCrebit=DebitSymbol;desc1=m.Desc6;desc2=m.Desc8;customerCode= m.TransactionNo}
        match businessType with
        |BusinessTypeEnum.Withdraw ->  
            match m.FormatType with
            |DepositFormatEnum.First ->
                line1 <- formateLine(p1)
                line2 <- formateLine(p2)
            |_ ->
                line1 <- formateLine(p1)
                line2 <-  formateLine({p2 with desc1=m.Desc5 ;desc2=m.Desc8;customerCode = m.SourceCustomerCode})
        |BusinessTypeEnum.Deposit ->
            let vouchType = if m.Type = 3 then TransferType else DepositType
            line1 <- formateLine({p2 with voucherType=vouchType; voucherNo=DepositTypeOrVISATypeVoucherNo; debicOrCrebit=CrebitSymbol; desc1= m.Desc5; desc2= m.Desc7 ;customerCode= m.BankCode})
            line2 <- formateLine({p1 with voucherType=vouchType; voucherNo=DepositTypeOrVISATypeVoucherNo; debicOrCrebit=DebitSymbol; desc1=m.Desc6; desc2=m.Desc8 ;customerCode= m.TransactionNo})
            if m.FormatType = DepositFormatEnum.Second 
            then 
                line1 <- formateLine({p2 with voucherType=vouchType; voucherNo=DepositTypeOrVISATypeVoucherNo;  debicOrCrebit=CrebitSymbol; desc1= m.Desc5; desc2= m.Desc7 ;customerCode= m.TransactionNo})
                line2 <- formateLine({p1 with voucherType=vouchType; voucherNo=DepositTypeOrVISATypeVoucherNo; debicOrCrebit=DebitSymbol; desc1=m.Desc6; desc2=m.Desc7 ;customerCode= m.TransactionNo})
                      
        |_ ->
            cm <- Math.Abs(m.Transfer.SourceCurrencyRate * m.Transfer.SourceAmount)
            line1 <- formateLine({p2 with voucherType = TransferType; debicOrCrebit=CrebitSymbol; desc1= m.Desc5; desc2= m.Desc7 ;customerCode= m.SourceCustomerCode})
            let n = m.Transfer
            let p = fillParameter(TransferType,p1.voucherNo,n.UpdateTime,n.AccountCode,n.UpdateTime,
                                  n.UpdateTime,n.SourceCurrencyName,Math.Abs(n.SourceAmount),n.SourceCurrencyRate,cm,DebitSymbol, m.Desc6, m.Desc8,n.SourceCustomerCode)
            line2 <- formateLine(p)
       
        let diff =dm - cm

        match diff <> 0m with
        |true ->
            let mutable debitOrCrebit = ""
            let mutable desc1=m.Desc6 
            let mutable desc2=m.Desc7
            let mutable customerCode = ""
            let mutable voucherType= ""
            let mutable voucherNo = p1.voucherNo
            match businessType with
            |BusinessTypeEnum.Withdraw ->
               if diff > 0m then debitOrCrebit <- CrebitSymbol else debitOrCrebit <- DebitSymbol
               customerCode <- m.TransactionNo
               voucherType <- WithdrawType
               if m.FormatType = DepositFormatEnum.Second then desc1<- m.Desc6 ; desc2 <- m.Desc9
            |BusinessTypeEnum.Deposit ->
                voucherNo <- DepositTypeOrVISATypeVoucherNo
                if diff > 0m then debitOrCrebit <- DebitSymbol else debitOrCrebit<- CrebitSymbol
                customerCode <- m.TransactionNo
                if m.Type = 3 then voucherType <- TransferType  else voucherType <- DepositType
                if m.FormatType = DepositFormatEnum.Second then desc2 <- m.Desc8
            |_ ->
                if diff > 0m then debitOrCrebit <- DebitSymbol else debitOrCrebit<- CrebitSymbol
                customerCode <- m.Transfer.TransactionNo
                voucherType <- TransferType

            let p3 = fillParameter(voucherType,voucherNo,m.UpdateTime,m.iExchangePlMT4LoginID,m.UpdateTime,m.UpdateTime,
                                    m.BaseCurrencyName,Math.Abs(diff),1m,Math.Abs(diff),debitOrCrebit,desc1,desc2,customerCode)
            line3 <- formateLine(p3)
        |_ -> ()
        writeToFile(line1,fs)
        match line3 with
        |NotNullAndEmpty -> 
                writeToFile(line2,fs)
                writeToFile(line3,fs)
        |NullOrEmpty -> writeToFile(line2,fs)
        appendNewLine(fs)

        )

     


