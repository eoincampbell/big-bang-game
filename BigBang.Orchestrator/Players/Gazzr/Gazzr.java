import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Random;

public class Gazzr { // <- Tries to guess

    private static final int DEPTH = 2; // <- amount of moves analysed
    private final List<String> moves = Arrays.asList(new String[] { "R", "L", "V", "S", "P" });
    private final String[] beats = new String[] { "LS", "PV", "RS", "PL", "RV" };
    private final List<String> losesFrom = Arrays.asList(new String[] { "PV", "RS", "PL", "RV", "LS" });
    private final Random random = new Random();

    public static void main(final String[] args) {
        System.out.println(new Gazzr().play(args));
    }

    private String play(final String[] args) {
        if (args == null || args.length == 0 || args[0].length() < DEPTH) {
            return moves.get(random.nextInt(moves.size()));
        }
        final Map<String, int[]> database = new HashMap<String, int[]>();
        final char[] myMoves = args[0].toCharArray();
        final char[] oppMoves = args[1].toCharArray();

        int score = 0;
        boolean losing = false;
        int playMode = 0;
        final StringBuilder history = new StringBuilder();
        for (int i = 0; i < myMoves.length; i++) {
            final int myIndex = moves.indexOf("" + myMoves[i]);
            final int oppIndex = moves.indexOf("" + oppMoves[i]);

            String predictedMove = predict(history, database, playMode);

            if(predictedMove != null) {
                if (i > DEPTH && myMoves[i] != oppMoves[i]) {
                    final String beatingMoves = beats[myIndex];
                    if (beatingMoves.contains(oppMoves[i] + "")) {
                        score--;
                    } else {
                        score++;
                    }
                }
                if(!losing && score < -2) {
                    losing = true;
                    playMode++;
                } else if(losing && score > 0) {
                    losing = false;
                }
            }

            //Update database:
            if(history.length() == DEPTH) {
                final String lookup = history.toString();
                int[] distr = database.get(lookup);
                if(distr == null) {
                    distr = new int[5];
                }
                distr[oppIndex]++;
                database.put(lookup, distr);
                history.delete(0, 1);
            }
            history.append(""+oppIndex);

        }

        String move = predict(history, database, playMode);
        if(move != null) {
            return move;
        } else {
            return moves.get(random.nextInt(moves.size()));
        }
    }

    public String predict(StringBuilder history, Map<String, int[]> database, int playMode) {
        final String historicMoves = history.toString();
        final int[] distr = database.get(historicMoves);

        if(distr == null) {
            return null;
        }
        int bestIndex = 0;
        for(int i = 1;i<distr.length;i++) {
            if(distr[i] > distr[bestIndex]) {
                bestIndex = i;
            }
        }
        //Play move:
        if(playMode%2 == 0) {
            return "" + beats[bestIndex].charAt(random.nextInt(2));
        } else {
            //He's on to me, try to iocaine!
            String illPick = beats[bestIndex];
            int opponentMove = losesFrom.indexOf(illPick);
            return "" + beats[opponentMove].charAt(random.nextInt(2));
        }
    }
}