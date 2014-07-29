import java.util.Random;

public class SelfHatingBot {

    static final Random RANDOM = new Random();

    private static char randomPlay() {

        switch (RANDOM.nextInt(5)) {

            case 0 : return 'R';

            case 1 : return 'P';

            case 2 : return 'S';

            case 3 : return 'L';

            default : return 'V';
        }
    }

    private static char antiPlay(String priorPlayString) {

        char[] priorPlays = priorPlayString.toCharArray();

        int choice = RANDOM.nextInt(2);

        switch (priorPlays[priorPlays.length - 1]) {

            case 'R' : return choice == 0 ? 'P' : 'V'; 

            case 'P' : return choice == 0 ? 'S' : 'L';

            case 'S' : return choice == 0 ? 'V' : 'R';

            case 'L' : return choice == 0 ? 'R' : 'S';

            default : return choice == 0 ? 'L' : 'P'; // V        
        }
    }

    public static void main(String[] args) {

        char play;

        if (args.length == 0) {

            play = randomPlay();

        } else {

            play = antiPlay(args[0]);
        }

        System.out.print(play);
        return;
    }
}