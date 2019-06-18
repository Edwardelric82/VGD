using UnityEngine;
using System.Collections;

namespace StarterKitMCA {
	public class GameController : MonoBehaviour {
		
		// GUI Textures
		[HideInInspector] // This hides the variables in the Inspector. remove this to change textures.
		public Texture gridBG, mouseMove, mouseLeft,mousePan, mouseScroll,
		cameraDefault, cameraFPS, cameraPan, cameraTop, WASD,
		fpsWASD, kill;
		
		// Show/Hide Cursor messages
		string fpsCursor;
		string cursorHide = "Press [L] to hide Mouse";
		string cursorShow = "Press [ESC] to show Mouse";
	
		// Player, Enemy & Camera
		public GameObject player;
		public GameObject cam;
		GameObject nearestEnemy;
		Vector3 camPos;
		Quaternion camRot;
		
		// Enemy Spawner
		public GameObject spawners;
	    
		// Movement & Camera Scripts
		ClickToMove click;
		DirectionalMove directional;
		FirstPersonMove firstPerson;
		CameraTopDown topDown;
		CameraTopDownPan topDownPan;
		CameraFirstPerson fps;
		
		// First Person boolean
		bool fpsMode, exitGame;
		
		// Spawn Enemy boolean
		private bool spawnEnemy;
		
		// Selection menu for Camera options
		int selCamInt = 0;
		string[] selCamStrings = new string[] {"Default", "Top Down", "Pan", "First Person"};
		
		// Selection menu for Movement options
		int selMoveInt = 0;
		string[] selMoveStrings = new string[] {"None", "Click-To-Move", "WASD", "*First Person"};
	    
		void Start ()
		{	
			// Player movement scripts
			click = player.GetComponent<ClickToMove>();
			directional = player.GetComponent<DirectionalMove>();
			firstPerson = player.GetComponent<FirstPersonMove>();
			
			// Camera movement scripts
			topDown = cam.GetComponent<CameraTopDown>();
			topDownPan = cam.GetComponent<CameraTopDownPan>();
			fps = cam.GetComponent<CameraFirstPerson>();
			
			// Set variables for initial camera position and rotation
			camPos = cam.transform.position;
			camRot = cam.transform.rotation;
		}
		
		void OnGUI()
		{
			GUI.backgroundColor = new Color(0.6f,0.7f,0.9f);
			GUI.DrawTexture(new Rect(5,5,230,245),gridBG);
			
			
			// * Camera Options * //
			GUI.Box(new Rect(10,10,220,75),"Camera Options");
			selCamInt = GUI.SelectionGrid(new Rect(15, 30, 210, 50), selCamInt, selCamStrings, 2);
			
			// * Movement Options * //
			GUI.Box(new Rect(10,90,220,75),"Movement Options");
			selMoveInt = GUI.SelectionGrid(new Rect(15, 110, 210, 50), selMoveInt, selMoveStrings, 2);
			
			// * AI Options * //
			GUI.Box(new Rect(10,170,220,75),"Ai Spawner");
			if(GUI.Button(new Rect(15,190,210,50), "Spawn Enemy"))
			{
				spawnEnemy = true;
			}
			else
			{
				spawnEnemy = false;
			}
			
			// IMAGES //
			GUI.Box(new Rect(Screen.width - 140, Screen.height - 125, 125,100),"Movement Controls");
			GUI.Box(new Rect(Screen.width - 280, Screen.height - 125, 125,100),"Camera Controls");
			GUI.Box(new Rect(Screen.width - 420, Screen.height - 125, 125,100),"Kill Nearest Enemy");
			GUIImages(); // Call images based on selection grids
			
			// KILL ENEMY BUTTON //
			if(GUI.Button(new Rect(Screen.width - 395, Screen.height - 105, 75,75),kill)) // If this button is pressed
			{
				Destroy(FindNearestEnemy()); // Destroy game object, this call has a return of a game object, either found or null
			}
			
			// * Message * //
			if(fpsMode)
			{
				GUI.Label(new Rect(Screen.width / 2 -50, Screen.height / 2 +100,200,25), fpsCursor);
			}
			
			// * Exit Game * //
			ExitGame(); // Display exit game GUI
		}	
	
		void Update ()
		{
			CameraSelection();
			MovementSelection();
			SpawnAi();
			FPSMode();
		}
		
		void FPSMode()
		{
			if(fpsMode)
			{
				if(Input.GetKeyDown(KeyCode.L))
				{
					fpsCursor = cursorShow;
					Screen.lockCursor = true;
				}
				if(Input.GetKeyDown(KeyCode.Escape))
				{
					fpsCursor = cursorHide;
					Screen.lockCursor = false;
	            }
	        }
	        else
	        {
	            fpsCursor = cursorHide;
	            Screen.lockCursor = false;
	        }
	    }
		
