my ($phist, $ohist) = @ARGV;

my %text2num = ('R',0,'V',1,'P',2,'L',3,'S',4);  #the RVPLS ordering is superior
my @num2text = ('R','V','P','L','S');

@phist = map($text2num{$_},split(//,$phist));
@ohist = map($text2num{$_},split(//,$ohist));

for(0..~~@phist-3){$curloc=$_;
 $result = $ohist[$curloc+2];
 @moveset = ($ohist[$curloc],$ohist[$curloc+1],$phist[$curloc],$phist[$curloc+1]);
 for(0..3){$a=$_;
  for(0..$a){$b=$_;
   $predict[$a][$b][$moveset[$a]][$moveset[$b]][$result]++;
  }
 }
}

@recentmoves = ($ohist[-2],$ohist[-1],$phist[-2],$phist[-1]);

@curpred = (1,1,1,1,1);

for(0..3){$a=$_;
 for(0..$a){$b=$_;
  for(0..4){$move=$_;
   $curpred[$move] *= $predict[$a][$b][$recentmoves[$a]][$recentmoves[$b]][$move]/5+1;
  }
 }
}

@bestmove = (0,0,0,0,0);
for(0..4){
 $bestmove[$_] = $curpred[$_]/2+$curpred[$_-1]+$curpred[$_-2];
}

$max = 0;
for(0..4){
 if($bestmove[$_]>$max){
  $max = $bestmove[$_];
 }
}
@options=();
for(0..4){
 if($bestmove[$_] == $max){
  push(@options,$num2text[$_]);
 }
}
$output = $options[int(rand(~~@options))];

print "$output"