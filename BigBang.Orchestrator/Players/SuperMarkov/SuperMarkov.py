import pprint
import sys
import random

if len(sys.argv) < 2:
    our_hist = opp_hist = "x"
else:
    our_hist = 'x'+sys.argv[1]
    opp_hist = 'x'+sys.argv[2]

hist = zip(opp_hist, our_hist)

def build_chains(length):
    chains = {}
    for typ in ('ours', 'theirs', 'both'):
        chain = {}
        for i in xrange(0, len(hist)-length):
            if typ == 'ours':
                prev = our_hist[i:i+length]
            elif typ == 'theirs':
                prev = opp_hist[i:i+length]
            elif typ == 'both':
                prev = tuple(hist[i:i+length])
            else:
                raise ValueError()
            next = opp_hist[i+length]
            if next == 'x':
                continue

            chain.setdefault(prev, {})
            if next not in chain[prev]:
                chain[prev][next] = 1
            else:
                chain[prev][next] += 1

        #normalize the chain
        # for prev in chain:
        #     total = sum(v for v in chain[prev].values())
        #     for next in list(chain[prev]):
        #         chain[prev][next] /= float(total)

        chains[typ] = chain

    return chains

beats = {
    "S": "LP",
    "P": "VR",
    "R": "SL",
    "L": "VP",
    "V": "SR",
}
beaten_by = {
    "S": "VR",
    "P": "SL",
    "R": "PV",
    "L": "SR",
    "V": "PL",
}

weighted_evs = dict((c, 0) for c in "SPRLV")
chain_weights = {0: 1, 1: 2, 2: 4}
chain_type_weights = {'theirs': 1, 'ours': 1, 'both': 3}

for L in (0, 1, 2):
    chains = build_chains(L)
    for typ, chain in chains.items():
        if typ == 'ours':
            this_prev = our_hist[-L:] if L > 0 else ''
        elif typ == 'theirs':
            this_prev = opp_hist[-L:] if L > 0 else ''
        elif typ == 'both':
            this_prev = tuple(hist[-L:]) if L > 0 else ()

        this_nexts = chain.get(this_prev, {})
        #print typ, this_prev, this_nexts
        sample_size = sum(v for v in this_nexts.values())
        if sample_size == 0:
            continue

        this_weight = chain_weights[L] * chain_type_weights[typ]
        if sample_size < 4:
            this_weight /= 4.0 - sample_size

        for choice in "SVPLR":
            ev = 0
            for which, count in this_nexts.items():
                if which == choice:
                    # push
                    continue
                elif which in beats[choice]:
                    ev += float(count)/sample_size
                elif which in beaten_by[choice]:
                    ev -= float(count)/sample_size
                else:
                    raise ValueError()
            #print "According to %s of length %d, picking %s gives EV %.3f" % (typ, L, choice, ev)
            weighted_evs[choice] += ev * this_weight

#pprint.pprint(weighted_evs)
print max("SPRLV", key=lambda choice: weighted_evs[choice])