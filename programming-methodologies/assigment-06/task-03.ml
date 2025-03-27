type 'a symbol =
| T of string
| N of 'a

type 'a grammar = ('a * ('a symbol list) list) list

(* Funkcja pomocnicza zwracająca losowy element listy *)
let choose_random_element (lst : 'a list) : 'a =
  let index = Random.int (List.length lst) in
  List.nth lst index

(* Funkcja zwracająca losowe zdanie typu złozone z wyrazow typu 'a *)
let rec generate (grammar : 'a grammar) (a : 'a) =
  let pds = (List.assoc a grammar) in 
  let p = choose_random_element pds in 
  (String.concat "" (List.map 
  (fun x -> 
    match x with 
    | N sym -> (generate grammar sym)
    | T sym -> sym
  ) 
  p));;

(* Gramatyki *)
  
let expr : unit grammar = 
  [(), [[N (); T "+"; N ()];
  [N (); T "*"; N ()];
  [T "("; N (); T ")"]; [T "1"];
  [T "2"]]]

let pol : string grammar =
  [ "zdanie" , [[ N "grupa-podmiotu" ; N "grupa-orzeczenia" ]]
  ; "grupa-podmiotu" , [[ N "przydawka" ; N "podmiot" ]]
  ; "grupa-orzeczenia" , [[ N "orzeczenie" ; N "dopelnienie" ]]
  ; "przydawka" , [[ T "Piekny " ];
                  [ T "Bogaty " ];
                  [ T "Wesoly " ]]
  ; "podmiot" , [[ T "policjant " ];
                [ T "student " ];
                [ T "piekarz " ]]
  ; "orzeczenie" , [[ T "zjadl " ];
                    [ T "pokochal " ];
                    [ T "zobaczyl " ]]
  ; "dopelnienie" , [[ T "zupe." ];
                    [ T "studentke." ];
                    [ T "sam siebie." ];
                    [ T "instytut informatyki." ]]
];;
  
  

  