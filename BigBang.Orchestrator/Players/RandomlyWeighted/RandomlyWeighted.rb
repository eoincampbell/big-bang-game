module Priorities
  PRIORITIES_FILE_PATH = File.join(DATA_FOLDER_PATH, "priorities.marshal-rb")

  def self.priorities
    begin
      load_saved_priorities
    rescue Errno::ENOENT => e
      # file doesn't exist yet; it's the first round
      chosen_priorities = choose_priorities_randomly
      save_priorities(chosen_priorities)
      chosen_priorities
    rescue IOError => e
      $stderr.puts "Error: priorities file exists, but could not read it."
      $stderr.puts "Fix its permissions or something."
      exit(1)
    end
  end

  private

  def self.choose_priorities_randomly
    probability_spread = [0.1, 0.1, 0.2, 0.2, 0.4]
    options = ['R', 'P', 'S', 'L', 'V']
    paired_options_and_probs = options.shuffle.zip(probability_spread)
    Hash[paired_options_and_probs]
  end

  def self.load_saved_priorities
    File.open(PRIORITIES_FILE_PATH) do |priorities_file|
      Marshal.load(priorities_file)
    end
  end

  def self.save_priorities(priorities_to_save)
    File.open(PRIORITIES_FILE_PATH) do |priorities_file|
      Marshal.dump(priorities_to_save, priorities_file)
    end
  end
end

def prioritized_random_choice(priorities)
  # precondition: the probabilities in `priorities` add up to 1

  remaining_probability_needed = rand
  priorities.to_a.shuffle.each do |option, probability|
    remaining_probability_needed -= probability
    if remaining_probability_needed < 0
      return option
    end
  end
end

puts prioritized_random_choice(Priorities.priorities)