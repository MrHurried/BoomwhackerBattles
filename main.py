# THIS PROGRAM IS USED TO CALCULATE ALL POSSIBLE SPAWNS OF THE NOTEBLOCKS IN MY GAME BWB
# HOW IT WORKS:
# 1. collect some basic info about the unity scene
# 2. make some useful calculations with that info
# 3. Inside the while loop: start with setting the right mask's x coord to an iterator, add it to an output string
# 3.1 decrease the iterator by "distance_between_spawns", add the iterator to an output string
# 3.2 repeat
# Noteworthy:
# * inside the while loop, there is code that also adds an ", " to the string. edit if unwanted

#import statements
import pyperclip

# user inputs
print("User guide for the following question: "
      "\n include the one underneath the rightmask"
      "\n in this case YOU SHOULD INPUT 6")
amount_of_NBs = int(input("Amount of NB's: "))
spawn_amount_between_NBs = int(input("Amount of spawnspositions in between two NBs: "))
leftmask_xpos = int(input("Global leftmask x coordinate: "))
rightmask_xpos = int(input("Global rightmask x coordinate: "))

#  the rest of the variables aren't put in an input

units_distance_across_staff = rightmask_xpos - leftmask_xpos   # measured in units
units_distance_between_NBs = units_distance_across_staff/amount_of_NBs    # measured in units
units_distance_between_spawns = units_distance_between_NBs/ spawn_amount_between_NBs

#  calculate and output the possible X coordinates

xposI = rightmask_xpos  # xpos iterator is set to rightmask's xpos, used later for decreasing it's value
outputStr = ""  # output string
while xposI >= leftmask_xpos:  # while xpos iterator is bigger than or equal to leftmask's xpos
    outputStr += str(xposI) + "f, "  # the f is used for marking floats in c#/unity
    xposI -= units_distance_between_spawns

outputStr = outputStr[:-2]  # removes the last two characters, in this case ", "
print(outputStr)
pyperclip.copy(outputStr)

# print useful info
print("\n"
      "----------------------------------------------------------\n"
      "THE OUTPUT HAS BEEN COPIED TO YOUR CLIPBOARD AUTOMATICALLY\n"
      "----------------------------------------------------------\n")

# print debug info
print("noteworthy info: distance_between_NBs = " + str(units_distance_between_NBs))
print("noteworthy info: distance_between_spawns = " + str(units_distance_between_spawns))
