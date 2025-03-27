(* UWr Metody Programowania: Lista 3/Zad 4 *)
(* Franciszek Przeliorz *)

let empty_set x = fun x -> false;;

let singleton a = fun x -> x = a;;

let in_set a s = s a;;

let union s t = fun x -> (in_set x s) || (in_set x t);;

let intersect s t = fun x -> (in_set x s) && (in_set x t);; 