# collapse-web-game

# Launch Order
1. Server project (name: "XServer")
2. Client project (name: "CollapseGameFormsApp") - run twice (for both players)

# Game rules
* The player who pressed "Start Game" faster goes first
* On the first turn, you can assign yourself a cell of any field by placing 3 dots of your color on it
* Further, you can only go to cells that you own
* When there are four dots in a cell, the cell is divided into 4 cells with one dot. They are placed in the top, bottom, left and right cells.
* If during division the cell falls on the opponent's cell, then the opponent's cell becomes yours
* The winner is the one who captures all the cells of the opponent

# Demo
> https://user-images.githubusercontent.com/45340222/222469163-827228da-fe23-462f-9ccf-e625ba16646d.mp4

# About the game
* This is a desktop game for two players, a copy of the mobile version of "Collapse game"
* Player interaction takes place over the network through a self-written server that connects two players and makes sure that none of them cheat
* If the server notices that any of the players violates the rules (trying to make an invalid move), then it removes him from the game, assigning the victory to the remaining player
* All communication between the players and the server takes place using a self-written protocol, the code for which was taken from [this article](https://tech-geek.ru/create-network-protocol/)
* Well, actually, the code isn't well-written, neither its parts are separated in well order (like what S from SOLID states for). But it works!
