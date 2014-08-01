import sys
if len(sys.argv)<2 or len(sys.argv[1])<2: print 'L'; sys.exit()
hist = [map('RPSVL'.index, p) for p in zip(sys.argv[1], sys.argv[2])]
N = len(hist)
cand = range(N-1)
for l in xrange(1,N):
    cand = ([c for c in cand if c>=l and hist[c-l+1]==hist[N-l]] or cand[-1:])
print 'RPSVL'[(hist[cand[-1]+1][1]+(1,3)[N%2==0])%5]