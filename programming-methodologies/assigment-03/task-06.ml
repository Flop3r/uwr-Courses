(* UWr Metody Programowania: Lista 3/Zad 6 *)
(* Franciszek Przeliorz *)

type 'a tree = Leaf | Node of 'a tree * 'a * 'a tree

let rec fold_tree f a t =
  match t with
  | Leaf -> a
  | Node (l, v, r) -> f (fold_tree f a l) v (fold_tree f a r)
  

let tree_product t =
  fold_tree (fun l v r -> l * v * r)  1 t

let tree_flip t = 
  let flip l v r = Node(r, v, l) in
  fold_tree flip Leaf t

let tree_height t =
  let height_node l _ r = 1 + max l r in
  fold_tree height_node 0 t

let bst_span t = 
  let max_node _ v r = max v r in
  let min_node l v _ = min v l in
  ((fold_tree max_node min_int t), (fold_tree min_node max_int t))

  let tree_span t =
    let f l v r = 
      match l, r with
      | None, None -> (Some v, Some v)
      | None, Some r' -> (Some v, r')
      | Some l', None -> (l', Some v)
      | Some l', Some r' -> (l', r') in
    fold_tree f None t
  

let preorder t =
  let preorder_node l v r = v :: (l @ r) in
  fold_tree preorder_node [] t
    
let t =
  Node (Node (Leaf, 2, Leaf),
    5,
    Node (Node (Leaf, 6, Leaf),
      8,
      Node (Leaf, 9, Leaf)));;

tree_span t;;