# Dice Notation Specification
Dice notation (also known as dice algebra, common dice notation, RPG dice notation, and several other titles) is a system to represent different combinations of dice in role-playing games using simple algebra-like notation such as 2d6+12.

### Basic Dice Notation
The number of sides and the number of dice used in a game are conventionally notated as a short sequence of characters.

Regular six-sided dice are notated as d6 and eight-sided dice are d8. The d represents the die. The number of sides on the die is represented by the number following the d.

If you're throwing more than 1 die then precede the letter d with the number of dice your using. So 3 dice that are six-sided are notated as 3d6 and 4 dice that are ten-sided are notated as 4d10. The number of dice used is represented by the number preceding the d.

The notation can be modified with a mathematic operator and number. So 2d6 - 3 means two six-sided dice totalled and then subtracted by 3. The operator could be either -, +, x, /.

__XdY [-] [+] [x] [/] N__          X dice with Y-sides operated on by (-, +, x, /) with number N

A second roll of different dice could be used instead of a fixed operating number.

__XdY [-] [+] AdB__        The first roll of X dice with Y-sides is operated on by (-, +) with a second roll of A dice with B-sides. 
**future support:** x, / operations

### Math Operators 
Dice notation supports the basic arithmetic operators in any mathematical formula: +, -, x (multiply), and / (divide).

Operations are performed by order of precedence, just like in normal mathematics. From highest to lowest precedence: 
* Multiplication (x), division (/), in the order they appear (left-to-right) in the formula 
* Addition (+) and subtraction (-), in the order they appear (left-to-right) in the formula 
**Note** Division only works after the dice definition. Cannot currently use dice roll as the denominator (perhaps in the future)

### Order of Operations 
While the Dice Notation does support basic math operations, there are special rules for handling dice operations that modify the order of operations. Here is the general order of operations: 
* Roll queries are executed (the player making the roll is asked to provide a value for each query, and that value is substituted in where the roll query appears in the formula) 
* The remaining roll is executed: first, dice are rolled for any dice (e.g. "2d6" is rolled; including any special dice operations such as kept), then the result of that roll is substituted into the formula. Finally, the entire remaining formula is evaluated, including observing proper math order of operations (parentheses first, then multiplication/division, then addition/subtraction). 