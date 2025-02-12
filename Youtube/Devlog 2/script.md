### Script

Hello and welcome to the second devlog for our new game.

In this video I will talk about how I added polish and how I made the UI simple for me to implament.

An easy way to make your game look nice is by adding post processing. To add post processing I started by installing the Universal Render Pipeline. The Build in render pipeline does not have post procesing which is why we have to install the URP. Next we have to go to our camera and enable post processing. Now that we have post processing enabled, we have to add some effects. To do this we create a new gameobject and call it Post Processing. Then I added a global volume component to this object. Next we create a new volume profile where we can add the effects and change the values on those effects. For this game I kept it simple and only added a vignet and some some chromatic aberation.

This is what the game looks like with and without post processing.

Now for the UI, one of my main conserns for this UI was its ability to scale and look correct on most sizes. To do this is created