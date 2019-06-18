  //*************************************************************//
 //* Thank you for buying my Start Kit - Movement, Camera & AI *//
//*************************************************************//
//
// The reason I made this Starter Kit is for people looking for the basics on how to get started with game dev.
// If I had this when I first started I feel like I would have had a much faster start at everything.
// This is why I made this, hopefully I can help other people who feel like it's too hard realize that it's not as hard as it looks.
//
// This Starter Kit is meant to get you started on the very basics of player movement, camera movement, and basic enemy AI.
//
// Everything you see is done using Unity FREE so Pro is not required.
//
// I suggest you look through the code and reverse engineer as much as you can to learn.
//
  ///////////////////
 //* TEST IT OUT *//
///////////////////
//
// First open up the scene the starter kit comes with and play around with it.
// Scene is located under the folder: /StarterKit-MoveCamAI/Example/Scene/
//
  ////////////////////////////
 //* HOW TO SETUP A SCENE *//
////////////////////////////
//
// STEP 1: 	In a brand new scene, set up a level to navigate. Use cubes for walls and a plane or terrain for your ground.
//			Once you've created a level that that has a small area to navigate, you're ready to add a player and an enemy.
//				*NOTE: Don't forget to add a directional light, otherwise you might not see very well! :P
//
// STEP 2: 	Now add a player, under the folder /StarterKit-MoveCamAI/StarterKit/Prefabs/ you'll find a Player prefab.
//			Drag and drop the player into your scene. :)
//
// STEP 3: 	You'll notice that your player has several scripts that are disabled, these are the movement control scripts.
//			Select whichever script you would like to use, for now lets use the Directional Move script, check that to be Enabled.
//
// STEP 4: 	Next we need to add a script to the camera. Select your Main Camera in your scene, click Add Component > Scripts and choose one of the Camera scripts
//			For this demonstration, I suggest we go with the CameraTopDown script to your camera.
//
// STEP 5: 	Test it! Hit play, use WASD to move your character around. You'll notice in the scene view there are blue squares following you, these are breadcrumbs, used for enemy AI.
//
// STEP 6: 	Go back to the Prefab folder and drag in an Enemy to your scene. The enemy already has the EnemyAI script added, and it doesn't require you to do anything, you're done!
//
// STEP 7: 	Test it! Now you can hit Play, run around walls and the enemy will follow you.
//
// That's it! You've now moved your player, moved the camera, and made an enemy follow you.
// All these scripts are located under /StarterKit-MoveCamAI/StarterKit/Scripts and you're free to look over them and make any modifications you need to make it work the way you want.
// For more info, watch these videos below!
//
  //////////////
 //* VIDEOS *//
//////////////
//
// Here's what it does: http://youtu.be/9UaMk712HAo
//
// Here's how to use it: http://youtu.be/pS-1l44iOcQ
//
  /////////////////////////////////////////
 //* Not so frequently asked questions *//
/////////////////////////////////////////
//
// 1.) My camera isn't following the player, it moves up and down but doesn't rotate or look through the eyes of my character.
//		A) You need to have a MainCamaera with the CameraFirstPerson script added to it.
//
// 2.) I added the movement script to my player but I'm getting an error when I try to move.
//		A) Make sure you added the LastPosition script to your player. Also make sure the LastPosition script has the LastPos prefab added to it.
//
// 3.) How does the button for killing all nearby enemies work?
//		A) Open the GameController script and you'll see a script called FindNearestEnemy()
//		   You can copy that script anywhere and use Destroy(FindNearestEnemy()) to kill any nearby enemies
//
// 4.) I have more questions and problems but this FAQ didn't help me at all.
//		A) Send me an e-mail or tweet, find that information below! :)
//
// ** CONTACT ** //
//
// Site: www.zerologics.com
// E-Mail: mike.desjardins@outlook.com
// Twitter: @ZeroLogics
//
// I'm fairly active on Twitter and through e-mail, so I'm not too hard to get a hold of
//
//
//
//  
// TTTTTT HH  HH     AA     NN   NN KK  KK   YY    YY   OO   UU   UU  !!                                  
//   TT   HH  HH    AAAA    NNNN NN KK KK     YY  YY  OO  OO UU   UU  !!                                   
//   TT   HHHHHH   AA  AA   NN NNNN KKK        YYYY   OO  OO UU   UU  !!                                      
//   TT   HH  HH  AAAAAAAA  NN  NNN KK KK       YY    OO  OO UU   UU                                    
//   TT   HH  HH AA      AA NN   NN KK  KK      YY      OO    UUUUU   !!                                   
//
//
