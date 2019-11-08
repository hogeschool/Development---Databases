module ParserUtils

open GeneratorLanguage

let generateDb (tableDefinitions : List<TableDefinition>) : Map<string,TableDefinition> =
  let rawDatabase =
    tableDefinitions |>
    List.map (fun def -> (def.Name,def)) |>
    Map.ofList
  let processedDefinitions =
    rawDatabase |>
    Map.map(
      fun _ tableDefinition ->
        {
          tableDefinition with
            ForeignKeys =
              tableDefinition.ForeignKeys |>
              Map.map(fun reference cols ->
                let referencePK = rawDatabase.[reference].PrimaryKey
                referencePK |>
                List.zip (cols |> List.map fst)
              )
        }
  )
  processedDefinitions

