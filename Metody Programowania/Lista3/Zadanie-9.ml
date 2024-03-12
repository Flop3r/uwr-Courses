type 'a tree = Leaf | Node of 'a tree * 'a * 'a tree

let rec find_min t =
  match t with
  | Leaf -> None
  | Node (Leaf, value, _) -> Some value
  | Node (left, _, _) -> find_min left

  
let rec delete x t =
  match t with
  | Leaf -> Leaf
  | Node (left, value, right) ->
      if x < value then
        Node (delete x left, value, right)
      else if x > value then
        Node (left, value, delete x right)
      else
        match left, right with
        | Leaf, _ -> right
        | _, Leaf -> left
        | _, _ ->
            match find_min right with
            | None -> Leaf
            | Some min_val -> Node (left, min_val, delete min_val right)

(* Pomogłem sobie gpt-3.5 zdebugowac i obsluzyc wartosci typu option *)


let t =
  Node (Node (Leaf, 2, Leaf),
    5,
    Node (Node (Leaf, 6, Leaf),
      8,
      Node (Leaf, 9, Leaf)))

