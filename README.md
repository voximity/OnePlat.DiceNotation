# OnePlat.DiceNotation
DiceNotation library written in .NET Standard to provide dice notation parsing, evaluation, and rolling. This library is built on .NET Standard 1.4, so you can incorporate it into any of your .NET projects: UWP, WPF, Xamarin, Xamarin.Forms, .NET 4.6.2, and .NET Core 1.1.

Dice notation (also known as dice algebra, common dice notation, RPG dice notation, and several other titles) is a system to represent different combinations of dice in role-playing games using simple algebra-like notation such as 2d6+12.

The specification for the dice notation supported in the current version on the library is located [here](docs/DiceNotationSpecCurrent.md).

# Build
To build this project, you will need to clone this repository locally.

This was built using Visual Studio 2017 Update 2 (version 15.2 - 26430.15). It will work with other versions of Visual Studio, but wasn't tested with them.

* Launch the solution in Visual Studio.
* Be sure to update "Restore NuGet Packages" on the solution.
* Then Rebuild the full solution.

You can run the OnePlat.DiceNotation.CommandLine project to launch a command-line applet that lets you play with the functionality.

From a command project in the output folder, run the following int he command prompt:

  dotnet dice.dll 4d6k3 -v

# Installation

# Usage

# Feedback
If you use this library and have any feedback, bugs, or suggestions, please file them in the Issues section of this GitHub repository.

I have plans for better notation support, so there will be updates coming for this project. Your feedback would be appreciated.
