;; Utility ;;
;;;;;;;;;;;;;

(defconstant +possible-moves+ '((paper . #\p) (scissors . #\s) (rock . #\r) (lizard . #\l) (spock . #\v)))

;; Split input string at delim
(defun split-string (string delim)
  (loop for i = 0 then (1+ j)
       as j = (position delim string :start i)
       collect (subseq string i j)
       while j))

(defun get-move (ch)
  (let ((early-move (car (rassoc (char-downcase ch) +possible-moves+))))
    (if (not early-move)
        'forfeit
        early-move)))

;; Take input string and turn into list of symbols for moves
(defun get-moves (string)
  (let ((move-list (coerce string 'list)))
    (loop for ch in move-list
         collect (get-move ch))))

;; Get his moves
(defun get-args ()
  (get-moves (caddar (split-string *posix-argv* #\Space))))

;; Game Logic ;;
;;;;;;;;;;;;;;;;

;; Randomly select move from list
(defun get-random-move ()
  (cdr
   (nth
    (random (list-length +possible-moves+))
    +possible-moves+)))

;; Return list of move counts for input moveset
(defun count-moves (move-set)
  (let ((keys (remove-duplicates move-set)))
    (let ((counts
           (loop for key in keys
                collect (count key move-set))))
      (mapcar #'list keys counts))))

;; Select highest move used from previous moves ignoring ties
(defun max-move (move-list)
  (loop for move in move-list
       with max = (car move-list)
       when (< (cadr max) (cadr move)) do (setf max move)
       finally (return (car max))))

;; Make an educated guess at their next input
(defun parse-input (move-set)
  (let ((move (max-move (count-moves move-set))))
    (cond
      ((eq 'paper    move) #\L)
      ((eq 'scissors move) #\R)
      ((eq 'rock     move) #\V)
      ((eq 'lizard   move) #\S)
      ((eq 'spock    move) #\P)
      (t (get-random-move)))))

;; Main Function ;;
;;;;;;;;;;;;;;;;;;;
(defun main ()
  ;; Reseed RNG
  (setf *random-state* (make-random-state t))
  (let ((input (get-args)))
    (if (< (list-length input) 3)
        (princ (char-upcase (get-random-move)))
        (princ (char-upcase (parse-input input))))))