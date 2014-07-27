responses = {
  'R' => ['P', 'V'],
  'P' => ['S', 'L'],
  'S' => ['R', 'V'],
  'L' => ['S', 'R'],
  'V' => ['P', 'L']
}

if ARGV.length == 0
  puts 'L'
else
  puts responses[ARGV[1][-1]].sample
end