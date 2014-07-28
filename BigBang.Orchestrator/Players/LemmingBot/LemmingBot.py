import sys

def totallyRandomStarterPick() :
    return "P"    ### Chosen by fair dice roll

picks = ["S", "V", "L", "R", "P"]

try :
    opn = sys.argv[2][-1]
    print(picks[picks.index(opn) - 1])    ### Kill me!
except Exception :
    print(totallyRandomStarterPick())