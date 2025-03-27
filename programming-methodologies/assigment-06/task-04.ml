(* Funkcja zamieniająca stringa w listę charow *)
let rec list_char (str : string) : char list =
  match str with
  | "" -> []
  | str -> String.get str 0 :: list_char (String.sub str 1 (String.length str - 1))

(* Funkcja sprawdzająca czy argument jest napisem zawierajacym
tylko symbole ’(’ oraz ’)’, które tworza ̨ poprawne nawiasowanie *)
let parens_ok (str : string) : bool =
  let rec parens_count (chars : char list) (l : int) (r : int) : bool =
    match chars with
    | [] -> l = r
    | x :: xs -> 
      match x with
      | '(' -> parens_count xs (l+1) r
      | ')' -> if l > r then parens_count xs l (r+1) else false
      | _ -> false
  in
  let chars = List.of_seq (String.to_seq str) in
  parens_count chars 0 0
