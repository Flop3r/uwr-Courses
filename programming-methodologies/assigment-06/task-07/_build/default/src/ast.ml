(* abstract syntax tree *)

(* Dodanie NotEq | Greater | Smaller | GreatEq | SmallEq *)
type bop = Mult | Div | Add | Sub | Eq | NotEq | Greater | Smaller | GreatEq | SmallEq

type expr =
  | Int of int
  | Bool of bool
  | Binop of bop * expr * expr
  | If of expr * expr * expr
                               
