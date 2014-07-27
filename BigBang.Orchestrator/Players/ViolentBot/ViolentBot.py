import sys

choice_dict = {"L" : "S", "P" : "S", "R" : "V", "S" : "V", "V" : "L"}

total = len(sys.argv)
if total==1:
    print("L")
    sys.exit()

opponent = sys.argv[2]
opponent_last = opponent[-1]

print(choice_dict[opponent_last])