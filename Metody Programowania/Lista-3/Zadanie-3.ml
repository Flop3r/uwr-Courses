(* UWr Metody Programowania: Lista 3/Zad 3 *)
(* Franciszek Przeliorz *)

let build_list n f =
  let rec aux i acc =
    if i = n then
      acc
    else
      aux (i + 1) (acc @ [f i])
  in
  aux 0 []

let negatives n =
  build_list n (fun x -> -1 - n)

let reciprocals n =
  build_list n (fun x -> 1.0 /. float_of_int (x + 1))

let evens n =
  build_list n (fun x -> x * 2)

let identityM n =
  build_list n (fun i ->
    build_list n (fun j ->
      if i = j then 1 else 0
    )
  )
