public class DogeBot {
     public static void main(String []args){

        if (args.length < 2) {
            System.out.println("P");
            return;
        }

        String yourHistory = args[1];  

        if (yourHistory.length() < 2) {
            System.out.println("P");
            return;
        }    

        String possibleHands = "RPSLV";

        for (int i = 0; i < 5; i++) {
            String searchString = "" + possibleHands.charAt(i) + possibleHands.charAt(i);

            if (yourHistory.contains(searchString)) {

                char newHand = possibleHands.charAt((i + 2) % 5);
                System.out.println(newHand == 'S' ? "P" : newHand);

                return;
            } else continue;

        }

        char lastHistoryHand = yourHistory.charAt(yourHistory.length() - 1);
        int i = possibleHands.indexOf(lastHistoryHand);

        char newHand = possibleHands.charAt((i + 2) % 5);
        System.out.println(newHand == 'S' ? "P" : newHand);

        return;
    }
}