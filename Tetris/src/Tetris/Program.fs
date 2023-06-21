// For more information see https://aka.ms/fsharp-console-apps
//printfn "Hello from F#"

open System
open Library

[<EntryPoint>]
let main args =

    let rand = System.Random()

    let mutable blocco: Blocco = new Blocco((Utils.initBloccoInt 3 3 0), 3, 6)

    blocco.generateRandom (rand.Next(1, 5))

    let mappa: Mappa = new Mappa(20, 20)

    mappa.initFloor

    reactiveKey()
    gravity()

    UtilsView.printMappa (mappa.getIstanceWith blocco)

    // NO END RN
    System.Threading.Thread.Sleep(-1);

    0