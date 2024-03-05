(* 1 Zadanie *)

(* Rekurencyjna implementacja obliczania n-liczby fibonacciego *)
(* Działa w czasie O(2^n))
let rec fib_rec n =
  match n with
  | 0 -> 0
  | 1 -> 1
  | _ -> fib_rec (n - 2) + fib_rec (n - 1)

(* Iteracyjna implementacja obliczania n-liczby fibonacciego *)
(* Działa w czasie O(n) *)
let fib_iter n =
  let rec it n a b =
    match n with
    | 0 -> a
    | _ -> it (n - 1) b (a + b)
  in it n 0 1
