# Changelog - Gambling Simulator

## 3/14/2024

Began work on this project. I want to make a mobile gambling simulation game. I want to include various games, Mines and Plinko are the ones I can think of at the moment.

* Created the initial Unity project (off the tutorial project I made)
* Implemented grid of 5x5 cubes where upon starting the game, one of them is marked Red (for the mine)
* You can click to delete cubes.

Future ideas:

* Animation to "break" open cubes and reveal a diamond/mine
* Option to add more mines with higher payout

## 3/15/2024

Lots of progress today:

* Main menu screen
    * Play - Takes you to Mines game
    * Quit - Quits the game
* Created covers for the Mines contents, now green squares with one red
* Game can now detect whether or not a red square has been unboxed.
* Hitting escape key from Mines game will navigate back to main menu.

Working title is now "Gambling Simulator"

When implementing the algorithm for bets, a good site to possibly reference is __Minding the Data__'s video on [Roobet Mines](https://www.youtube.com/watch?v=94ylCzrVY90&t=66s&ab_channel=MindingTheData)

## 3/16/2024

Today's implementations:

* Added ability to select 1,3, or 5 mines instead of always having one

Now that this is added, covers need to be replaced every time this happens. I am running into issues trying to locate the inactive cover objects, meaning the ones that have been clicked away. When I try and set the game object back to active, I get a null reference exception in my code, for any of the covers. The algorithm I am using looks fine according to my searches, but maybe I am missing something. Algorithm/method here needs refined and completed.

After some research - I think the solution here is to change WHERE I am making the covers inactive. I might need to be making the covers active and inactive from the same script.

## 4/1/2024

Finally found some time to work on this project some more (classes have been nuts as of late)

* Covers can now be replaced after being clicked off
* Contents now succesfully reset after you pick a number of mines

The fix for the cover problem was to disable the object's renderer instead of setting the object to inactive. Doing some research, I found that setting objects to inactive also disables their scripts respectively. Setting the renderer to inactive to "remove" or make an object invisible when you want to get to get it back seems to be the move.

## 4/3/2024

Re-wrote some code. I was locating gameobjects in a really inefficent way before, and I wanted to fix it before I started adding more complexity.

Note to self: start using version control at some point.

## 4/4/2024

Added some UI components, you can now see placeholders for balance, a "Start Game" button, and a "Cash Out" button.

The next step is being able to tell whether or not the game is over, and making certain elements not clickable when that happens.

## 4/9/2024

Moved game board to the right to accomadate for more UI features.

You can now start game and cash out (without money) successfully.

Currently working on making toggle 1, 3, or 5 mines buttons turn colors based off whether or not they are selected. Current approach is not working correctly.

## 4/12/2024

Scrapped making toggle buttons change colors, for now. Seems to be rather tedious and I would like to have core features before I make the game look prettier.

Added bet amount on screen. The bet starts at one cent and can be cut in half, doubled, or you can go all in.

## 4/26/2024

Bet value can now be modified.

## 4/30/2024

Starting the math section of this project.

Installed NuGetForUnity to manage NuGet packages, I needed MathNet.Numerics to be able to use a factorial function for probability calculations.

The amount to win (what player cashes out with) can now be calculated.

Next task to finish is updating balance and displaying the amount won on the cash out button.

## 5/11/2024

Done with Spring classes! Looking to make substantial progress on this project.

Fixed a bug where you could click squares when there was not a game in progress by introducing a "red square revealed" variable.

Got rid of the static variables in MinesGame for use in CoverBehavior by instead finding parent MinesGame, and making them non-static.

Balance and bet amount now update appriopriately on the screen. 

Core mechanics are essentially finished now.

## 5/12/2024

Discovered and fixed a bug where after playing a round of mines, mines are not generated unless you click a mines number.

Added a feature where up arrow can be pressed to make all spaces transparent for debugging (and cheating).

## 5/13/2024

Fixed another bug where bet amount was not properly reducing when balance was low.

Cleaned up and shortened code throughout CoverBehavior and MinesGame.

I moved files around and now the MinesGame script cannot find the 'MathNet' namespace from NuGet. I deleted NuGet from
the project, re-imported it, and I am still getting this error.

Eventually fixed it. I'll take this as my sign to start using version control like I said I would.

## 5/14/2024

Added this project to GitHub. Everything up to this point is now considered version 1.0.


