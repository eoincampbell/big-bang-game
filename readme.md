Question from codegolf.stackexchange.com

http://codegolf.stackexchange.com/questions/35079/the-rock-paper-scissors-lizard-spock-tournament-of-epicness


#Most Recent Leaderboard @ 2014-07-27
 

    | #   | Author               | Name                 | Score      | Avg. Time       |
    |-----+----------------------+----------------------+------------+-----------------+
    | 01  | Martin Buttner       | MarkovBot            | 21         | 39.89 ms        |
    | 02T | kaine                | BoringBot            | 18         | 66.89 ms        |
    | 02T | Martin Buttner       | SlowLizard           | 18         | 39.08 ms        |
    | 04T | Thaylon              | NitPicker            | 17         | 47.28 ms        |
    | 04T | Kyle Kanos           | ViolentBot           | 17         | 33.05 ms        |
    | 04T | bitpwner             | AlgorithmBot         | 17         | 25.59 ms        |
    | 07  | Stretch Maniac       | SmartBot             | 16         | 68.89 ms        |
    | 08  | Trimsty              | Herpetologist        | 15         | 37.44 ms        |
    | 09T | EoinC                | SimpleRandomBot      | 14         | 14.57 ms        |
    | 09T | Mikey Mouse          | LizardsRule          | 14         | 14.73 ms        |
    | 09T | Emil                 | Pony                 | 14         | 26.21 ms        |
    | 12T | HuddleWolf           | HuddleWolfHatesBi... | 13         | 25.34 ms        |
    | 12T | Martin Buttner       | FairBot              | 13         | 39.60 ms        |
    | 12T | histocrat            | WereVulcan           | 13         | 39.48 ms        |
    | 12T | Kyle Kanos           | LexicographicBot     | 13         | 37.63 ms        |
    | 12T | Claudiu              | SuperMarkov          | 13         | 27.14 ms        |
    | 12T | PhiNotPi             | BayesianBot          | 13         | 09.41 ms        |
    | 18  | killmous             | MAWBRBot             | 12         | 16.81 ms        |
    | 19T | Martin Buttner       | ConservativeBot      | 11         | 38.57 ms        |
    | 19T | Martin Buttner       | Vulcan               | 11         | 38.63 ms        |
    | 19T | DrJPepper            | MonadBot             | 11         | 08.28 ms        |
    | 22  | ArcticanAudio        | SpockOrRockBot       | 10         | 14.73 ms        |
    | 23T | ProgramFOX           | Echo                 | 9          | 14.41 ms        |
    | 23T | Stranjyr             | ToddlerProof         | 9          | 67.89 ms        |
    | 23T | William Barbosa      | StarWarsFan          | 9          | 38.43 ms        |
    | 26T | Timmy                | DynamicBot           | 8          | 37.94 ms        |
    | 26T | William Barbosa      | BarneyStinson        | 8          | 05.19 ms        |
    | 28  | undergroundmonorail  | TheGambler           | 7          | 25.75 ms        |

**Notes**

28 Players now requires 378 games (75600 hands) so running the tourney is getting slow. Looking at ways to paralellize it.

**Broken Bots**

 - Analogizer - strange bug when playing java players... investigating
 - Randomly Weighted - waiting bug fix

**Awaiting Techsupport**

 - BashRocks - needs cygwin/bug fix
 - YaarBot - needs google v8 engine - not having much joy with this

*Next Tournament will try add support for the above*


#Original Posted Question

You've swung around to your friends house for the most epic showdown Battle ever of Rock, Paper, Scissors, Lizard, Spock. In true BigBang nerd-tastic style, none of the players are playing themselves but have created console bots to play on their behalf. You whip out your USB key and hand it over to the **Sheldor the Conqueror** for inclusion in the showdown. Penny swoons. Or perhaps Howard swoons. We don't judge here at Leonard's apartment.


**Rules**

Standard Rock, Paper, Scissors, Lizard, Spock rules apply.

 - Scissors cut Paper
 - Paper covers Rock
 - Rock crushes Lizard
 - Lizard poisons Spock
 - Spock smashes Scissors
 - Scissors decapitate Lizard
 - Lizard eats Paper
 - Paper disproves Spock
 - Spock vaporizes Rock
 - Rock crushes Scissors

![RPSLV Rules][1]
 
Each player's bot will play one *Match* against each other bot in the tournament.

Each Match will consist of 100 iterations of an RPSLV game.

After each match, the winner is the player who has one the most number of games/hands out of 100.

If you win a match, you will be assigned 1 point in the league table. In the result of a draw-match, neither player will gain a point.


