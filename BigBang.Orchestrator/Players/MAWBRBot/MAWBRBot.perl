use strict;
use warnings;
use Carp qw(croak);

###Stolen from List::Util::ChooseWeighted (with love)
sub choose_weighted{
    my ($objects, $weightsArg ) = @_;
    my $calcWeight = $weightsArg if 'CODE' eq ref $weightsArg;
    my @weights;        # fix wasteful of memory
    if( $calcWeight){
    @weights =  map { $calcWeight->($_) } @$objects;
    }
    else{
    @weights =@$weightsArg;
    if ( @$objects != @weights ){
        croak "given arefs of unequal lengths!";
    }
    }

    my @ranges = ();        # actually upper bounds on ranges
    my $left = 0;
    for my $weight( @weights){
    $weight = 0 if $weight < 0; # the world is hostile...
    my $right = $left+$weight;
    push @ranges, $right;
    $left = $right;
    }
    my $weightIndex = rand $left;
    for( my $i =0; $i< @$objects; $i++){
    my $range = $ranges[$i];
    return $objects->[$i] if $weightIndex < $range;
    }
}
###Credit to Danny Sadinof for choose_weighted code

sub getmove {
    my (@opponenthistory) = @_;
    if(~~@opponenthistory == 0) {
        return ~~(5*rand());
    }
    my %mapper = qw(R 0 P 1 S 2 V 3 L 4);
    @opponenthistory = map {$mapper{$_}} @opponenthistory;
    my %counts = (
        0 => 0,
        1 => 0,
        2 => 0,
        3 => 0,
        4 => 0);
    $counts{$_}++ for @opponenthistory;
    my @keys = keys %counts;
    my @values = values %counts;
    my $choice = choose_weighted(\@keys, \@values);
    return ($choice + 1) % 4;
}

if(~~@ARGV == 0) {
    print qw/R P S V L/[~~(5*rand())];
    exit;
}
my ($myhistory, $opponenthistory) = @ARGV;

print qw/R P S V L/[getmove(split //, $opponenthistory)];