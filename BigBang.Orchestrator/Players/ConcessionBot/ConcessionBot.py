import sys
import random

concessions = {"R": "LS", "P": "VR", "S": "PL", "L": "VP", "V": "SR"}
valid_letters = "RPSLV"

# Create a dictionary of all possible patterns of how the cause history could have affected the effect history's next letter
def generate_pattern_dict(cause_history, effect_history):
    dictionary = {}

    # Run through all the rounds so far
    for round in xrange(len(cause_history)):

        history = cause_history[:round][::-1]
        letter = effect_history[round]

        if not letter in dictionary:
            dictionary[letter] = {}

        # Loop through every size of a string that could fit in the history
        for hs_len in range(1, len(history) + 1):

            # Loop through every position a string of that size could be in
            for hs_pos in range(len(history) - hs_len + 1):

                # Add this occurance to the dictionary; noting how long before the letter it occured and what it was
                history_string = history[hs_pos:hs_pos + hs_len]
                dist = hs_pos
                try:
                    dictionary[letter][(dist, history_string)] += 1
                except KeyError:
                    dictionary[letter][(dist, history_string)] = 1
    return dictionary

# Given a pattern dictionary; predict the next letter based on a history
def get_probabilities(patterns, history):
    probs = {}
    history = history[::-1]

    # Look at all the letters that have been used
    for letter in patterns:
        probs[letter] = 0

        # Look at all the known string patterns to have preceded this string and mark this letter as more likely the more strings that point to it
        for key, occurrences in patterns[letter].iteritems():
            dist, history_string = key

            if history[dist: dist + len(history_string)] == history_string:
                probs[letter] += occurrences

    return probs

def get_most_probable(dictionary):
    highest_prob = 0
    hightst_letters = ""

    for letter, prob in dictionary.iteritems():
        if prob >= highest_prob:
            if prob > highest_prob:
                hightst_letters = letter
                highest_prob = prob
            else:
                hightst_letters += letter

    if len(hightst_letters) == 0:
        hightst_letters = valid_letters

    return random.choice(hightst_letters), highest_prob

def get_histories():
    try:
        return (sys.argv[1], sys.argv[2])
    except IndexError:
        return ("", "")

def concede(letter):
    return random.choice(concessions[letter])

def get_hand(my_history, opponent_history):

    # Look at the affect the opponent's moves have on what they choose next
    pattern_dict = generate_pattern_dict(opponent_history, opponent_history)
    pattern_probs = get_probabilities(pattern_dict, opponent_history)
    pattern_letter, pattern_prob = get_most_probable(pattern_probs)

    # Look at what affect my moves have on what the opponent chooses next
    prediction_dict = generate_pattern_dict(my_history, opponent_history)
    prediction_probs = get_probabilities(prediction_dict, my_history)
    prediction_letter, prediction_prob = get_most_probable(prediction_probs)

    if pattern_prob > prediction_prob:
        opponent_letter = pattern_letter
    elif pattern_prob < prediction_prob:
        opponent_letter = prediction_letter
    else:
        opponent_letter = random.choice((pattern_letter, prediction_letter))

    return concede(opponent_letter)


print get_hand(*get_histories())