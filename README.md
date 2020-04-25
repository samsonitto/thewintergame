# Survive The Crash

* Samson Azizyan
* M3156
* Game Programming (spring 2020), JAMK

# Table of Contents 

<!--**Pidä sisällysluettelo kunnossa, eli päivitä sitä tarpeen mukaan! Huomaa MarkDown-ankkurilinkitys**-->

- [Survive The Crash](#survive-the-crash)
- [Table of Contents](#table-of-contents)
- [Story](#story)
- [Installation](#installation)
    - [Windows version (Recommended)](#windows-version-recommended)
    - [Web version](#web-version)
- [How To Play](#how-to-play)
  - [Objectives](#objectives)
  - [Controls](#controls)
- [Game Mechanics](#game-mechanics)
  - [Player](#player)
  - [Terrain](#terrain)
  - [Animals](#animals)
  - [Items](#items)
  - [Inventory](#inventory)
  - [Weapon](#weapon)
  - [Flashlight](#flashlight)
  - [Objective mechanics](#objective-mechanics)
  - [UI](#ui)
- [Used Technoligies](#used-technoligies)
- [Used Tutorials](#used-tutorials)
- [Used Assets](#used-assets)
- [Work Hours](#work-hours)
- [Bugs](#bugs)
  - [Animals](#animals-1)
  - [Player](#player-1)
- [Left out Features](#left-out-features)
- [Overview](#overview)
- [Grade suggestion](#grade-suggestion)

# Story
You have crashed somewhere in the uncharted territory of the Siberia.
Your Super Spitfire Airplane is in very poor condition, you need to fix it to get home.
You need to search the territory for the plane parts. Ne aware of the dangerous animals and your hunger and thirst.

# Installation
Download the game from the link below, unzip it and run the WinterSurvival.exe
### [Windows version (Recommended)](http://survive.codesamson.com/SurviveTheCrash.zip)

Or you can play the game straight from the browser. I recommend this option, if you have a decent computer. Use Mozilla Firefox browser.
### [Web version](http://survive.codesamson.com)

# How To Play

## Objectives
Find all the airplane parts to fix the airplane. Kill the enemies, pick up water and meat that are dropped by dead animals.
Drink and eat so you won't starve.

## Controls
| Action                   | Button                                                       |
| ------------------------ | ------------------------------------------------------------ |
| Movement                 | W,A,S,D                                                      |
| Turn / Head Movement     | Mouse                                                        |
| Sprint                   | Left Shift                                                   |
| Shoot                    | Left Mouse Button                                            |
| Pick Up                  | F                                                            |
| Equip / Eat / Drink      | Left Mouse Click on the item in the inventory menu           |
| Unequip Weapon           | E                                                            |
| Unequit Flashlight       | T                                                            |
| Turn Flashlight On / Off | Q                                                            |
| Craft                    | Left Mouse Click on the craftable item in the inventory menu |
| Toggle Inventory         | Tab                                                          |
| Toggle Pause Menu        | Esc                                                          |

# Game Mechanics
I've used a lot of different game mechanics. The list of every game mechanic and the explanations can be found below.

## Player
I have used the First Player All In One asset, all the movement is inherited from it. I have disabled the stamina as the game would have taken too long to complete.
Player can sprint, jump and has a normal fps movement. The player has health, thirst and hunger. Hunger increases by maxHunger * 0.1 and thirst is increasing by maxThirst * 0.15.
If the player is reached max thirst or max hunger his health will be decreasing. If the hunger and thirst are below 50% the health will regenerate.

## Terrain
I have created the terrain by myself with the unity terrain tools. It does not look amazing and you can fall of it.

## Animals
I have used 3 animals: Tiger, Leopard and Spider. I have purchased the animal pack for 5 dollars from the Unity Asset Store.
By default animals are roaming around the terrain. When the player enters the look radius od the animal, animal starts sprinting towards the player
and if the animal gets to the player in time, it will kill the player. Animals can be shot and killed by the player. If animal's health gets the 30%, animal
will start running away from the player. When animal dies it drops items, that the player needs to survive.
The challenge here was to align the animal to the terrain correctly as it moves on the hills. I've done it by using the RayCastHit.
Animals have 3 animations: walk, run and attack. I had the death animation also implemented, but it did not work properly so I removed it.

## Items
There are various types of items in the game: water, meat, weapon, flashlight, airplane parts, animal skin (useless in this version), silk (useless in this version)
Water can be drinked to get the thirst down, the same goes for the meat and hunger. Weapon you use to shoot the animals. Flashlight you might not need, but it's useful if you are in the shadow.
Animal skin was suppose on be for crafting pants ja jacket, but I did not have time to finish this feature. Same goes with silk.
Airplane parts are crucial for the completion of the game.
Items can be picked up. After you have picked up the items you can see them in the inventory.
I was trying to add the feature where you can drop the items, but the items were dropping through the ground after the intantiation.
Items that are dropped by animals will disappear after 30 seconds, I used Coroutine for this.
Most of the items such as water, meat, silk and animal skin do not have a proper models. Those items are rendered in game as cubes with different colors.

## Inventory
Player can access the inventory by pressing TAB. In inventory you can see all of the items, tha you have picked up.
You can use the items by clicking on them, if they are useable of course.
There is also a crafting menu in the inventory window. At this point of the development of this game you can only craft the Airplane, which is the last objective.
Inventory has also the objective info and the crafting info. The objective info shows your next objective.
The crafting info shows you what items do you need to craft the item, that you are hovering over with your mouse.

## Weapon
Weapon can be equipped by clicking the weapon icon in the inventory menu, weapon can be unequipped by clicking the E button.
You can shoot the weapon by clicking the left mouse button. The shooting is made with RayCastHit. Ray is shooting from the fps camera forward.
When animal gets shot, there is a blood impact effect (particle system).

## Flashlight
Can be equipped / unequipped, tuned on / off.

## Objective mechanics
There are 12 objectives, first two are for picking up the rifle and the flashlight. The next 9 objectives are for searching for the airplane parts.
The last objective is to get back to the crashed airplane and fix it. You can see the objectives as they pop up in the upper left corner of the screen and in the inventory menu.

## UI
The game has bunch of different UIs. In game UI shows health, hunger, thirst, minimap, distance to the next objective, item info, objectives.
Main menu UI has the Play, How To and Quit buttons. By pressing the How To button you will see the controls. Death and Victory menus are almost identical.
Pause menu has 4 buttons: Resume, Restart, Main Menu, Quit. Those are pretty self explanatory. Inventory menu is also part of the UI.

# Used Technoligies
* Unity 2019.3.7f1
* Microsoft Visual Studio Community 2019
* Blender
* C#

# Used Tutorials
* [How to make Terrain in Unity](https://www.youtube.com/watch?v=MWQv2Bagwgk)
* [First Person Movement in Unity](https://www.youtube.com/watch?v=_QajrabyTJc)
* [Creating a Survival Game in Unity 2018](https://www.youtube.com/watch?v=JhkoYxuZ2UI&t=194s)
* [START MENU in Unity](https://www.youtube.com/watch?v=JhkoYxuZ2UI&t=194s)
* [PAUSE MENU in Unity](https://www.youtube.com/watch?v=JivuXdrIHK0&t=304s)
* [How to make a Minimap in Unity](https://www.youtube.com/watch?v=28JTTXqMvOU)

# Used Assets
* [First Person All-in-One (FREE)](https://assetstore.unity.com/packages/tools/input-management/first-person-all-in-one-135316)
* [Rifle (FREE)](https://assetstore.unity.com/packages/3d/props/guns/rifle-25668)
* [Super Spitfire (FREE)](https://assetstore.unity.com/packages/3d/vehicles/air/super-spitfire-53217)
* [Flashlight PRO (FREE)](https://assetstore.unity.com/packages/3d/props/tools/flashlight-pro-53053)
* [Tree Pack 2 (FREE)](https://assetstore.unity.com/packages/3d/vegetation/trees/tree-pack-2-67117)
* [Animal pack deluxe v2 (€4.46)](https://assetstore.unity.com/packages/3d/characters/animals/animal-pack-deluxe-v2-144071)

# Work Hours
| Date       | Duration | Task                                                                         |
| ---------- | -------- | ---------------------------------------------------------------------------- |
| 30.03.2020 | 8h       | Terrain, First person controller                                             |
| 03.04.2020 | 10h      | Gun, airplane, hunger, thirst, health, tiger, flashlight                     |
| 04.04.2020 | 8h       | Inventory                                                                    |
| 05.04.2020 | 8h       | Shooting, Inventory                                                          |
| 06.04.2020 | 8h       | Animal AI, info UI, stats UI, picking up items                               |
| 07.04.2020 | 8h       | New animals, AI, Inventory, Crafting                                         |
| 08.04.2020 | 1h       | Inventory Info                                                               |
| 15.04.2020 | 5h       | Animal AI, Animal angle adjustment fail                                      |
| 17.04.2020 | 5h       | Animal Animations, Animal angle adjustment success                           |
| 21.04.2020 | 5h       | Objectives, Airplane parts, minimap                                          |
| 22.04.2020 | 5h       | Objectives, Animal movement bug fixes, Main menu, Death scene, Victory scene |
| 24.04.2020 | 9h       | Bug fixes, Pause Menu, documentation                                         |
| 25.04.2020 | 1h       | Documentation                                                                |
| Total      | 81h      |                                                                              |

# Bugs
There are some bugs in this game, I did not have time to fix them all.

## Animals
Sometimes animals walk inside the ground, but very rarely. I think it happens after the collisions with trees.
Animals get stuck in the terrain. Especially when trying to run away from the player. Does not happen frequently when walking.

## Player
Player is kinda jumpy when climbing the hills.
Player can't die when falling from high up.

# Left out Features
I have left out bunch of features, that were supposed to be in this game.
Crafting, clothing, cooking. I've created the campfire and was able to light it and put it down, but did not have time to implement the cooking.
Weather effects on the player, as we are in Siberia and it's a winter. For this I needed to implement the clothing and more crafting.
I was able to implement the crafting, for example the campfire (you can still craft it in the game, if you can find wood).
It was challenging to cut down the trees that are painted by the terrain tools.
I also wanted to add the inventory to everything: campfire, animals, Airplane end so on.

# Overview
All in all I am satisfied with the game, even though it is little buggy. I did not have any previous experience with game development,
so I had to learn a lot. The learning took a lot of time, but I've learned a lot and I belive that I can make games now. At first I thought that
this course was useless for me, but at the end it was very fun to learn game programming.

# Grade suggestion
I suggest 5, because I have spent a lot of time on this game and the game has a lot of game mechanics. It's not perfect, but I've put a lot of work into it.