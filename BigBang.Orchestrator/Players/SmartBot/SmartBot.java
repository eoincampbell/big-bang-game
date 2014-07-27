import java.util.ArrayList;
public class SmartBot {
    public static void main(String[] args) {
        if(args.length ==0){
            System.out.print("L");
            return;
        }
        if(args[0].length()<3){
            String[] randLetter = new String[]{"R","P","S","L","V"};
            System.out.print(randLetter[(int) Math.floor(Math.random()*5)]);
            return;
        }
        String myHistory = args[0];
        String otherHistory = args[1];

        double rScore,pScore,sScore,lScore,vScore;//score - highest = highest probability of next opponent move
        rScore = pScore = sScore = lScore = vScore = 0;
        lScore = .001;
        ArrayList<ArrayList<Integer>> moveHits = new ArrayList<ArrayList<Integer>>();
        for(int g = 0;g<2;g++){
            for(int i=1;i<(myHistory.length() / 2) + 1;i++){
                if(g==0){
                    moveHits.add(findAll(myHistory.substring(myHistory.length() - i),myHistory));
                }
                else{
                    moveHits.add(findAll(otherHistory.substring(otherHistory.length() - i),otherHistory));
                }
            }
            for(int i = 0; i < moveHits.size();i++){
                int matchingMoves = i+1;
                ArrayList<Integer> moveIndexes = moveHits.get(i);
                for(Integer index:moveIndexes){
                    if(index+matchingMoves +1<= otherHistory.length()){
                        char nextMove = otherHistory.charAt(index + matchingMoves-1);
                        if(nextMove=='R'){rScore = rScore + matchingMoves;}
                        if(nextMove=='P'){pScore = pScore + matchingMoves;}
                        if(nextMove=='S'){sScore = sScore + matchingMoves;}
                        if(nextMove=='L'){lScore = lScore + matchingMoves;}
                        if(nextMove=='V'){vScore = vScore + matchingMoves;}
                    }
                }
            }
        }
        if(rScore >= pScore && rScore >= sScore && rScore >= lScore && rScore >= vScore){
            System.out.print("V");
            return;
        }
        if(pScore >= rScore && pScore >= sScore && pScore >= lScore && pScore >= vScore){
            System.out.print("L");
            return;
        }
        if(sScore >= pScore && sScore >= rScore && sScore >= lScore && sScore >= vScore){
            System.out.print("R");
            return;
        }
        if(vScore >= pScore && vScore >= sScore && vScore >= lScore && vScore >= rScore){
            System.out.print("L");
            return;
        }
        if(lScore >= pScore && lScore >= sScore && lScore >= rScore && lScore >= vScore){
            System.out.print("S");
        }
        return;
    }
    public static ArrayList<Integer> findAll(String substring,String realString){
        ArrayList<Integer> ocurrences = new ArrayList<Integer>();
        Integer index = realString.indexOf(substring);
        if(index==-1){return ocurrences;}
        ocurrences.add(index+1);
        while(index!=-1){
            index = realString.indexOf(substring,index + 1);
            if(index!=-1){
                ocurrences.add(index+1);
            }
        }
        return ocurrences;
    }
}