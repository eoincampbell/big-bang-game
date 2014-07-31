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

  hardcoded_moves = 'PRPSPLPVPRPSPLPV'

  if m = hardcoded_moves[player.to_s.size]
    m
  elsif winning?(player, opponent)
    %w[R P S L V].sample
  else
    extrapolate(player[-16,16],opponent[-16,16])
  end

end

def winning?(player,opponent)
  player.chars.zip(opponent.chars).map{ |move| score(*move) }.inject(0,:+) >= 5
end

def extrapolate(player,opponent)
  likelihoods = Hash.new {0}

  opponent_last_move = opponent[-1]
  @moves.each { |m| likelihoods[m] += opponent.scan(opponent_last_move + m).size }

  my_last_move = player[-1]
  reactions = player.chars.zip(opponent.chars.drop(1))
  @moves.each { |m| likelihoods[m] += reactions.count([my_last_move, m]) }

  @moves.shuffle.max_by do |m|
    likelihoods.map{ |n,c| score(m,n) * c }.reduce(0,:+)
  end

end

puts move