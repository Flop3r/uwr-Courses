%{
open Ast
%}

%token <int> INT
%token TIMES
%token DIV
%token PLUS
%token MINUS
%token LPAREN
%token RPAREN
%token EQ

%token NOTEQ
%token GREATEQ
%token SMALLEQ
%token GREATER
%token SMALLER

(*---------*)
%token TRUE
%token FALSE
%token IF
%token THEN
%token ELSE
%token EOF
(*---------*)

%start <Ast.expr> prog

%nonassoc ELSE

(*-----------------------------------------*)
%nonassoc EQ NOTEQ GREATEQ SMALLEQ GREATER SMALLER
(*-----------------------------------------*)

%left PLUS MINUS
%left TIMES DIV

%%

prog:
  | e = expr; EOF { e }
  ;

expr:
  | i = INT { Int i }
  | e1 = expr; PLUS; e2 = expr { Binop(Add, e1, e2) }
  | e1 = expr; MINUS; e2 = expr { Binop(Sub, e1, e2) }
  | e1 = expr; DIV; e2 = expr { Binop(Div, e1, e2) }
  | e1 = expr; TIMES; e2 = expr { Binop(Mult, e1, e2) }
  | LPAREN; e = expr; RPAREN { e }
  | TRUE { Bool true }
  | FALSE { Bool false }
  | e1 = expr; EQ; e2 = expr { Binop(Eq, e1, e2) }
  (*-----------------------------------------------------------------*)
  | e1 = expr; NOTEQ; e2 = expr { Binop(NotEq, e1, e2) }
  | e1 = expr; SMALLEQ; e2 = expr { Binop(SmallEq, e1, e2) }
  | e1 = expr; GREATEQ; e2 = expr { Binop(GreatEq, e1, e2) }
  | e1 = expr; GREATER; e2 = expr { Binop(Greater, e1, e2) }
  | e1 = expr; SMALLER; e2 = expr { Binop(Smaller, e1, e2) }
  (*-----------------------------------------------------------------*)
  | IF; e1 = expr; THEN; e2 = expr; ELSE; e3 = expr { If(e1, e2, e3) }
  ;
