import random
import sys


def pickRand():
    plays = ['R', 'P', 'S', 'L', 'V']
    return random.choice(plays)

def beat(me, them):
    if me==them:
        return 0
    if(me=='V' and (them == 'L' or them == 'P')):
        return -1
    if(me=='S' and (them == 'V' or them == 'R')):
        return -1
    if(me=='P' and (them == 'S' or them == 'L')):
        return -1
    if(me=='R' and (them == 'P' or them == 'V')):
        return -1
    if(me=='L' and (them == 'R' or them == 'S')):
        return -1
    return 1

if len(sys.argv) > 1:
    if beat((sys.argv[1])[-1], (sys.argv[2])[-1]) > 0:
        print(sys.argv[1][-1])

    else:
        print(pickRand())
else:
    print(pickRand())