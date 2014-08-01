public class OneBehind {

    public static void main(String[] args) {
        if(args.length == 0) {
            System.out.print("L"); //Represent
            return;
        }
        char[] opp = args[1].toCharArray();
        int index = opp.length - 1;
        char choice = opp[index];
        switch(choice) {
            case 'R':
            System.out.print("R"); 
            break;
            case 'P':
            System.out.print("P");  
            break;
            case 'S':
            System.out.print("S");
            break;
            case 'L':
            System.out.print("L");
            break;
            case 'V':
            System.out.print("V");
            break;
        }
        return;
    }

}