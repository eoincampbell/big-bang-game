public class BoringBot
{
    public static void main(String[] args)
    {
        int Rock=0;
        int Paper=0;
        int Scissors=0;
        int Lizard=0;
        int Spock=0;

        if (args.length == 0)
        {
            System.out.print("P");
            return;
        }

        char[] oppPreviousPlays = args[1].toCharArray();

        for (int j=0; j<oppPreviousPlays.length; j++) {
            switch(oppPreviousPlays[j]){
                case 'R': Rock++; break;
                case 'P': Paper++; break;
                case 'S': Scissors++; break;
                case 'L': Lizard++; break;
                case 'V': Spock++;
            }
        }

        int Best = Math.max(Math.max(Lizard+Scissors-Spock-Paper,
                                     Rock+Spock-Lizard-Scissors),
                            Math.max(Math.max(Paper+Lizard-Spock-Rock,
                                              Paper+Spock-Rock-Scissors),
                                     Rock+Scissors-Paper-Lizard));

        if (Best== Lizard+Scissors-Spock-Paper){
            System.out.print("R"); return;
        } else if (Best== Rock+Spock-Lizard-Scissors){
            System.out.print("P"); return;
        } else if (Best== Paper+Lizard-Spock-Rock){
            System.out.print("S"); return;
        } else if(Best== Paper+Spock-Rock-Scissors){
            System.out.print("L"); return;
        } else {
            System.out.print("V"); return;
        }
    }
}