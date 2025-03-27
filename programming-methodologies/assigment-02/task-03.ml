(* 4 Zadanie *)

(* Funckja znajdująca max wartość w li)
let rec mem x xs = 
  if xs = [] then false
  else if x = List.hd xs then true
  else mem x (List.tl xs);;