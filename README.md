All the code in this repository is mine but it is HEAVILY based on a tutorial from Catlike Coding, which can be accessed by this link:<br/>
https://catlikecoding.com/unity/tutorials/basics/building-a-graph/<br/>


![](https://media.giphy.com/media/EGkq7z3OleXqXNQ1gH/giphy.gif)

# GraphsWithCubes (Unity)
Unity 2021.1.23f project which is a simple animated sine wave using cubes that change color based on their position.<br/>

To replicate this behaviour on a project of yours do the following:<br/>
1- Be sure to have a project using the same Unity version as I did (cannot guarantee it works on other versions, but it should);<br/>
2- Be sure your project uses Unity default Render Pipeline (not URP, HDRP or SRP);<br/>
3- Grab the content on the "Assets" folder and put it in your project;<br/>
4- Apply the "Graph" script on a object inside your scene and reset it's Transform (yes, it only works when the object is centered in the world);<br/>
5- Assing the "Point" prefab on the "Point Prefab" property of the Graph object;<br/>
6- Set the "Resolution" according to a number that makes you happy (this is the amount of cubes your graph will have);<br/>
7- Play the Editor and it should work.

Interesting things you can do with this repo:<br/>
1- Change the function which maps the cubes position<br/>
2- Change the cubes shader<br/>
3- Make the graph movable and/or scalable<br/>
4- Anything else you might like to do! :D
