import sys
import random

MODE = 1

moves = 'RSLPV'

def element_sums(a, b):
    return [a[i] + b[i] for i in xrange(len(a))]

def move_scores(p):
    def calc(to_beat):
        return ['LDW'.find('DLLWW'[moves.find(m)-moves.find(to_beat)]) for m in moves]

    return dict(zip(moves, element_sums(calc(p[0]), calc(p[1]))))

def move_chooser(my_history, opponent_history):
    predict = sorted(moves, key=opponent_history.count, reverse=MODE)[-2:]
    scores = move_scores(predict)
    return max(scores, key=lambda k:scores[k])

if __name__ == '__main__':
    if len(sys.argv) == 3:
        print move_chooser(*sys.argv[1:])
    elif len(sys.argv) == 1:
        print random.choice(moves)