		void CameraSelection()
		{
			// See OnGUI() section for *Camera Options. The following IF statements are based on a SelectionGrid (radio buttons)
			if (selCamInt == 0)
			{
	            Camera.main.transform.position = camPos;
				Camera.main.transform.rotation = camRot;
	        }
			if (selCamInt == 1)
			{					
				topDown.enabled = true;
			}
			else
			{
				topDown.enabled = false;
			}
			if (selCamInt == 2)
			{	
	            topDownPan.enabled = true;
			}
			else
			{
				topDownPan.enabled = false;
			}
			if (selCamInt == 3)
			{
				selMoveInt = 3;
				fpsMode = true;
				fps.enabled = true;
				firstPerson.enabled=true;
			}
			else
			{
				fpsMode = false;
				fps.enabled = false;
				firstPerson.enabled = false;
	        }
		}
		
		void MovementSelection()
		{
			// See OnGUI() section for *Movement Options. The following IF statements are based on a SelectionGrid (radio buttons)
			if(selMoveInt == 0)
			{
	        }
	        if(selMoveInt == 1)
			{
	            click.enabled = true;
			}
			else
			{
				click.enabled = false;
			}
			if(selMoveInt == 2)
			{
				directional.enabled = true;
	        }
	        else
	        {
				directional.enabled = false;
	        }
	        if(selMoveInt != 3)
	        {
				fps.enabled = false;
	        }
	    }
	    
	    
	    void SpawnAi()
		{
	        if(spawnEnemy)
	        {
	        	spawners.GetComponent<SpawnEnemy>().SpawnRandomEnemy(); // Spawn a random enemy on the map
	        }
	    }
	    
	    // This gameobject function finds the closest enemy to the player and returns that gameobject
	    GameObject FindNearestEnemy()
		{
			GameObject player = GameObject.Find("Player"); // Find the player game object
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Find all game objects with the tag Enemy
			float distance = Mathf.Infinity; // Set a float with infinit distance
			foreach (GameObject enemy in enemies) // For every game object within the enemies game object do the following
			{
				Vector2 diff = player.transform.position - enemy.transform.position; /// Set a vector distance from the enemy to the player
				float curDistance = diff.sqrMagnitude; // Apply squared magnitude to the vector for distance comparison
				if (curDistance < distance) // If the current distance from the enemy is less than the set distance continue
				{
					nearestEnemy = enemy; // Set the nearestEnemy game object to equal the enemy that was located
					distance = curDistance; // Set the distance to the current distance.
				}
			}
			return nearestEnemy; // Return the nearestEnemy game object that was found, if nothing was found this return becomes NULL
		}
		
		void GUIImages()
	    {
	    	// This function displays GUI Textures based on specific variables listed in OnGUI()
	        if(fpsMode)
	        {
	            GUI.DrawTexture(new Rect(Screen.width-140,Screen.height-100,75,75), fpsWASD);
				GUI.DrawTexture(new Rect(Screen.width-80,Screen.height-100,75,75), mouseMove);
				GUI.DrawTexture(new Rect(Screen.width-280,Screen.height-100,75,75), cameraFPS);
				GUI.DrawTexture(new Rect(Screen.width-220,Screen.height-100,75,75), mouseMove);
	        }
	        if(selMoveInt == 2)
	        {
				GUI.DrawTexture(new Rect(Screen.width-115,Screen.height-100,75,75), WASD);
			}
			if(selMoveInt == 1)
			{
	            GUI.DrawTexture(new Rect(Screen.width-115,Screen.height-100,75,75), mouseLeft);
	        }
			if (selCamInt == 1)
	        {	
				GUI.DrawTexture(new Rect(Screen.width-280,Screen.height-100,75,75), cameraTop);
				GUI.DrawTexture(new Rect(Screen.width-205,Screen.height-105,50,50), mouseLeft);
				GUI.DrawTexture(new Rect(Screen.width-220,Screen.height-75,50,50), WASD);
	        }
			if (selCamInt == 2)
	        {	
				GUI.DrawTexture(new Rect(Screen.width-280,Screen.height-100,75,75), cameraPan);
				GUI.DrawTexture(new Rect(Screen.width-205,Screen.height-105,50,50), mousePan);
				GUI.DrawTexture(new Rect(Screen.width-220,Screen.height-75,50,50), mouseScroll);
	        }
			if (selCamInt == 0)
			{
				GUI.DrawTexture(new Rect(Screen.width-250,Screen.height-100,75,75), cameraDefault);
			}
		}
		
		void ExitGame()
		{
			GUI.backgroundColor = Color.red;
			if(GUI.Button(new Rect(Screen.width-27,2,25,25),"X")) // If the X button is pressed
			{
				exitGame = true;
			}
			if(exitGame) // If exitGame is true then display the GUI below
			{
				GUI.Box(new Rect(Screen.width / 2 - 120, Screen.height / 2 - 40, 240,80),"Are you sure you want to quit?");
				GUI.backgroundColor = Color.red;
				if(GUI.Button(new Rect(Screen.width/2-105,Screen.height/2,100,25),"Yup")) // If YES is pressed
				{
					Application.Quit(); // Quit the game
				}
				GUI.backgroundColor = Color.green;
				if(GUI.Button(new Rect(Screen.width/2+5,Screen.height/2,100,25),"Nope!")) // if NO is pressed
				{
					exitGame = false;
				}
			}
		}
	}
}