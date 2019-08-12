


module RLib

    open RDotNet
    open RProvider
    open RProvider.``base``
    open RProvider.stats
    open Deedle


    /// kMeans Cluster using RDotNet / RProvider

    type kMeansClusterR = {
        cluster : int[]
        centers : NumericMatrix
        size : int[]
        totalss : float 
        withinss : float[] 
        totwithinss : float }

    // Helper function to access R data
    let (?) (x:SymbolicExpression) param = 
        let paramDict = 
            x.GetAttribute("names").AsList() 
            |> Seq.mapi (fun i n -> n.GetValue<string>(), i)  
            |> dict
        x.AsList().[paramDict.[param]]
    
    let rKMeansR (nbc: int) (nstart: int option) (df: Frame<int,string>) =
        match nstart with
        | None   -> R.kmeans(x = df, centers = nbc, nstart = 1)
        | Some i -> R.kmeans(x = df, centers = nbc, nstart = i)

    let kMeansF (nbc: int) (nstart: int option) (df: Frame<int,string>) =
        match (df.RowCount >= nbc) with
        | false -> Result.Error "Too many cluster centres"
        | true  ->
                    let km = rKMeansR nbc nstart df
                    { cluster = (km?cluster).GetValue<int[]>()
                      centers = (km?centers).AsNumericMatrix()
                      size    = (km?size).GetValue<int[]>() 
                      totalss = (km?totss).GetValue<float>()
                      withinss = (km?withinss).GetValue<float[]>() 
                      totwithinss = (km?("tot.withinss")).GetValue<float>() } 
                    |> Result.Ok