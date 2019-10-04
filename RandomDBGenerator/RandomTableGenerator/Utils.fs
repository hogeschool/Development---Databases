module Utils

open System

let mergeMaps (m1 : Map<'k,'v>) (m2 : Map<'k,'v>) : Map<'k,'v> =
  m2 |> Map.fold (fun m k v -> m.Add(k,v)) m1

let mapItems (m : Map<'k,'v>) : List<'v> =
  m |> Map.fold(fun items k v -> v :: items) [] |> List.rev

let randomListElement (random : Random) (l : List<'a>) : Option<'a> =
  match l with
  | [] -> None
  | _ -> Some l.[random.Next(l.Length)]

let addMapList (key : 'k,value : 'v) (m : Map<'k,List<'v>>) : Map<'k,List<'v>> =
  match m.TryFind(key) with
  | None -> m.Add(key,[value])
  | Some l -> m.Add(key,value :: l)
