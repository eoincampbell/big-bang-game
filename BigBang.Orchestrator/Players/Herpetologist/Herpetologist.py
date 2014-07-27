from sys import argv
from random import choice
print("PLSVR"[len(argv)<2 or "RVPSL".index(sorted([choice(argv[2]) for i in range(12)], key=lambda *a:argv[1].count(a[0]))[choice([0, 1])])])