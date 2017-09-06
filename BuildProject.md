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

### Building samples

To build the samples, you will need to:
* Navigate to the /Samples folder in the repository.
* Open the Samples.sln solution file in Visual Studio.
* Be sure to update "Restore NuGet Packages" on the solution.
* Then Rebuild the full solution.

To run the sample:
* Ensure that the DiceRoller.Win10 project is selected as the startup project.
* Then Debug-F5 (or Run - Ctrl+F5) the solution.
* The DiceRoller app should start up, give it a try (you can try some [example notations](docs/DiceNotationExamples.md)).
