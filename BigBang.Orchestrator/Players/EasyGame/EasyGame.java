public class EasyGame {

    public static String randomLetter(){
        String[] randomLetters = new String[]{"R","P","S","L","V"};
        String character = (randomLetters[(int)Math.floor(Math.random()*5)]);
        return character;
    }

    public static int[] calculateScores(int[] scores, char[] enemyPlays){
        for (int i=0; i != enemyPlays.length; i++){
            if (enemyPlays[i] == 'R') scores[0]++;
            if (enemyPlays[i] == 'P') scores[1]++;
            if (enemyPlays[i] == 'S') scores[2]++;
            if (enemyPlays[i] == 'L') scores[3]++;
            if (enemyPlays[i] == 'V') scores[4]++;
        }
        return scores;
    }

    public static boolean randomPick(int chances, int size){
        if (chances >= (int)Math.floor(Math.random()*size))
            return true;
        return false;       
    }

    public static char nextMove(int[] seed, int length){
        if (randomPick(seed[0], length)) return 'P';
        if (randomPick(seed[1], length-(seed[0]))) return 'S';
        if (randomPick(seed[2], length-(seed[0]+seed[1]))) return 'V';
        if (randomPick(seed[3], length-(seed[0]+seed[1]+seed[2]))) return 'R';
        if (randomPick(seed[4], length-(seed[0]+seed[1]+seed[2]+seed[3]))) return 'L';
        return 'V';
    }

    public static void main(String[] args) {

        int[] scores = new int[]{0,0,0,0,0};
        if(args.length == 0 || args[1].length() < 10){
            System.out.print(randomLetter());
            return;
        }

        char[] enemyPlays = args[1].toCharArray();
        scores = calculateScores(scores, enemyPlays);
        System.out.print(nextMove(scores, args[1].length()));
        return;
    }
}