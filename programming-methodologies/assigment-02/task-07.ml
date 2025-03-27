(* 7 Zadanie *)

(* Funkcja sprawdzająca czy funkcja jest posortowana niemalejąco *)
let rec is_sorted xs =
  if xs = [] || List.tl xs = [] then true
  else if List.hd xs > List.hd (List.tl xs) then false
  else is_sorted (List.tl xs);;