import java.util.HashMap;
public class ToddlerProof
{
    public static void main(String[] args)
    {
        if(args.length<1) //first Round
        {
            System.out.print('V');//Spock is best
            return;
        }
        else
        {
            String them = args[1];
            String me = args[0];
            int streak = 0;

            HashMap<Character, Character> nextMove = new HashMap<Character, Character>();
            //Next move beats things that beat my last move
            nextMove.put('L', 'V');
            nextMove.put('V', 'S');
            nextMove.put('S', 'P');
            nextMove.put('P', 'R');
            nextMove.put('R', 'L');
            //Check if last round was a tie or the opponent beat me
            int lastResult = winner(me.charAt(me.length()-1), them.charAt(them.length()-1));
            if(lastResult == 0)
            {
                //tie, so they will chase my last throw
                System.out.print(nextMove.get(me.charAt(me.length()-1)));

                return;
            }
            else if(lastResult == 1)
            {
                //I won, so they will chase my last throw
                System.out.print(nextMove.get(me.charAt(me.length()-1)));


                return;
            }

            else{
                //I lost
                //find streak
                for(int i = 0; i<me.length(); i++)
                {
                    int a = winner(me.charAt(i), them.charAt(i));
                    if(a >= 0) streak = 0;
                    else streak++;
                }
                //check lossStreak
                if(streak>5)
                {
                    //if they are on to me, do something random-ish
                    int r = (((them.length()+me.length()-1)*13)/7)%4;
                    System.out.print(nextMove.get(r));
                    return;
                }
                //otherwise, go on with the plan
                System.out.print(nextMove.get(me.charAt(me.length()-1)));
                return;
            }
        }
    }
    public static int winner(char me, char them)
    {
        //check for tie
        if(me == them) return 0;
        //check if they won
        if(me=='V' && (them == 'L' || them == 'P')) return -1;
        if(me=='S' && (them == 'V' || them == 'R')) return -1;
        if(me=='P' && (them == 'S' || them == 'L')) return -1;
        if(me=='R' && (them == 'P' || them == 'V')) return -1;
        if(me=='L' && (them == 'R' || them == 'S')) return -1;
        //otherwise, I won
        return 1;
    }
}