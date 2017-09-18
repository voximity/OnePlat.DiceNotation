# Building Code

### Building project code

To build this project, you will need to clone this repository locally.

This was built using Visual Studio 2017 Update 3 (version 15.3.3). It will work with other versions of Visual Studio, but wasn't tested with them.

* Open the OnePlat.DiceNotation.sln solution file from the repository root in Visual Studio.
* Be sure to update "Restore NuGet Packages" on the solution.
* Then Rebuild the full solution.

Along with the library build, this also build a command-line tool for testing. You can run the OnePlat.DiceNotation.CommandLine project to launch a command-line applet that lets you play with the functionality.

From a command project in the output folder, run the following in the command prompt:

```
dotnet dice.dll d20+3
dotnet dice.dll 4d6k3 -v
```

### Building ExtendedDieRollers projects

To build the ExendedDieRollers project, you need to follow similar steps as above, but with a different solution:
* Open the ./OnePlat.DiceNotation/ExtendedDieRollers/ExtendedDieRollers.sln solution file from the repository root in Visual Studio.
* Be sure to update "Restore NuGet Packages" on the solution.
* Then Rebuild the full solution.

Note that ExtendedDieRollers is a Portable Class Library (PCL) rather than a .NET Standard 1.4 library because it has dependencies on a couple of other PCLs (MathNet and PCLCrypto). This library will be updated to .NET Standard 1.4 or 2.0 as soon as either the two dependent packages are upgraded, or .NET Standard supports the same targets that those packages do. (Likely it will be when we upgrade to .NET Library 2.0.)

### Building samples

To build the samples, you will need to:
* Navigate to the /Samples folder in the repository.
* Open the Samples.sln solution file in Visual Studio.
* Be sure to update "Restore NuGet Packages" on the solution.
* Then Rebuild the full solution.

To run the Win10 sample:
* Ensure that the DiceRoller.Win10 project is selected as the startup project.
* Then Debug-F5 (or Run - Ctrl+F5) on the project.
* The DiceRoller app should start up, give it a try (you can try some [example notations](docs/DiceNotationExamples.md)).

To run the ASP.NET MVC sample:
* Ensure that the DiceRoller.Mvc project is selected as the startup project.
* Then Debug-F5 (or Run - Ctrl+F5) on the project.
* The DiceRoller web app should start up in your default browser, give it a try (you can try some [example notations](docs/DiceNotationExamples.md)).
* From the main page, click the Dice Roller button to get to the input page.

Also, if you just want to try out the web application, you can find it online at [d20 Dice Roller](http://dicenotation-diceroller-mvc.azurewebsites.net/).