**Bot Requirement**

Your bot must be runnable from the command line.

Sheldor's *nix box has died, so we're running it off his windows 8 Gaming Laptop so make sure your provided solution can run on windows. Sheldor has graciously offered to install any required runtimes (within reason) to be able to run your solution. (.NET, Java, Php, Python, Ruby, Powershell ...)

**Inputs**

In the first game of each match no arguments are supplied to your bot.
In each subsequent game of each match:
 - Arg1 will contain the history of your bots hands/decisions in this match
 - Arg2 will contain the history of your opponents hands/decisions in this match

History will be represented by a sequence of single capital letters representing the possible hands you can play.

     | R | Rock     |
     | P | Paper    |
     | S | Scissors |
     | L | Lizard   |
     | V | Spock    |

E.g.

 - Game 1: MyBot.exe
 - Game 2: MyBot.exe S V
 - Game 3: MyBot.exe SS VL
 - Game 4: MyBot.exe SSR VLS

**Output**

Your bot must write a single character response representing his "hand" for each game. The result should be written to STDOUT and the bot should then exit.
Valid single capital letters are below.

     | R | Rock     |
     | P | Paper    |
     | S | Scissors |
     | L | Lizard   |
     | V | Spock    |

 
In the case where your bot does not return a valid hand (i.e. 1 of the above 5 single  capital letters, then you automatically forfeit that hand and the match continues.

In the case where both bots do not return a valid hand, then the game is considered a draw and the match continues.

**Match Format**

Each submitted bot will play one match against each other bot in the tournament.

Each match will last exactly 100 games.

Matches will be played anonymously, you will not have an advanced knowledge of the specific bot you are playing against, however you may use any and all information you can garner from his decision making during the history of the current match to alter your strategy against your opponent. You may also track history of your previous games to build up patterns/heuristics etc... (See rules below)

During a single game, the orchestration engine will run your bot and your opponents bot 100 milliseconds apart and then compare the results in order to avoid any PRNG collisions in the same language/runtime. (this actually happened me during testing).

**Judging & Constraints**

Dr. Sheldon Cooper in the guise of Sheldor the Conqueror has kindly offered to oversee the running of the tournament.
Sheldor the Conqueror is a fair and just overseer (mostly). All decisions by Sheldor are final.

Gaming will be conducted in a fair and proper manner:

 - Your bot script/program will be stored in the orchestration engine under a subfolder `Players\[YourBotName]\`
 - You may use the subfolder `Players\[YourBotName]\data` to log any data or game history from the current tournament as it proceeds. Data directories will be purged at the start of each tournament run.
 - You may not access the Player directory of another player in the tournament
 - Your bot cannot have specific code which targets another specific bots behavior
 - Each player may submit more than one bot to play so long as they do not interact or assist one another.
 - Regarding forfeits, they won't be supported. Your bot must play one of the 5 valid hands. I'll test each bot outside of the tournament with some random data to make sure that they behave. Any bots that throw errors (i.e. forfeits errors) will be excluded from the tourney til they're bug fixed. 

Sheldor will update this question as often as he can with Tournament results, as more bots are submitted.

**Orchestration / Control Program**

Coming Soon - I'll get this up on Github so people can see it, and I'll also keep copies of any submissions 
 
**Leaderboard**

Coming soon after the first tournament. I'll wait until there are a few (4-5) submissions before running the first tournament.

**Submission Details**

Your submission should include

 - Your Bot's name
 - Your Code
 - A command to 
  - execute your bot from the shell e.g.
   - ruby myBot.rb
   - python3 myBot.py
  - OR
  - first compile your both and then execute it. e.g.
   - csc.exe MyBot.cs
   - MyBot.exe
 

**Sample Submission**

    BotName: SimpleRandomBot
    Compile: "C:\Program Files (x86)\MSBuild\12.0\Bin\csc.exe" SimpleRandomBot.cs
    Run:     SimpleRandomBot [Arg1] [Arg2]

Code:

	using System;
	public class SimpleRandomBot
	{
		public static void Main(string[] args)
		{
			var s = new[] { "R", "P", "S", "L", "V" };
			if (args.Length == 0)
			{
				Console.WriteLine("V"); //always start with spock
				return;
			}
			char[] myPreviousPlays = args[0].ToCharArray();
			char[] oppPreviousPlays = args[1].ToCharArray();
			Random r = new Random();
			int next = r.Next(0, 5);
			Console.WriteLine(s[next]);
		}
	}
	
**Clarification**

Any questions, ask in the comments below.




  [1]: http://i.stack.imgur.com/jILea.png