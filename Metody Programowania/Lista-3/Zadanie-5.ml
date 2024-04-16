(* UWr Metody Programowania: Lista 3/Zad 5 *)
(* Franciszek Przeliorz *)

type 'a tree = Leaf | Node of 'a tree * 'a * 'a tree

let rec insert_bst x t =
  match t with
  | Leaf -> Node (Leaf, x, Leaf)
  | Node (left, value, right) ->
      if x < value then
        Node (insert_bst x left, value, right)
      else if x > value then
        Node (left, value, insert_bst x right)
      else
        t

let t =
  Node (Node (Leaf, 2, Leaf),
    5,
    Node (Node (Leaf, 6, Leaf),
      8,
      Node (Leaf, 9, Leaf)))


let result = insert_bst 7 t