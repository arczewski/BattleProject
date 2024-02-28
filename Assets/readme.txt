Welcome to the AF Interview Project. This test consists of 2 parts. The first one requires just some final touches to the existing code, while the second one is a bit more complex and requires you to implement a new system from scratch.

Your tasks for today are as follows:
Part 1:
a) Find a bug in the code logic and fix it - done
b) Optimize the ItemsManager class code - done
c) Extend the whole system by introducing a concept of a consumable item, that upon using, will either add money or a different item to the inventory. - done

Part 2:
Implement a system that will be used to execute fights between certain army units.

Unit types: Long Sword Knight, Archer, Druid, Catapult, Ram

Unit should specify its:
-unit attributes (Light, Armored, Mechanical - each unit can have one or more attributes at once) - done
-health points - done
-armor points - done
-attack interval (the number of turns that need to pass to be able to perform an attack again, so a value of 1 means that a unit can perform the attack each turn) - done
-attack damage - done
-an optional attack damage override value against units with a specific attribute - done
 
The damage dealt should be calculated by taking the attack damage and subtracting the armor points. The final damage dealt can not fall below the value of 1 in any case.
When the health points of a unit reach 0, the unit should be considered unable to fight and should be removed from the army. - done

Set up a turn-based fight between 2 armies, one spawned on the left side of the screen and the other on the right side:
2 Long Sword Knights - done
1 Druid - done
1 Ram - done

versus

3 Archers - done
1 Catapult - done
1 Ram - done

The fight turn order should be determined randomly at the start of the fight and kept for its whole duration. - done
Assume that all of the units are always within the required range to attack any enemy unit. - done
Make sure that editing all of the balance-related values is designer-friendly. - done?
Providing an animated visual representation of the fight will be considered a plus. - done

You'll be graded based on how well you've completed these tasks and on the quality of your code. 
If you're not 100% sure how something should work, try to make it work however *you* would see fit.

One more thing: get a public git repository for this task and commit your changes as you go. We'd like to see your repository work ethic in action.

Email us at work@ancientforgestudio.com with the repository link and a report of what you've managed to do within the next 24 hours. In your work, please use 2 design patterns that you know and document their usage. - done

Good luck!