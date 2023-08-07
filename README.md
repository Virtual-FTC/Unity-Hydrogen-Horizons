# Virtual-FTC Constructor Kit
see this video on how to use the "kit" https://www.youtube.com/watch?v=0nmN_ruTxjg
This repo contains a constructor kit for building the field, robots, etc.

# Getting Started

(In your own branch) either open the FieldTemplate scene or create a new scene using the VRS Field template (available in the "New Scene" window).

# Understanding the Basics

The intent of this kit is to provide modular parts that make it easy to design field layouts that work "right out of the box". 

The key aspects of the kit are these:

* Scoring Object Types
* Score Object Locations
* Scoring Guides
* Goals
* Indexes
	* Round Index
	* Score Index
	* Material Index
* Events (for tracking "one-off" events like a button press)
* Drives (for translating vector value events into motion, such as a thumbstick)

# Scoring Object Types

Scoring Object Types can be created two ways:

1. From the main menu, select VRS > Scoring > Create Scoring Object Type

This is the preferred way since it walks you through all that is needed to create a Scoring Object Type

2. Right click in the Project Window to create an asset, and select Scoring > Scoring Object Type from the top of the pop-up menu. The Scoring Object Types need to live in the same folder 

## What's in a Scoring Object Type?

Right now Scoring Object Types have an associated prefab. 

*Why don't we just use the prefab?*

The Scoring Object Type is a flexible data container that allows for future information that might be relevant to the game. For instance, there might be a type that uses multiple prefabs, chosen randomly. Or we might want to include information that restricts the type to a certain round, or set of rounds. By building the system around a flexible data container, we allow for these future changes. 

## What else uses the Scoring Object Type?

Multiple systems dynamically draw on the Scoring Object Types in the SpawnableObjects folder. See Scoring Guides and Scoring Object Locations below.

# Scoring Object Locations

Scoring Object Locations are how Scoring Object Types are placed on the field. Instead of placing the objects yourself by dragging prefabs out, use the @SpawnObjectManager under the Management hierarchy. 

## Creating Scoring Object Locations

First you need to create a folder for the locations. Each field configuration can have its own folder, for instance 2020Field, 2020FieldTestSetup, 2021Field, etc. This allows you to dynamically swap between field configurations. 

You can create Scoring Object Locations in two ways:

1. From the menu VRS > Scoring > Create Scoring Object Location
2. Right click in the Project Window and choose Scoring > Scoring Object Location

## Different Scoring Object Locations Types

When you create a Scoring Object Location, you can choose which type of Location:

* Specific Points places objects at specific points 
* Random Over Area places objects randomly distributed within a cube rectangular area 
* Random over Points places objects randomly over points
* Stacked at a Point places objects at a point, either all at once or spawned one at a time

## Automatic Tape

You can choose to have the Scoring Object Location automatically place tape on the field. NOTE: it will create a piece of tape when it hovers over any collider with the "Field" tag. 

# Scoring Guides

Scoring Guides allow you to set the scores per Scoring Object Type, per round. To properly set the rounds, you need to have a Round Index (see below), and you need to have your Scoring Object Types defined. 

Once you have these ready, and have created a Scoring Guide, click the button on the Scoring Guide to load the Scoring Object Types, and set the score per type, per round.

You can use Scoring Guides in any Goal Object (see below), to have specific Scoring Guides for each object. 

## Creating a Scoring Guide

To create a Scoring Guide, right click in the project window and select Scoring > Scoring Guide from above. 

# Goals

Goals receive prefab objects that are linked to Scoring Object Types. The Scoring Object Type has a button that will set the tags of the prefabs; the prefabs need to have the same tag as the name of the Scoring Object Type. (NOTE: when you create the Scoring Object Type via the VRS menu, it automatically sets the tags.)

## Creating a Goal

To create a goal, select in the menu VRS > Goals > Create Cube Goal or Create Sphere Goal

## Automatic Tape

The Cube Goal has the option to automatically create tape around the borders. It will create tape borders on any object that has the "Field" tag. You can choose how many sides the tape should show up on.

# Indexes

Indexes are data objects that let you easily track other kinds of data. Instead of tracking the individual objects, the index allows you to have a single reference. 

## Round Index

The Round Index allows you to set the number of rounds, the names of the rounds, and the length of the rounds. 

## Score Index 

The Score Index lets you track which team receives a score.

## Material Index

The Material Index lets you track various materials that are commonly used in the scene. 

# Events

Game Events allow you to connect single occurences together across the scene. They can be placed in prefabs, allowing you to connect objects whether or not they are in the same scene. 

Events need a Game Event Receiver component on the object that needs the connection. The sender uses a Unity Event (or other means) to run the Raise() method on the Game Event. 

The @InputManager has an example of the Game Event, while the TempRobot has an example of the receiver. 

# Drives

Drives work similarly to events, but they transfer Vector or Float data, allowing objects to drive each other without a direct connection. 

Drives can either work on Rotation, Translation, Force, Torque, Rigidbody Move, or Rigidbody Rotation. 

Drives allow a single action to drive multiple objects, such as wheels. 
