responses = {
  'R' => ['P', 'V'],
  'P' => ['S', 'L'],
  'S' => ['R', 'V'],
  'L' => ['S', 'R'],
  'V' => ['P', 'L']
}

if ARGV.length == 0 || (history = ARGV[1]).length < 3
    choices = ['R','P','S','L','V']
else
    markov = Hash.new []
    history.chars.each_cons(3) { |chars| markov[chars[0..1].join] += [chars[2]] }

    choices = []
    likely_moves = markov.key?(history[-2,2]) ? markov[history[-2,2]] : history.chars
    likely_moves.each { |move| choices += responses[move] }
end

puts choices.sample