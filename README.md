# OnePlat.DiceNotation
DiceNotation library written in .NET Standard to provide dice notation parsing, evaluation, and rolling. This library is built on .NET Standard 1.4, so you can incorporate it into any of your .NET projects: UWP, WPF, Xamarin, Xamarin.Forms, .NET 4.6.2, and .NET Core 1.1.

Dice notation (also known as dice algebra, common dice notation, RPG dice notation, and several other titles) is a system to represent different combinations of dice in role-playing games using simple algebra-like notation such as 2d6+12.

The specification for the dice notation supported in the current version on the library is located [here](docs/DiceNotationSpecCurrent.md). There are also [examples of dice notation](docs/DiceNotationExamples.md) strings.

# Build
To build this project, you will need to clone this repository locally.

This was built using Visual Studio 2017 Update 3 (version 15.3.3). It will work with other versions of Visual Studio, but wasn't tested with them.

* Launch the solution in Visual Studio.
* Be sure to update "Restore NuGet Packages" on the solution.
* Then Rebuild the full solution.

You can run the OnePlat.DiceNotation.CommandLine project to launch a command-line applet that lets you play with the functionality.

From a command project in the output folder, run the following int he command prompt:

```
dotnet dice.dll d20+3
dotnet dice.dll 4d6k3 -v
```

# Installation
To install these packages into your solution, you can use the Package Manager. In PM, please use the following commands:
```  
PM > Install-Package OnePlat.DiceNotation -Version 1.0.3
``` 

To install in the Visual Studio UI, go to the Tools menu > "Manage NuGet Packages". Then search for OnePlat.DiceNotation and install it from there.

# Usage
OnePlat.DiceNotation has a couple of different modes that it can be used in depending on how you want to build up the dice expression:

### Programmatically:
You can build up the dice to roll by coding the various parts that make up a dice expression. The expression can be build by chaining together operations (as a Fluent API style).

```csharp
IDice dice = new Dice();
// equivalent of dice expression: 4d6k3 + d8 + 5
dice.Dice(6, 4, choose: 3).Dice(8).Constant(5);
DiceResult result = dice.Roll(new RandomDieRoller());
Console.WriteLine("Roll result = " + result.Value);
```
   
### Dice Notation String:
You can also create the dice expression by parsing a string that follows the defined Dice Notation language. When you parse the text, we create a similar expression tree as the programmatic version that is then evaluated.

```csharp
IDice dice = new Dice();
DiceResult result = dice.Roll("d20+4", new RandomDieRoller());
Console.WriteLine("Roll result = " + result.Value);
```

### Dice Rollers:
Both of these options use the RandomDieRoller, which uses the .NET random class to produce the random dice rolls. There is also a ConstantDieRoller, which lets you create a roller that always returns the same value. This roller is great for testing features and expressions because the results will be consistent in your unit tests.

Finally, the library defines a IDieRoller interface that you can use to build your own custom die rollers. If the pseudo-random generation of the .NET Random class isn't good enough, you can override it with your own rolling implementation.

# Feedback
If you use this library and have any feedback, bugs, or suggestions, please file them in the Issues section of this GitHub repository.

I have plans for better notation support, so there will be updates coming for this project. Your feedback would be appreciated.
