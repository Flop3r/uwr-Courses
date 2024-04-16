(* 4 Zadanie *)

(* Funckja sprawdzająca czy x nalezy do listy *)
let rec mem x xs = 
  if xs = [] then false
  else if x = List.hd xs then true
  else mem x (List.tl xs);;