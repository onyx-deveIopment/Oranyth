### Script

Hello and welcome to the first devlog for our new game! The idea behind the game is simple yet has a lot of potential for expansion. The goal is to collect the right color. When the correct color is collected, the timer increases. But be careful – collecting the wrong color will cause you to lose time. And since the timer is always ticking down, you need to move quickly!

In this video, Charlie will be explaining how the game was made, starting with the Player Controller, which was developed by Andrew.

The player controller in this game is simple. The player is just a circle that moves around. So the first thing Andrew did was install Unity’s Input System package. After that, he created a basic Input Action asset to handle movement with a Vector2 input.

Next, Andrew created a new game object called "Player" and added a circle sprite as a child so the player would be visible in the game. He then added the PlayerInput component to the Player object, making it easy to connect the input actions to the script. He also added a 2D circle collider and set it as a trigger to detect when the player touches an object to collect it.

Before moving to the player controller script, Andrew tagged the Player object with the "Player" tag. Then, he created a script called PlayerController and attached it to the Player object. The script is fairly simple — Andrew set up a few variables for acceleration, max speed, and movement damping, then created a function to get the movement input using the Input Actions.

In the FixedUpdate function, Andrew used the input variable, multiplied it by acceleration, deltaTime, and damping to calculate the player’s velocity. After that, he clamped the velocity to the max speed and updated the player’s position based on the velocity. Finally, he connected the input function to the Player Inputs and tested the movement.

With the player working, let’s talk about the collectible color dots.

For the collectibles, Andrew created a new object called "Collectible Dot" and added a circle sprite. Then, he created a script called Collectible and added it to the collectible object. In the script’s Start function, he added a Rigidbody2D and a CircleCollider2D to the collectible. When the player touches the collectible, the PlayerController script calls the onCollected function in the Collectible script. The collectible checks if it's the right color and awards time based on whether it’s correct or not.

Next, Charlie will quickly go over the other parts of the game.

First is the ColorController script. This handles the player's current color and when to change it.

Then there's the CollectibleSpawner script. This one spawns collectibles over time for the player to collect.

Finally, there's the GameController script. This manages the timer, keeps track of the score, and handles pausing and resuming the game.

With all these scripts in place, the prototype is now complete! Thanks for watching, and stay tuned for the next devlog where Charlie will talk about adding polish and overhauling the UI Andrew built!











### OLD

Hello and welcome to the first devlog for our new game! The idea behind our game is simple yet has a lot of potential for expansion. The goal is to collect the right color. When you collect the correct color, the timer increases. But be careful – if you collect the wrong color, you'll lose time. And since the timer is always ticking down, you need to move quickly!

In this video, I’ll be explaining how the game was made, starting with the Player Controller.

The player controller in this game is simple. The player is only a circle that moves around. So the first thing I did was install Unity’s Input System package. After that, I created a basic Input Action asset to handle movement with a Vector2 input.

Next, I created a new game object called "Player" and added a circle sprite as a child so we can see the player in the game. I then added the PlayerInput component to the Player object, which helps connect the input actions to the script easily. I also added a 2D circle collider and set it as a trigger so we can detect when the player touches an object to collect it.

Before moving to the player controller script, I made sure to tag the Player object with the "Player" tag. Then, I created a script called PlayerController and attached it to the Player object. This script is pretty simple. I set up a few variables for acceleration, max speed, and movement damping. I then created a function to get the movement input using the Input Actions.

In the FixedUpdate function, I used the input variable, multiplied it by acceleration, deltaTime, and damping to calculate the player’s velocity. After that, I clamped the velocity to the max speed and updated the player’s position based on the velocity. Finally, I connected the input function to the Player Inputs and tested the movement.

Now that the player works, let’s talk about the collectible color dots.

For the collectibles, I created a new object called "Collectible Dot" and added a circle sprite. Then, I created a script called Collectible and added it to the collectible object. In the script’s start function, I added a Rigidbody2D and a CircleCollider2D to the collectible. When the player touches the collectible, the PlayerController script calls the onCollected function in the Collectible script. The collectible checks if it's the right color and awards time based on whether it’s correct or not.

Next, I’ll quickly go over the other parts of the game.

First is the ColorController script. This handles the player's current color and when to change it.

Then we have the CollectibleSpawner script. This one spawns collectibles over time for the player to collect.

Finally, there’s the GameController script. This one manages the timer, keeps track of the score, and handles pausing and resuming the game.

With these scripts in place, the prototype is now complete! Thanks for watching, and stay tuned for the next devlog where I’ll talk about adding polish and overhauling the UI!