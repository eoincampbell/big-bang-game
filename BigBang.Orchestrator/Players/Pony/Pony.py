import sys

# just play Spock for the first two rounds
if len(sys.argv)<2 or len(sys.argv[1])<2: print 'V'; sys.exit()

# initialize and translate moves to numbers for better handling:
my_moves, opp_moves = sys.argv[1], sys.argv[2]
moves = ('R', 'P', 'S', 'V', 'L')   
history = zip([moves.index(i) for i in my_moves],
              [moves.index(i) for i in opp_moves])

# predict possible next moves based on history
def prediction(hist):
    N = len(hist)    

    # find longest match of the preceding moves in the earlier history
    cand_m = cand_o = cand_b = range(N-1)
    for l in xrange(1,min(N, 20)):
        ref = hist[N-l]
        cand_m = ([c for c in cand_m if c>=l and hist[c-l+1][0]==ref[0]]
                  or cand_m[-1:])
        cand_o = ([c for c in cand_o if c>=l and hist[c-l+1][1]==ref[1]]
                  or cand_o[-1:])
        cand_b = ([c for c in cand_b if c>=l and hist[c-l+1]==ref]
                  or cand_b[-1:])

    # analyze which moves were used how often
    freq_m, freq_o = [0]*5, [0]*5
    for m in hist:
        freq_m[m[0]] += 1
        freq_o[m[1]] += 1

    # return predictions
    return ([hist[-i][p] for i in 1,2 for p in 0,1]+   # repeat last moves
            [hist[cand_m[-1]+1][0],     # history matching of my own moves
             hist[cand_o[-1]+1][1],     # history matching of opponent's moves
             hist[cand_b[-1]+1][0],     # history matching of both
             hist[cand_b[-1]+1][1],
             freq_m.index(max(freq_m)), # my most frequent move
             freq_o.index(max(freq_o)), # opponent's most frequent move
             0])                        # good old rock (and friends)


# what would have been predicted in the last rounds?
pred_hist = [prediction(history[:i]) for i in xrange(2,len(history)+1)]

# how would the different predictions have scored?
n_pred = len(pred_hist[0])
scores = [[0]*5 for i in xrange(n_pred)]
for pred, real in zip(pred_hist[:-1], history[2:]):
    for i in xrange(n_pred):
        scores[i][(real[1]-pred[i]+1)%5] += 1
        scores[i][(real[1]-pred[i]+3)%5] += 1
        scores[i][(real[1]-pred[i]+2)%5] -= 1
        scores[i][(real[1]-pred[i]+4)%5] -= 1

# return best counter move
best_scores = [list(max(enumerate(s), key=lambda x: x[1])) for s in scores]
best_scores[-1][1] *= 1.001   # bias towards the simplest strategy    
if best_scores[-1][1]<0.4*len(history): best_scores[-1][1] *= 1.4
strat, (shift, score) = max(enumerate(best_scores), key=lambda x: x[1][1])
print moves[(pred_hist[-1][strat]+shift)%5]