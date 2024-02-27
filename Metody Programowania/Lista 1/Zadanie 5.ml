let func a b c = 
  if a <= b && a <= c then
    (b*b) + (c*c) 
  else if b <= a && b <= c then
    (a*a) + (c*c)
  else
    (a*a) + (b*b);;


let test_cases = [
  (* Test when a is the smallest *)
  (1, 2, 3, 13);  (* Expected result: (2*2) + (3*3) = 4 + 9 = 13 *)
  (1, 3, 2, 13);  (* Expected result: (3*3) + (2*2) = 9 + 4 = 13 *)
  (1, 1, 1, 2);   (* Expected result: (1*1) + (1*1) = 1 + 1 = 2 *)

  (* Test when b is the smallest *)
  (2, 1, 3, 13);   (* Expected result: (2*2) + (3*3) = 4 + 9 = 13 *)
  (2, 3, 1, 13);  (* Expected result: (2*2) + (3*3) = 4 + 9 = 13 *)
  (2, 2, 1, 8);   (* Expected result: (2*2) + (2*2) = 4 + 4 = 8 *)

  (* Test when c is the smallest *)
  (3, 1, 2, 13);   (* Expected result: (3*3) + (2*2) = 9 + 4 = 13 *)
  (3, 2, 1, 13);  (* Expected result: (3*3) + (2*2) = 9 + 4 = 13 *)
  (3, 2, 2, 13);  (* Expected result: (3*3) + (2*2) = 9 + 4 = 13 *)

  (* Additional test cases *)
  (5, 4, 3, 41);  (* Expected result: (5*5) + (4*4) = 25 + 16 = 41 *)
  (4, 5, 3, 41);  (* Expected result: (5*5) + (4*4) = 25 + 16 = 41 *)
  (4, 3, 5, 41);  (* Expected result: (5*5) + (4*4) = 25 + 16 = 41 *)
]

let run_test_cases () =
  List.iter (fun (a, b, c, expected) ->
      let result = func a b c in
      Printf.printf "Input: (%d, %d, %d), Expected: %d, Result: %d\n" a b c expected result;
      assert (result = expected)
    ) test_cases

let () = run_test_cases ()
