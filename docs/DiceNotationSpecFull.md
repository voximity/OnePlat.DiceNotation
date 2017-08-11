# Dice Notation Specification
Dice notation (also known as dice algebra, common dice notation, RPG dice notation, and several other titles) is a system to represent different combinations of dice in role-playing games using simple algebra-like notation such as 2d6+12.

### Basic Dice Notation
The number of sides and the number of dice used in a game are conventionally notated as a short sequence of characters.

Regular six-sided dice are notated as d6 and eight-sided dice are d8. The d represents the die. The number of sides on the die is represented by the number following the d.

If you're throwing more than 1 die then precede the letter d with the number of dice your using. So 3 dice that are six-sided are notated as 3d6 and 4 dice that are ten-sided are notated as 4d10. The number of dice used is represented by the number preceding the d.

The notation can be modified with a mathematic operator and number. So 2d6 - 3 means two six-sided dice totalled and then subtracted by 3. The operator could be either -, +, x, /.

__XdY [-] [+] [x] [/] N__          X dice with Y-sides operated on by (-, +, x, /) with number N

A second roll of different dice could be used instead of a fixed operating number.

__XdY [-] [+] [x] [/] AdB__        The first roll of X dice with Y-sides is operated on by (-, +. x, /) with a second roll of A dice with B-sides. 

d% represents a percentile dice. That is one with 100 faces. Two ten sided dice numbered 0 - 9 and 00 - 90 can be used as percentile dice.  Two 10 faced dice numbered 0 to 9 can be used with one die representing 10s and the other representing units. If both dice are rolled 0 then the roll represents 100%.  20 sided dice are sometimes numbered 0 to 9 twice and can be used as percentile dice in the same way.  

### Math Operators 
Dice notation supports the basic arithmetic operators in any mathematical formula: +, -, * (multiply), and / (divide).

In addition to the basic four, there are: 
* %, for modulus division. The result of a % b is the remainder of a / b. In general, the result of a % b when a and b are both whole numbers will be a whole number in the range [0, |b| - 1] where |b| is the absolute value of b.
* ^, the notation for "raising a to the power of b".

Operations are performed by order of precedence, just like in normal mathematics. From highest to lowest precedence: 
* Grouping with parentheses (( and )); just like in real math, you can modify the precedence ordering by wrapping parentheses around things 
* Exponentiation (^) 
* Multiplication (x), division (/), and modulus (%), in the order they appear (left-to-right) in the formula 
* Addition (+) and subtraction (-), in the order they appear (left-to-right) in the formula 

### Order of Operations 
While the Dice Notation does support basic math operations, it is still all about how dice are represented, and so it has a modified order of operations. This means that putting parentheses inside of your dice formula will not always affect the outcome of the roll or be allowed. Here is the general order of operations: 
* Macros are expanded, including nested macros up to 99 levels deep. 
* Variables are substituted 
* Roll queries are executed (the player making the roll is asked to provide a value for each query, and that value is substituted in where the roll query appears in the formula) 
* All previous steps are repeated until there are no longer any unresolved macros, variables, or queries. This allows for nesting.
* Inline rolls are executed, starting with the most deeply nested inline roll working upward. The overall result of the inline roll is substituted in place where it appeared in the formula. 
* The remaining roll is executed: first, dice are rolled for any dice (e.g. "2d6" is rolled; including any special dice operations such as kept), then the result of that roll is substituted into the formula. Finally, the entire remaining formula is evaluated, including observing proper math order of operations (parentheses first, then multiplication/division, then addition/subtraction). 
