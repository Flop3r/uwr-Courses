(* 5 Zadanie *) 

(* Funckcja zwracająca maksymalną wartość nalezacą do listy floatów *)
let rec maximum xs =
  let rec max_p xs m =
    if xs = [] then m
    else if (List.hd xs) > m then max_p (List.tl xs) (List.hd xs)
    else max_p (List.tl xs) m
  in
  if xs = [] then neg_infinity
  else max_p xs (List.hd xs);;     
