public class Botzinga {

private static int curTurn = 0;
private static char[] opHand;
private static char[] myHand;

public static void main(String[] args) {

    if(args.length != 0) {
        int curTurn = args[0].length();
        opHand = args[1].toCharArray();
        myHand = args[0].toCharArray();
    }
    char c = ' ';

    if(curTurn >= 5) {
        if(opHand[curTurn-1] == opHand[curTurn-2] && opHand[curTurn-1] == opHand[curTurn-3])
            c = beater(opHand[curTurn - 1]);
        else {
            if (beats(myHand[curTurn - 1], opHand[curTurn - 1]))
                c = beater(myHand[curTurn - 1]);
            else
                c = beater(opHand[curTurn - 1]);
        }

    }
    else
        c = playRandom();
    System.out.print(c);

}


public static char playRandom() {
    char c = ' ';
    switch((int)(Math.random()*5)) {
        case 0: c = 'R'; break;
        case 1: c = 'P'; break;
        case 2: c = 'S'; break;
        case 3: c = 'L'; break;
        case 4: c = 'V'; break;
    }
    return c;
}

private static char beater(char a) {
    int Rock=0;
    int Paper=0;
    int Scissors=0;
    int Lizard=0;
    int Spock=0;

    for (int j=0; j<curTurn; j++) {
        switch(myHand[j]){
            case 'R': Rock++; break;
            case 'P': Paper++; break;
            case 'S': Scissors++; break;
            case 'L': Lizard++; break;
            case 'V': Spock++;
        }
    }

    switch (a) {
        case 'R': return Paper < Spock ? 'P' : 'V';
        case 'P': return Scissors < Lizard ? 'S' : 'L';
        case 'S': return Rock < Spock ? 'R' : 'V';
        case 'V': return Paper < Lizard ? 'P' : 'L';
        case 'L': return Rock < Scissors ? 'R' : 'S';
        default: return ' ';
    }

}

private static boolean beats(char a, char b) {
    boolean win = false;
    switch (a) {
        case 'R':
            win = b == 'S' || b == 'L';
            break;
        case 'P':
            win = b == 'R' || b == 'V';
            break;
        case 'S':
            win = b == 'P' || b == 'L';
            break;
        case 'V':
            win = b == 'S' || b == 'R';
            break;
        case 'L':
            win = b == 'V' || b == 'P';
            break;
    }
    return win;
}
}