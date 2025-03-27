
(* The type of tokens. *)

type token = 
  | TRUE
  | TIMES
  | THEN
  | SMALLER
  | SMALLEQ
  | RPAREN
  | PLUS
  | NOTEQ
  | MINUS
  | LPAREN
  | INT of (int)
  | IF
  | GREATER
  | GREATEQ
  | FALSE
  | EQ
  | EOF
  | ELSE
  | DIV

(* This exception is raised by the monolithic API functions. *)

exception Error

(* The monolithic API. *)

val prog: (Lexing.lexbuf -> token) -> Lexing.lexbuf -> (Ast.expr)
