import sys, random
choices = ['L','V','S','P','R'] * 20
if len(sys.argv) > 1:
    my_history = sys.argv[1]
    [choices.remove(my_history[-1]) for i in range(15)]
print(choices[random.randrange(len(choices))])