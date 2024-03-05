(* 6 Zadanie *) 

let rec suffixes xs =
  let rec suff_p xs sfxs =
    match xs with
    | [] -> []
    | x :: t -> xs :: (suff_p t sfxs)
  in
  if xs = [] then [[]]
  else (suff_p xs []);;