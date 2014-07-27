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
  if defeats?(move1, move2)
    1
  elsif defeats?(move2, move1)
    -1
  else
    0
  end
end
def move
  player, opponent = ARGV
  strategy = %w[Shoot Bullet, Laugh Victoriously, Pick Randomly]
  strategy[player.to_s.size] || pick_randomly(opponent.chars.to_a)
end
def pick_randomly(opponent)
  predicted_moves = opponent.sample(5)
  @moves.shuffle.max_by{ |m| predicted_moves.map { |predicted|
score(m, predicted) }.reduce(:+) }
end
putc move
