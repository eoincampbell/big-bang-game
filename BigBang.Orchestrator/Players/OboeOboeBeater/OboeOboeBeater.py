import sys

game = "VLVSLLVSRPLPPRLPRSRSSPRSLPPVPPVSSSVRPSPSPRLPLPRRSPVVLRRSPLPVSPLLSLPPVRSSRRVRVSLLRVLVPSVSLRVVPVSPRVVL"

try:
    round = len(sys.argv[1])
except IndexError:
    round = 0

print game[round]