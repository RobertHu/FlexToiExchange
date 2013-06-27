module DebugHelper
open System
open System.Diagnostics

type System.DateTime with
    member t.ToStandardDateTime() = t.ToString("yyyy-MM-dd HH:mm:ss")

let Log(content)=
    Debug.WriteLine(sprintf "%s %s" (DateTime.Now.ToStandardDateTime()) content)

let LogWhen(condition,content) =
    Debug.WriteLineIf(condition,sprintf "%s %s" (DateTime.Now.ToStandardDateTime()) content)



