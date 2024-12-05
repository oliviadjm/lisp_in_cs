; Format: (input_expression) ; expected_output

(nil? nil) ; true
(nil? 0) ; false
(symbol? 'x) ; true
(symbol? 123) ; false
(number? 42) ; true
(number? 'x) ; false
(list? '(1 2 3)) ; true
(list? 4) ; false
(cons 1 '(2 3)) ; (1 2 3)
(car '(1 2 3)) ; 1
(car '((1 2) 3)) ; (1 2)
(cdr '(1 2 3)) ; (2 3)
(add 1 2) ; 3
(add -3 5) ; 2
(add 1 2 3) ; 6
(sub 5 3) ; 2
(sub 0 3) ; -3
(sub 3 2 1) ; 0
(mul 2 3) ; 6
(mul -2 3) ; -6
(div 6 2) ; 3
(div 7 2) ; 3.5
(mod 7 3) ; 1
(lt 1 2) ; true
(lt 2 1) ; false
(gt 2 1) ; true
(gt 1 2) ; false
(eq? 'x 'x) ; true
(and true false) ; false
(or nil true) ; true
(if true "yes" "no") ; yes
(cond ((eq? 1 2) "no") ((eq? 1 1) "yes")) ; yes
(cons '() nil) ; (() nil)
(add 1 2 3 4 5) ; 15
(sub 10 5 2) ; 3
(mul 2 3 4) ; 24
(sub 5.0 2.5) ; 2.5
(mul 3.5 2) ; 7
(div 7 2) ; 3.5
(mod 10 3) ; 1
(not true) ; false
(not false) ; true
(and true true) ; true
(and false true) ; false
(and true false) ; false
(or false false) ; false
(or true false) ; true
(and (or false true) (eq? 1 1)) ; true
(and (not (eq? 1 1)) (not (eq? 2 2))) ; false
(if (lt 5 10) (if (gt 5 3) "yes" "no") "no") ; yes
(if (lt 10 5) (if (gt 5 3) "yes" "no") "no") ; no
(car '(1 "string" true nil)) ; 1
(cdr '(1 "string" true nil)) ; (string true nil)
