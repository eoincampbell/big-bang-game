import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Random;

public class Gazzr {

    private static final int DEPTH = 3;
    private final List<String> moves = Arrays.asList(new String[] { "R", "L", "V", "S", "P" });
    private final String[] beatIndex = new String[] { "PV", "RS", "PL", "RV", "LS" };
    private final List<String> loseFromIndex = Arrays.asList(new String[] { "LS", "PV", "RS", "PL", "RV" });
    private final Random random = new Random();

    public static void main(final String[] args) {
        System.out.println(new Gazzr().play(args));;
    }

    public String play(final String[] args) {
        if (args == null || args.length == 0 || args[0].length() < DEPTH) {
            return moves.get(random.nextInt(moves.size()));
        }
        final Map<String, float[]> oppHistDb = new HashMap<String, float[]>();
        final Map<String, float[]> myHistDb = new HashMap<String, float[]>();
        final char[] oppMoves = args[1].toCharArray();
        int i = DEPTH;
        while(i < args[0].length()) {
            updateDB(args[0].substring(i-DEPTH, i), oppMoves[i], myHistDb);
            updateDB(args[1].substring(i-DEPTH, i), oppMoves[i], oppHistDb);
            i++;
        }
        //Predict based on own and opponent moves:
        Integer p1 = predict(args[0].substring(i-DEPTH, i), myHistDb);
        Integer p2 = predict(args[1].substring(i-DEPTH, i), oppHistDb);
        if(p1 == null && p2 == null) {
            return moves.get(random.nextInt(moves.size()));
        }
        if(p2 == null) {
            return "" + beatIndex[p1].charAt(random.nextInt(2));
        } else if(p1 == null) {
            return "" + beatIndex[p2].charAt(random.nextInt(2));
        }
        String s1 = moves.get(p1)+moves.get(p2);
        String s2 = moves.get(p2)+moves.get(p1);
        if(loseFromIndex.contains(s1)) {
            return moves.get(loseFromIndex.indexOf(s1));
        }
        if(loseFromIndex.contains(s2)) {
            return moves.get(loseFromIndex.indexOf(s2));
        }
        if(random.nextBoolean()) {
            return ""+ beatIndex[p1].charAt(random.nextInt(2));
        } else {
            return ""+ beatIndex[p2].charAt(random.nextInt(2));
        }
    }

    private void updateDB(String lastMoves, char nextMove, Map<String, float[]> database) {
        int oppIndex = moves.indexOf("" + nextMove);
        float[] distr = database.get(lastMoves);
        if(distr == null) {
            distr = new float[5];
        }
        distr[oppIndex] += 1.0f;
        database.put(lastMoves, distr);
    }

    public Integer predict(String historicMoves, Map<String, float[]> database) {
        final float[] distr = database.get(historicMoves);
        if(distr == null) {
            return null;
        }
        return predictFromArray(distr);
    }

    private Integer predictFromArray(final float[] distr) {
        float total = 0f;
        for(int i = 0;i<distr.length;i++) {
            total += distr[i];
        }
        float target = total * random.nextFloat();
        total = 0f;
        for(int i = 0;i<distr.length;i++) {
            total += distr[i];
            if(total > target) {
                return i;
            }
        }
        return distr.length - 1;
    }
}