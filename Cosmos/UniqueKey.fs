﻿[<AutoOpen>]
module FSharp.Azure.Cosmos.UniqueKey

open Microsoft.Azure.Cosmos

type UniqueKeyPolicyBuilder () =

    member __.Zero () = UniqueKeyPolicy ()

    member builder.Yield (key : UniqueKey) =
        builder.Key (builder.Zero (), key)

    member builder.Yield _ = builder.Zero ()

    [<CustomOperation "key">]
    member __.Key (policy : UniqueKeyPolicy, key : UniqueKey) =
        policy.UniqueKeys.Add key
        policy

    [<CustomOperation "keys">]
    member __.Keys (policy : UniqueKeyPolicy, keys : UniqueKey seq) =
        let policyKeys = policy.UniqueKeys
        keys |> Seq.iter policyKeys.Add
        policy

let uniqueKeyPolicy = UniqueKeyPolicyBuilder ()

type UniqueKeyBuilder () =

    member __.Zero () = UniqueKey ()

    member builder.Yield (path : string) =
        builder.Path (builder.Zero (), path)

    member builder.Yield _ = builder.Zero ()

    [<CustomOperation "path">]
    member __.Path (key : UniqueKey, path : string) =
        key.Paths.Add path
        key

    [<CustomOperation "paths">]
    member __.Paths (key : UniqueKey, paths : string seq) =
        let keyPaths = key.Paths
        paths |> Seq.iter keyPaths.Add
        key

let uniqueKey = UniqueKeyBuilder ()
