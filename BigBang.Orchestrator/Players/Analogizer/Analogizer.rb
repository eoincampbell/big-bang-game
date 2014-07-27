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
  # Throw six lizards in the beginning to confuse opponent
  when 0..5
    'L'
  when 6
    'V'
  when 7
    'S'
  when 8
    'P'
  when 9
    'R'
  else
    analyze_history(player.chars.to_a, opponent.chars.to_a)
  end

end

def analyze_history(player, opponent)
  my_last_move = player.last
  predicted_moves = Hash.new {0}
  opponent_reactions = player.drop(1).zip(opponent)

  # Check whether opponent tended to make a move that would've beaten, lost, or tied my last move
  opponent_reactions.each do |my_move, reaction|
    score = score(reaction, my_move)
    analogous_moves = @moves.select { |move| score == score(move, my_last_move) }
    analogous_moves.each { |move| predicted_moves[move] += 1 }
  end

  # Assume if an opponent has never made a certain move, it never will
  @moves.each { |m| predicted_moves[m] = 0 unless opponent.include?(m) }

  # Pick the move with the best score against opponent's possible moves, weighted by their likelihood, picking randomly for ties
  @moves.shuffle.max_by{ |m| predicted_moves.map { |predicted, freq| score(m, predicted) * freq }.reduce(0,:+) }

end

puts move