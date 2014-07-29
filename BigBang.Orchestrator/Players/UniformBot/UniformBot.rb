@counters = { "R" => [ "P", "V" ], "P" => [ "S", "L" ], "S" => [ "V", "R" ], "L" => [ "R", "S" ], "V" => [ "P", "L" ] }
@counts = { "R" => 0, "S" => 0, "P" => 0, "V" => 0, "L" => 0 }

choices = []

if( ARGV != nil && ARGV[0] != nil )

  mine = ARGV[0].split("")
  theirs = ARGV[1].split("")

  c = @counts.clone

  if( mine.length < 50 )
    if( mine.length.even? )
      mine.values_at(* mine.each_index.select {|i| i.even?}).each {|x| c[x]+=1}
      @counts.keys.each {|x| if( c[x]>4 ) then c.delete(x) end}
    else
      mine[0..-1].each_index.select {|i| i.even?}.each {|x| if( mine[x]==mine[-1] ) then c.delete(mine[x+1]) end}
    end
    choices = c.keys
  else
    c2 = @counts.clone
    for i in 0 .. mine.length - 3
      if( mine[i..i+1].join == mine[-2..-1].join ) then c[theirs[i+2]]+=1 end
    end
    bestCount = 0
    @counters.keys.each {|x| @counters[x].each {|y| c2[y]+=c[x];c2[x]+=c[x]*0.4}}
    @counters.keys.each {|x| if( c2[x] > bestCount ) then bestCount = c2[x] end}
    @counters.keys.each {|x| if( c2[x] == bestCount ) then choices.push( x ) end}
  end
else
  choices = @counters.keys
end

puts choices.sample