let empty_set = fun x -> false;;

let singleton a = fun x -> x = a;;

let rec in_set a s = s a;;

let union s t = fun x -> (in_set x s) || (in_set x t);;

let intersect s t = fun x -> (in_set x s) && (in_set x t);;
