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

  case player.to_s.size
  when 0..19
    'R'
  when 20..30
    opponent.count('P') > opponent.count('V') ? 'S' : 'L'
  else
    extrapolate(player, opponent)
  end

end

def extrapolate(player,opponent)
  likelihoods = Hash.new {0}

  opponent_last_move = opponent[-1]
  @moves.each { |m| likelihoods[m] += opponent.scan(opponent_last_move + m).size }

  my_last_move = player[-1]
  reactions = player.chars.zip(opponent.chars.drop(1))
  @moves.each { |m| likelihoods[m] += reactions.count([my_last_move, m]) }

  @moves.shuffle.max_by do |m|
    likelihoods.map{ |n,c| score(m,n) * c }.reduce(:+)
  end
end

puts move