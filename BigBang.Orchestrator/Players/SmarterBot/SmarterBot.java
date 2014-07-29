import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;

public class SmarterBot {

	public static void main(String[] args){
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
		String myUsableHistory = myHistory;
		String otherUsableHistory = otherHistory;
		if(myHistory.length() > 4){
			int startIndex = findLastInstanceOfXLosses(myHistory,otherHistory,3);
			myUsableHistory = myHistory.substring(startIndex);
			otherUsableHistory = otherHistory.substring(startIndex);
		}

		if(lostLastXRounds(myHistory,otherHistory) >= 1){
			String[] randLetter = new String[]{"R","P","S","L","V"};
			System.out.print(randLetter[(int) Math.floor(Math.random()*5)]);
			return;
		}

		ArrayList<ArrayList<Integer>> moveHits = new ArrayList<ArrayList<Integer>>();
		for(int g = 0;g<2;g++){
			for(int i=1;i<(myUsableHistory.length() / 2) + 1;i++){
				if(g==0){
					moveHits.add(findAll(myUsableHistory.substring(myUsableHistory.length() - i),myUsableHistory));
				}
				else{
					moveHits.add(findAll(otherUsableHistory.substring(otherUsableHistory.length() - i),otherUsableHistory));
				}
			}
			for(int i = 0; i < moveHits.size();i++){
				int matchingMoves = i+1;
				ArrayList<Integer> moveIndexes = moveHits.get(i);
				for(Integer index:moveIndexes){
					if(index+matchingMoves +1<= otherUsableHistory.length()){
						char nextMove = otherUsableHistory.charAt(index + matchingMoves-1);
						if(nextMove=='R'){rScore = rScore + matchingMoves;}
						if(nextMove=='P'){pScore = pScore + matchingMoves;}
						if(nextMove=='S'){sScore = sScore + matchingMoves;}
						if(nextMove=='L'){lScore = lScore + matchingMoves;}
						if(nextMove=='V'){vScore = vScore + matchingMoves;}
					}
				}
			}
		}

		HashMap<Character,Double> scores = new HashMap<Character,Double>();
		scores.put('R', rScore);
		scores.put('P',pScore);
		scores.put('S', sScore);
		scores.put('L', lScore);
		scores.put('V', vScore);
		ArrayList<Character> winners = orderHashMap(scores);
		char number1 = winners.get(winners.size() - 1);
		char number2 = winners.get(winners.size() - 2);
		char charToPlay = beatsMove(number1,number2);
		System.out.print(charToPlay);
		return;
	}
	public static int findLastInstanceOfXLosses(String myHistory,String otherHistory, int losses){
		int index = myHistory.length();
		while(lostLastXRounds(myHistory.substring(0,index),otherHistory.substring(0,index)) < losses && index >1){
			index--;
		}
		return index;
	}
	public static ArrayList<Character> orderHashMap(HashMap<Character,Double> scores){
		ArrayList<Double> orderedScores = new ArrayList<Double>();
		orderedScores.add(scores.get('R'));
		orderedScores.add(scores.get('P'));
		orderedScores.add(scores.get('S'));
		orderedScores.add(scores.get('L'));
		orderedScores.add(scores.get('V'));
		Collections.sort(orderedScores);

		ArrayList<Character> orderedKeys = new ArrayList<Character>();
		for(Double value:orderedScores){
			for(Character key:scores.keySet()){
				if(value.equals(scores.get(key))){
					orderedKeys.add(key);
				}
			}
		}
		return orderedKeys;
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
	public static int lostLastXRounds(String myHistory,String otherHistory){
		int index = myHistory.length()-1;
		int lost = 0;
		while(ifCharBeatsOther(otherHistory.charAt(index),myHistory.charAt(index)) == true){
			index--;
			lost++;
		}
		return lost;
	}
	public static boolean ifCharBeatsOther(char char1,char char2){
		ArrayList<Character> beatsRock = new ArrayList<Character>();
		beatsRock.add('V');
		beatsRock.add('P');
		ArrayList<Character> beatsPaper = new ArrayList<Character>();
		beatsPaper.add('S');
		beatsPaper.add('L');
		ArrayList<Character> beatsSissors = new ArrayList<Character>();
		beatsSissors.add('V');
		beatsSissors.add('R');
		ArrayList<Character> beatsLizard = new ArrayList<Character>();
		beatsLizard.add('S');
		beatsLizard.add('R');
		ArrayList<Character> beatsSpock = new ArrayList<Character>();
		beatsSpock.add('P');
		beatsSpock.add('L');

		if(char2 == 'V'){if (beatsSpock.contains(char1)){return true;}}
		if(char2 == 'R'){if (beatsRock.contains(char1)){return true;}}
		if(char2 == 'P'){if (beatsPaper.contains(char1)){return true;}}
		if(char2 == 'S'){if (beatsSissors.contains(char1)){return true;}}
		if(char2 == 'L'){if (beatsLizard.contains(char1)){return true;}}
		return false;
	}
	public static char beatsMove(char moveToBeBeaten,char runnerUp){
		if(moveToBeBeaten == 'R'){
			if(runnerUp == 'S'){
				return 'V';
			}
			if(runnerUp == 'V'){
				return 'P';
			}
			return 'V';
		}
		if(moveToBeBeaten == 'P'){
			if(runnerUp == 'L'){
				return 'S';
			}
			if(runnerUp == 'V'){
				return 'L';
			}
			return 'L';
		}
		if(moveToBeBeaten == 'S'){
			if(runnerUp == 'R'){
				return 'V';
			}
			if(runnerUp =='L'){
				return 'R';
			}
			return 'V';
		}
		if(moveToBeBeaten == 'L'){
			if(runnerUp == 'P'){
				return 'S';
			}
			if(runnerUp == 'S'){
				return 'R';
			}
			return 'S';
		}
		if(moveToBeBeaten == 'V'){
			if(runnerUp == 'R'){
				return 'P';
			}
			if(runnerUp == 'P'){
				return 'L';
			}
		}
		return 'L';
	}
}