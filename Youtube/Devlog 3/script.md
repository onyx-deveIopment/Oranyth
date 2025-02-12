# Script

Hello and welcome to the third devlog about our new game!

In this video, I will be talking about how I added power-ups to my game.

The first thing I did was modify the Collectible script so it could be inherited by other scripts. This allows me to easily add new types of collectibles.

After that, I created a new script for the main dot collectible. By inheriting from the base Collectible class, I can easily implement the collection functions. The base class handles detecting when an object is collected and runs a function when that happens. The script that inherits from it can override the OnCollect function to make it do what I need—like adding or removing time.

Now that everything is working as it was before, I can start adding new types of collectibles.

The first new collectible I added is a time freeze power-up. The way this works is simple: the script inherits from the Collectible class and overrides the OnCollect function. To stop time, I added a float variable to the GameController. In the GameController, I make it check if that variable is less than zero before lowering the time. Then, in the collectible script, I override the OnCollect function to add time to the float in the GameController.

Now that I have two types of collectibles, I need a better way to spawn them. I opened the Spawner script and created a new class called SpawnableObject. This class has an int variable for the chance of spawning (out of 100) and a GameObject variable to reference the prefab. I then created a list of SpawnableObjects so I could modify what spawns directly in the Inspector. When an object is spawned, the script picks a random one from the list, influenced by its spawn chance variable.

Here’s the new system in action. I increased the spawn rate for the time freeze power-up so you can see how it works.

Now that this system is working, I can add the next power-up I had planned.

This power-up is called the Rainbow Power-up. When collected, it gives the player a short period of time where they can collect any color. To implement this, I added a float variable to the GameController called Rainbow Time. When a normal dot is collected, it checks if this variable is above zero. If it is, the dot always registers as a correct collection. 

I also added a script to the PlayerController to change the player’s sprite, giving it a rainbow effect when the power-up is active. Then, I created another script that inherits from the Collectible script. Like the time freeze script, when collected, it adds time to the Rainbow Time variable in the GameController.

I added this power-up to the spawner system, and here it is in action—with increased spawn chances for demonstration.

That’s all for this video! Thanks for watching.