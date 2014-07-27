import sys
import random

choices = ["L", "P", "R", "S", "V"]

total = len(sys.argv)
if total==1:
    print("L")
    sys.exit()

opponent = sys.argv[2]
opponent_last = opponent[-1]

if opponent_last == choices[-1]:
    print(random.choice(choices))
else:
    next = choices.index(opponent_last)+1
    print(choices[next])