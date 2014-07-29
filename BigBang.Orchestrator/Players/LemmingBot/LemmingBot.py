import sys, random

def totallyRandomStarterPick() :
    return "P"    ### Chosen by fair dice roll

picks = { "S" : "PL", "P" : "RV", "R" : "LS", "L" : "VP", "V" : "SR" }
confused = lambda p : random.choice(picks[p])

try :
    opn = sys.argv[-1][-1]
    print(confused(opn))    ### Kill me!
except Exception :
    print(totallyRandomStarterPick())