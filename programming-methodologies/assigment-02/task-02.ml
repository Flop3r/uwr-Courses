(* 2 Zadanie *)

(* Definicja macieży *)
let matrix a b c d = (a, b, c, d)

(* Dostęp do wartości macieży *)
let matrix_a (a, _, _, _) = a
let matrix_b (_, b, _, _) = b
let matrix_c (_, _, c, _) = c
let matrix_d (_, _, _, d) = d

(* Procedura mnożenia macieży m i n *)
let matrix_mult m n =
  let a = matrix_a m and b = matrix_b m and c = matrix_c m and d = matrix_d m in
  matrix
    (a * matrix_a n + b * matrix_c n)
    (a * matrix_b n + b * matrix_d n)
    (c * matrix_a n + d * matrix_c n)
    (c * matrix_b n + d * matrix_d n)

(* Definicja macieży identycznościowej *)
let matrix_id = matrix 1 0 0 1

(* Potęgowanie macierzy *)
let rec matrix_expt m k =
  match k with
  | 0 -> matrix_id
  | 1 -> m
  | _ -> matrix_mult m (matrix_expt m (k - 1));;

(* Obliczanie liczby fibonacciego metodą macieży *)
(* Działa w czasie O(n) *)
let fib_matrix k = matrix_b (matrix_expt (matrix 1 1 1 0) k)