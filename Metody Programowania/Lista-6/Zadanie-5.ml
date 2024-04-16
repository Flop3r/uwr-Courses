(* Funkcja zamieniająca stringa w listę charow *)
let rec list_char (str : string) : char list =
  match str with
  | "" -> []
  | str -> String.get str 0 :: list_char (String.sub str 1 (String.length str - 1))

(* Funkcja sprawdzająca czy argument tworzy napis poprawnie
nawiasowany składający się z symboli ’(’, ’)’, ’[’, ’]’, ’{’ oraz ’}’ *)
let parens_ok (str : string) : bool =
  let rec parens_count (chars : char list) (stack : char list) : bool =
    match (chars, stack) with
    | ([], []) -> true
    | ([], _) -> false
    | (x :: xs, _) ->
        match x with
        | '(' | '[' | '{' -> parens_count xs (x :: stack)
        | ')' ->
            (match stack with
            | '(' :: rest -> parens_count xs rest
            | _ -> false)
        | ']' ->
            (match stack with
            | '[' :: rest -> parens_count xs rest
            | _ -> false)
        | '}' ->
            (match stack with
            | '{' :: rest -> parens_count xs rest
            | _ -> false)
        | _ -> false
  in
  let chars = list_char str in
  parens_count chars []