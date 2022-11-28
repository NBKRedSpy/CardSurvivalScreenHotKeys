![Keyboard Icon](./Media/computer-keyboard_64.png)

# Screen Hot Keys

A modification for the Card Survival Game.

Adds the following hotkeys:
* Escape closes most dialogs.
* Space accepts most action dialogs.
* C opens the Character window.
* B opens the Blueprint window.

# Accepting Actions
If there is more than one button on a dialog, the left most button will be activated.
 
For example: The Fishing spear has two options:  Train and Break.  When pressing space bar, the Train action will be activated.

# Installation 
This mod requires the BepInEx mod loader.

## BepInEx Setup
If BepInEx has already been installed, skip this section.

Download BepInEx from https://github.com/BepInEx/BepInEx/releases/download/v5.4.21/BepInEx_x64_5.4.21.0.zip

* Extract the contents of the BepInEx zip file into the game's directory:
```<Steam Directory>\steamapps\common\Card Survival Tropical Island```

    __Important__:  The .zip file *must* be extracted in the root of the game.  If BepInEx was extracted correctly, there will be a BepInEx folder directly in the game's directory.  This is a common install issue.

* Run the game.  Once the main menu is shown, exit the game.

* In the BepInEx folder, there will now be a "plugins" directory.

## Mod Setup
* Download the ScreenHotKeys.zip.  
    * If on Nexumods.com, download from the Files tab.
    * Otherwise, download from https://github.com/NBKRedSpy/CardSurvivalScreenHotKeys/releases/

* Extract the contents of the zip file into the ```BepInEx/plugins``` folder.

* Run the Game.  The mod will now be enabled.

# Uninstalling

## Undo Modding the Game
This resets the game to an unmodded state.

Delete the BepInEx folder from the game's directory
```<Steam Directory>\steamapps\common\Card Survival Tropical Island\BepInEx```

## Only Uninstalling This Mod

This method removes this mod, but keeps the BepInEx mod loader any any other mods.

Delete the ```ScreenHotKeys.dll``` from the ```<Steam Directory>\steamapps\common\Card Survival Tropical Island\BepInEx\plugins``` directory.
# Compatibility
Safe to add and remove from existing saves.

The UnityExplorer mod interferes with the hotkey handling.

# Acknowledgments
Electric keyboard icon created by yoyonpujiono https://www.flaticon.com/free-icons/electric-keyboard

# Change Log
# 1.0.2
* Changed GetKey to GetKeyDown to avoid repeating executions.
* Fixed accept looking for Escape and Space instead of just Space.

# 1.0.3
* Added Character and Blueprint windows.
* Fixed disabled buttons invoking.  For example, at night.


