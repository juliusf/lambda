(define gcd (lambda (a b) (if (= b 0) a (gcd b (modulo a b)))))

(define scons (lambda (x y) (lambda (m) (m x y))))
(define scar (lambda (z) (z (lambda (p q) p))))
(define scdr (lambda (z) (z (lambda (p q) q))))