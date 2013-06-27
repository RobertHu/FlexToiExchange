[<AutoOpen>]
module FlexInterface.Helper.Common
open System
open ValidationLibrary.XmlProccessor
open System.IO
open System.Text

let internal CrebitSymbol = "C"
let internal DebitSymbol = "D"

let (|NullOrEmpty|NotNullAndEmpty|) (source: string) =
    match String.IsNullOrEmpty(source) with
    | true -> NullOrEmpty
    | _ -> NotNullAndEmpty


let formatMoney fmt (str: 'a) = String.Format(sprintf "{0:%s}" fmt,str)

let writeToFile (content: string,fs: FileStream,changeLine: bool) =
    let tw= new StreamWriter(fs,Encoding.Default)
    tw.Write(content)
    tw.WriteLine()
    if changeLine then for i in 0..3 do tw.WriteLine()
    tw.Flush()


let getVouchNo (index: int) = GetFormatString(index.ToString(),true)

let getPLVouchNo (index: int) = GetFormatString(index.ToString(),false)



let isTheStartOrEndOfTheMonth (d1: DateTime) (d2: DateTime) =
    match d1.Year = d2.Year && d1.Month = d2.Month with
    |true ->
        let days = DateTime.DaysInMonth(d1.Year,d1.Month)
        let actureDays= (d2 - d1).Days + 1
        match days = actureDays with
        | true ->
           Some(d1.Year,d1.Month)
        | false -> None
    |false -> None


let generateBlankspace n =
    let sb = new System.Text.StringBuilder()
    let seq = seq{for i in 1..n -> " "}
    seq
    |> Seq.iter(fun m -> 
        sb.Append(m) |> ignore
        )
    sb.ToString()


let getReverseValue (input: string) (sides: string *string) =
    let side , otherside  = sides
    match input = side with
    | true -> otherside
    | _ -> side