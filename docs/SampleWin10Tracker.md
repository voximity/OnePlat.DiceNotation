# Sample - Win10 (Using IDieRollTracker)
As part of a recent release, we add the ability to gather statistical frequency data of die rolls based on the IDieRoller used, the type of dice that were used, and instances of each roll result. This helps show the randomness of some of the random number algorithms, and allows application developers to use the library to show that frequency data to users.

The responsibility for capturing and querying data in memory is on the OnePlat.DiceNotationLibrary. Developers can disable die roll tracking by not providing one to the IDieRoller constructor. App developers are responsible for visualization of the frequency data and persisting that data between application instance (if desired). 

The interface we focus on for these capabilities is the IDieRollTracker interface.

### Setting Up IDieRollers To Use Roll Tracker

### Querying for Roll Tracker Data

### Persisting Tracker Data
