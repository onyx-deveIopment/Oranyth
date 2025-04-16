# Script

Hello and welcome to the third devlog about our new game!

In this video, Charlie will be talking about how Andrew added power-ups to the game.

The first thing Andrew did was modify the Collectible script so it could be inherited by other scripts. This made it easy to add new types of collectibles without rewriting a lot of code.

After that, he created a new script specifically for the main dot collectible. By inheriting from the base Collectible class, Andrew could implement the collection functionality easily. The base class handles detecting when an object is collected and runs a function in response. The inherited script overrides the OnCollect function to define custom behavior—like adding or removing time.

With that system in place and everything still working as before, Andrew was ready to add new collectible types.

The first new collectible he added was a Time Freeze power-up. The setup is straightforward: the script inherits from Collectible and overrides the OnCollect function. To make time stop, Andrew added a float variable to the GameController. In that controller, he made the timer check whether that variable was less than zero before counting down. Then, in the collectible script, he updated the OnCollect function to add time to the float in the GameController.

Now that there were two types of collectibles, Andrew needed a better way to manage spawning them. So, he went into the Spawner script and created a new class called SpawnableObject. This class includes an int for spawn chance (out of 100) and a reference to the GameObject prefab. Then, he made a list of SpawnableObject items, allowing him to tweak what spawns and how often—right from the Inspector. When an object is spawned, the script picks a random one based on its spawn chance.

Here’s the new system in action, with an increased spawn rate for the Time Freeze power-up so you can see how it works.

With that working, Andrew moved on to the next power-up: The Rainbow Power-up.

When collected, this power-up gives the player a short period during which they can collect any color. To implement it, Andrew added a float variable called RainbowTime to the GameController. Then, when a normal dot is collected, it checks if RainbowTime is above zero. If it is, the dot is treated as a correct collection, no matter the color.

Andrew also updated the PlayerController to include a rainbow effect for the player’s sprite while the power-up is active. Then, just like the other power-up, he created a script that inherits from Collectible, and when it’s collected, it adds time to the RainbowTime variable in the GameController.

He added this new power-up to the spawner system, and here it is in action—with increased spawn chances for demonstration.

That’s all for this video! Thanks for watching, and we’ll see you in the next devlog!











### OLD

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