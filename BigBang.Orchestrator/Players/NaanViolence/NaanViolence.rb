@counters = { "R" => [ "P", "V" ], "P" => [ "S", "L" ], "S" => [ "V", "R" ], "L" => [ "R", "S" ], "V" => [ "P", "L" ] }
@counts = { "R" => 0, "S" => 0, "P" => 0, "V" => 0, "L" => 0 }

choices = []

if( ARGV != nil && ARGV[0] != nil )

  mine = ARGV[0].split("")
  theirs = ARGV[1].split("")

  c2 = @counts.clone
  bestChoice = 100

  mine.each {|x| c2[x]+=2}
  theirs.each {|x| c2[@counters[x][0]]-=1; c2[@counters[x][1]]-=1}
  c2.keys.each {|x| if( c2[x] < bestChoice ) then bestChoice = c2[x] end}
  c2.keys.each {|x| if( c2[x] == bestChoice ) then choices.push(x) end}

else
  choices = @counters.keys
end

puts choices.sample