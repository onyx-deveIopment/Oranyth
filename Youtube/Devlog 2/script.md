### Script

Hello and welcome to the second devlog for our game!

In this video, Charlie will go over how Andrew polished the visuals and simplified the UI to make it easier to implement.

One of the easiest ways Andrew improved the game’s visuals was by adding post-processing. To do this, he started by installing the Universal Render Pipeline (URP), since the Built-in Render Pipeline doesn’t support post-processing.

Once URP was set up, Andrew enabled post-processing on the main camera. Next, he created a new GameObject called "Post Processing" and added a Global Volume component to it. This allowed him to apply post-processing effects to the entire scene.

To control these effects, Andrew created a new Volume Profile, where he could adjust various settings. For this game, he kept things simple by adding Vignette and Chromatic Aberration to give the visuals a more polished look.

Here’s a comparison of the game with and without post-processing.

One of Andrew’s main goals for the UI was to make sure it scaled properly across different screen sizes. To achieve this, he created a Panel and set its Anchor Mode to Stretch so that it would resize dynamically.

Inside this panel, he added a Vertical Layout Group component. This component automatically arranges and spaces out UI elements, so all Andrew had to do was add the UI objects as children, and everything stayed neatly centered and aligned. He repeated this process for each UI page, creating a separate panel for each one.

With the layout done, the next step was making the buttons actually work.

To handle UI navigation, Andrew created a UIPanelController script. This script keeps a list of all the UI panels and includes a function to switch between them using their index. Any other script can reference this controller to change the UI.

Some buttons needed extra functionality, so Andrew made a MainMenuExtras script to handle those cases. For example, when the Start button is pressed, it loads the next scene, and when the Exit button is pressed, it closes the application.

Here’s how the final UI looks in action!

That’s all for this devlog! Thanks for watching, and be sure to check back for the next video, where Charlie will talk about how Andrew added power-ups to the game.











### OLD

Hello and welcome to the second devlog for our game!

In this video, I’ll go over how I polished the visuals and simplified the UI to make it easier to implement.

One of the easiest ways to improve your game’s visuals is by adding post-processing. To do this, I started by installing the Universal Render Pipeline (URP) because the Built-in Render Pipeline doesn’t support post-processing.

Once URP was set up, I enabled post-processing on the main camera. Next, I created a new GameObject called Post Processing and added a Global Volume component to it. This allows us to apply post-processing effects to the entire scene.

To control these effects, I created a new Volume Profile, where I could adjust various settings. For this game, I kept it simple by adding Vignette and Chromatic Aberration to give the visuals a more polished look.

Here’s a comparison of the game with and without post-processing.

One of my main goals for the UI was to make sure it scaled properly across different screen sizes. To achieve this, I created a Panel and set its Anchor Mode to Stretch so that it resizes dynamically.

Inside this panel, I added a Vertical Layout Group component. This component automatically arranges and spaces out UI elements, so all I had to do was add my UI objects as children, and everything stayed neatly centered and aligned. I repeated this process for each UI page, creating a separate panel for each one.

With the layout done, the next step was making the buttons actually work.

To handle UI navigation, I created a UIPanel Controller script. This script keeps a list of all the UI panels and has a function to switch between them using their index. Any other script can reference this controller to change the UI.

Some buttons needed extra functionality, so I made a Main Menu Extras script to handle those cases. For example, when the Start button is pressed, it loads the next scene, and when the Exit button is pressed, it closes the application.

Here’s how the final UI looks in action!

That’s all for this devlog! Thanks for watching, and be sure to check back for the next video, where I’ll talk about adding power-ups to the game.