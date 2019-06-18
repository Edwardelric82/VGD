using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour
{
	// Animation Script
	private CharacterAnimation anim;
	
	// Movement variables
	private Vector3 destinationPosition;
	private float destinationDistance = 0.0f;
	private float minMove = 0.5f;
	private float maxMove = 500.0f;
	private float speed = 10f;
	
	void Start()
	{
		anim = GetComponent<CharacterAnimation>(); // Get the animation script
	}
	
	void OnEnable()
	{
		destinationPosition = transform.position; // Set the destinationPosition to your current location so you don't move on enable
	}
	
	// FixedUpdate is used for physics based movement
	void FixedUpdate()
	{
		MovementControl(); // Player movement function
	}
	
	private void MovementControl()
	{
		MovePlayer(); // Player Move function
		
		if (Input.GetMouseButton(0)) // If left mouse button is clicked or held down
		{
			RotatePlayer(); // Player Rotate function
		}
		destinationPosition.y = transform.position.y; // Set the destination Y position to your local Y position (allows you to move up ramps)
		destinationDistance = Vector3.Distance(destinationPosition, transform.position); // Distance between the player and where clicked
	}
	
	private void MovePlayer()
	{
		
		if (destinationDistance >= minMove && destinationDistance <= maxMove)// If the distance between the player and clicked is greater than the minimum range and less than the maximum range
		{
			GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.forward * speed * Time.deltaTime); // Move forward based on players Vector3
			anim._animRun = true; // Enable the run animation
			Debug.DrawLine(destinationPosition, transform.position, Color.cyan); // This draws a line in Scene View so you can see where you've clicked
		}
		else // If the distance between the player and clicked is less than the min range and less than the max range then continue
		{
			anim._animRun = false; // Disable the run animation
		}
	}
	
	public void RotatePlayer()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Set ray to the position of your mouse
		Plane playerPlane = new Plane(Vector3.up, transform.position); // Create a plane for the raycast
		float hitdist = 0.0f; // set a float for the position of your click
		if (playerPlane.Raycast(ray, out hitdist)) // If the Raycast has hit something (in this case, the mouse position) then continue
		{
			Vector3 targetPoint = ray.GetPoint(hitdist); // Set a Vector3 for position clicked
			destinationPosition = targetPoint; // Set destination position to position clicked
			GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(targetPoint - transform.position)); // Rotate player towards position clicked
		}
	}
}