; Format: (input_expression) ; expected_output

(nil? nil) ; true
(nil? 0) ; false
(symbol? x) ; true
(symbol? 123) ; false
(number? 42) ; true
(number? x) ; false
(list? (1 2 3)) ; true
(list? 4) ; false
(cons 1 '(2 3)) ; (1 2 3)
(car (1 2 3)) ; 1
(car ((1 2) 3)) ; (1 2)
(cdr (1 2 3)) ; (2 3)
(add 1 2) ; 3
(add -3 5) ; 2
(sub 5 3) ; 2
(sub 0 3) ; -3
(mul 2 3) ; 6
(mul -2 3) ; -6
(div 6 2) ; 3
(div 7 2) ; 3.5
(mod 7 3) ; 1
(lt 1 2) ; true
(lt 2 1) ; false
(gt 2 1) ; true
(gt 1 2) ; false
(eq? x x) ; true
(and true false) ; false
(or nil true) ; true
(if true "yes" "no") ; yes
(cond ((eq? 1 2) "no") ((eq? 1 1) "yes")) ; yes
