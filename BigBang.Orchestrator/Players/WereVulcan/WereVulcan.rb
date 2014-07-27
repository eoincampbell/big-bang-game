@rules = {

  'L' => %w[V P],
  'P' => %w[V R],
  'R' => %w[L S],
  'S' => %w[P L],
  'V' => %w[R S]
}

@moves = @rules.keys

def defeats?(move1, move2)
  @rules[move1].include?(move2)
end

def score(move1, move2)
  if move1 == move2
    0
  elsif defeats?(move1, move2)
    1
  else
    -1
  end
end

def move
  player, opponent = ARGV

  # For the first 30 rounds, pick a random move that isn't Spock
  if player.to_s.size < 30
    %w[L P R S].sample
  elsif opponent.chars.to_a.uniq.size < 5
    exploit(opponent)
  else
    # Pick a random move that's biased toward Spock and against lizards
    %w[L P P R R S S V V V].sample
  end

end

def exploit(opponent)
  @moves.shuffle.max_by{ |m| opponent.chars.map{|o| score(m,o) }.reduce(:+) }
end

puts move