module ValidationLibrary.XmlProccessor
open System.Linq
open System.IO
open System.Text.RegularExpressions
open  System.Diagnostics

let rec prefix (i:int) (init:string) =
    if i=0 then init
    else prefix (i-1) ("0"+init)

let GetFormatString ((index:string),(isDeposit:bool)) =
    let baseIndex =if isDeposit then 10 else 8
    (prefix (baseIndex-index.Length) "")+index

 

let GetFormatStr (baseIndex:string) (index:string)  =
    (prefix (baseIndex.Length - index.Length) "")+index


let GetContinuousCustomerNo((start:string),(last:string))=
    let rec get (start:int) (last:int) (acc:string list) =
        if start=last then (string start)::acc
        elif start > last then acc
        else get (start+1) last ((string start)::acc)
    let regex=new Regex("^0+([1-9][\d]+)$")
    let regex2= new Regex("^0+([1-9][\d]+)$")
    let r1 = regex.Match(start)
    let subString m =
        match m with
        |"" -> ""
        |_ -> m.Substring(0,m.Length-1)
    try
        match r1.Success with
        |false ->
            let s= System.Int32.Parse(start)
            let e = System.Int32.Parse(last)
            if System.Math.Abs(e - s) > 10000 then false,null
            else true,(get s e [] |> List.fold (fun m n -> sprintf "%s%s%s" n "," m) ""|>subString)
        |true ->
            let r2= regex2.Match(last)
            let r11 =System.Int32.Parse(r1.Groups.[1].Value)
            let r22 =System.Int32.Parse(r2.Groups.[1].Value)
            if System.Math.Abs(r22 - r11) > 10000 then false,null
            else true,(get r11 r22 [] |> List.map (GetFormatStr start)|> List.fold (fun m n -> sprintf "%s%s%s" n "," m) "" |> subString)
    with
    |e ->
        Debug.WriteLine(e)
        (false ,null)