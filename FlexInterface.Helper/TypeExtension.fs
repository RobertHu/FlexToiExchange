[<AutoOpen>]
module FlexInterface.Helper.TypeExtension
open System

let formatStr fmt (str: 'a) = String.Format(sprintf "{0,%s}" fmt,str)

type DateTime with
    member this.ToFormateStr() =this.ToString("dd/MM/yyyy")

type System.String with
    member this.IsNullOrEmpty() = String.IsNullOrEmpty(this)
    member this.ToFormatStr(len: int ,?positive:bool) = 
        if this = null then ""
        else
            let p = defaultArg positive true
            let symbol = if p then "" else "-"
            let target=if this.Length > len then  this.Substring(0,len) else this
            formatStr (sprintf "%s%d" symbol len) target

type System.Decimal with
    member this.ToFormatStr(s: string) = 
        let absThis = Math.Abs(this)
        formatStr s absThis

type System.Decimal with
    member this.ToThousandSeparatedString(len: int ,?floatLen: int) = 
        let afterLen = defaultArg floatLen 2
        let formatPattern = sprintf "%d:n%d" len afterLen
        let absThis = Math.Abs(this)
        formatStr formatPattern absThis 



type System.Double with
    member this.ToThousandSeparatedString() = 
        let absThis = Math.Abs(this)
        formatStr "n2" absThis 

