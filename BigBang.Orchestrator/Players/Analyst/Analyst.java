import java.util.Random;

public class Analyst{
    public static void main(String[] args){
        char action = 'S';

        try{
            char[] enemyMoves = null, myMoves = null;

            //first move is random
            if(args.length == 0){
                System.out.print(randomMove());
                System.exit(0);
            //moves 2-3 will beat their last move
            }else if(args[0].length() < 8){
                System.out.print(counterFor(args[1].charAt(args[1].length()-1)));
                System.exit(0);
            //following moves will execute some analyzation stuff
            }else{
                //get previous moves
                myMoves = args[0].toCharArray();
                enemyMoves = args[1].toCharArray();
            }

            //test if they're trying to beat our last move
            if(beats(enemyMoves[enemyMoves.length-1], myMoves[myMoves.length-2])){
                action = counterFor(counterFor(myMoves[myMoves.length-1]));
            }
            //test if they're copying our last move
            else if(enemyMoves[enemyMoves.length-1] == myMoves[myMoves.length-2]){
                action = counterFor(myMoves[myMoves.length-1]);
            }
            //else beat whatever they've done the most of
            else{
                action = counterFor(countMost(enemyMoves));
            }

            //if they've beaten us for the first 40 moves, do the opposite of what ive been doing
            if(theyreSmarter(myMoves, enemyMoves)){
                action = counterFor(action);
            }

        //if you break my program do something random
        }catch (Exception e){
            action = randomMove();
        }

        System.out.print(action);
    }

    private static char randomMove(){
        Random rand = new Random(System.currentTimeMillis());
        int randomMove = rand.nextInt(5);

        switch (randomMove){
            case 0: return 'R';
            case 1: return 'P';
            case 2: return 'S';
            case 3: return 'L';
            default: return 'V';
        }
    }

    private static char counterFor(char move){
        Random rand = new Random(System.currentTimeMillis());
        int moveSet = rand.nextInt(2);

        if(moveSet == 0){
            switch (move){
                case 'R': return 'P'; 
                case 'P': return 'S'; 
                case 'S': return 'R'; 
                case 'L': return 'R'; 
                default: return 'P';
            }
        }else{
            switch (move){
                case 'R': return 'V'; 
                case 'P': return 'L'; 
                case 'S': return 'V'; 
                case 'L': return 'S'; 
                default: return 'L';
            }
        }
    }

    private static boolean beats(char move1, char move2){
        if(move1 == 'R'){
            if((move2 == 'S') || (move2 == 'L')){
                return true;
            }else{
                return false;
            }
        }else if(move1 == 'P'){
            if((move2 == 'R') || (move2 == 'V')){
                return true;
            }else{
                return false;
            }
        }else if(move1 == 'S'){
            if((move2 == 'L') || (move2 == 'P')){
                return true;
            }else{
                return false;
            }
        }else if(move1 == 'L'){
            if((move2 == 'P') || (move2 == 'V')){
                return true;
            }else{
                return false;
            }
        }else{
            if((move2 == 'R') || (move2 == 'S')){
                return true;
            }else{
                return false;
            }
        }
    }

    private static char countMost(char[] moves){
        int[] enemyMoveList = {0,0,0,0,0};

        for(int i=0; i<moves.length; i++){
            if(moves[i] == 'R'){
                enemyMoveList[0]++;
            }else if(moves[i] == 'P'){
                enemyMoveList[1]++;
            }else if(moves[i] == 'S'){
                enemyMoveList[2]++;
            }else if(moves[i] == 'L'){
                enemyMoveList[3]++;
            }else if(moves[i] == 'V'){
                enemyMoveList[4]++;
            }
        }

        int max = 0, maxIndex = 0;
        for(int i=0; i<5; i++){
            if(enemyMoveList[i] > max){
                max = enemyMoveList[i];
                maxIndex = i;
            }
        }

        switch (maxIndex){
            case 0: return 'R';
            case 1: return 'P';
            case 2: return 'S';
            case 3: return 'L';
            default: return 'V';
        }
    }

    private static boolean theyreSmarter(char[] myMoves, char[] enemyMoves){
        int loseCounter = 0;

        if(enemyMoves.length >= 40){
            for(int i=0; i<40; i++){
                if(beats(enemyMoves[i],myMoves[i])){
                    loseCounter++;
                }
            }
        }else{
            return false;
        }

        if(loseCounter > 20){
            return true;
        }else{
            return false;
        }
    }
}