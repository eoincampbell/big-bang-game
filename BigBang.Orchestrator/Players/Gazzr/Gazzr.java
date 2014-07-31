import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Random;

public class Gazzr { // <- Tries to guess

    private static final int DEPTH = 3; // <- amount of moves analysed
    private final List<String> moves = Arrays.asList(new String[] { "R", "L", "V", "S", "P" });
    private final String[] beats = new String[] { "LS", "PV", "RS", "PL", "RV" };
    private final Random random = new Random();

    public static void main(final String[] args) {
        System.out.println(new Gazzr().play(args));
    }

    String play(final String[] args) {
        if (args == null || args.length == 0 || args[0].length() < DEPTH) {
            return moves.get(random.nextInt(moves.size()));
        }
        final Map<String, int[]> database = new HashMap<String, int[]>();
        final char[] myMoves = args[0].toCharArray();
        final char[] oppMoves = args[1].toCharArray();
        final StringBuilder history = new StringBuilder();
        for (int i = 0; i < myMoves.length; i++) {
            final int oppIndex = moves.indexOf("" + oppMoves[i]);

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

        String move = predict(history, database);
        if(move != null) {
            return move;
        } else {
            return moves.get(random.nextInt(moves.size()));
        }
    }

    public String predict(StringBuilder history, Map<String, int[]> database) {
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
        return "" + beats[bestIndex].charAt(random.nextInt(2));
    }
}