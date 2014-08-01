import random
import sys

hands = "RPSVL"

def outcome(h1, h2):
    dist = (hands.index(h1)-hands.index(h2)) % 5
    if dist==0:
        return 0
    if dist & 1:
        return 1
    return -1

class EM():
    def __init__(self, predictor):
        self.predictor = predictor

    def hand(self, ours="", theirs=""):
        probs = self.predictor.probs(ours, theirs)

        if not(probs):
            return random.choice(hands)

        expectation = { our : sum([outcome(our, their) * probs[their] for their in hands]) for our in hands }
        winning = max(expectation.values())
        candidates = filter(lambda h: expectation[h]==winning, expectation.keys())
        return random.choice(candidates)

class Predictor:
    def __init__(self):
        self.fnum = 7

    def extract(self, ours, theirs, idx):
        if idx < 2:
            return None

        f1 = ours[idx-1]
        f2 = theirs[idx-1]
        f3 = (f1, f2)
        f4 = (f1, (f1 == ours[idx-2]))
        f5 = (f2, (f2 == theirs[idx-2]))
        f6 = (f1, ours[idx-2])
        f7 = (f2, theirs[idx-2])

        return [f1,f2,f3,f4,f5,f6,f7]

    def probs(self, ours, theirs):
        if not(theirs):
            return None

        idx = 0

        fcount = []
        flcount = []
        for i in range(0, self.fnum):
            fcount.append({})
            flcount.append({})

        lcount = {}
        samples = 0

        while idx < len(theirs):
            label = theirs[idx]

            features = self.extract(ours, theirs, idx)
            if not(features):
                idx +=1
                continue

            samples += 1 

            if not(label in lcount):
                lcount[label] = 0

                for feature in flcount:
                    feature[label] = {}

            lcount[label] += 1

            for fi in range(0, self.fnum):
                realz = features[fi]
                fcount_i = fcount[fi]

                if not(realz in fcount_i):
                    fcount_i[realz] = 0

                fcount_i[realz] += 1

                flcount_il = flcount[fi][label]
                if not(realz in flcount_il):
                    flcount_il[realz] = 0

                flcount_il[realz] += 1

            idx+=1

        if not(samples):
            return None

        lprob = {label: float(c) / samples for (label, c) in lcount.items() }
        flprob = [ { label : { realz : float(c) / sum(realizations.values()) for realz, c in realizations.items()} for label, realizations in feature.items()} for feature in flcount ]

        features = self.extract(ours, theirs, idx) 

        probs = {}
        for label in hands:
            if not(label in lprob):
                probs[label] = 0
                continue

            prob = lprob[label]
            for fi in range(0, self.fnum):
                realz = features[fi]
                flprob_il = flprob[fi][label]
                if realz in flprob_il:
                    prob *= flprob_il[realz]
                elif flprob_il:
                    prob = 0
                    break

            probs[label] = prob

        return probs

bot = EM(Predictor())

if len(sys.argv) == 3:
    move = bot.hand(sys.argv[1], sys.argv[2])
else:
    move = bot.hand()

sys.stdout.write(move)