$scores = [ 0, 0, 0 ]

$moves = { "R" => 0, "P" => 1, "S" => 2, "L" => 3, "V" => 4 }
$responses = [ "R", "P", "S", "L", "V" ]
$counters = { "R" => [ "P", "V" ], "P" => [ "S", "L" ], "S" => [ "V", "R" ], "L" => [ "R", "S" ], "V" => [ "P", "L" ] }

def try( theirs, mine )
        return $counters[theirs].include? mine
end

def counter( move )
        return $counters[move][rand(2)]
end

def follower( move )
        return $responses[($moves[move]+1)%5]
end

def echo( move )
        return move
end

if ARGV[1] != nil
        for i in 1 .. ARGV[1].length - 1
                if try( ARGV[1].split("")[i], counter( ARGV[1].split("")[i-1] ))
                        $scores[0] += 1
                end
                if try( ARGV[1].split("")[i], follower( ARGV[1].split("")[i-1] ))
                        $scores[1] += 1
                end
                if try( ARGV[1].split("")[i], echo( ARGV[1].split("")[i-1] ))
                        $scores[2] += 1
                end
        end
end

if $scores[0] > $scores[1] && $scores[0] > $scores[2]
        puts counter( ARGV[1].split("")[-1] )
elsif $scores[1] > $scores[2] && $scores[1] > $scores[0]
        puts follower( ARGV[1].split("")[-1] )
elsif $scores[2] > $scores[0] && $scores[2] > $scores[1]
        puts follower( ARGV[1].split("")[-1] )
else
        puts $responses[rand(5)]
end