using UnityEngine;
using System.Collections;

public class FirstPersonMove : MonoBehaviour {

	// Animation script
	private CharacterAnimation anim;
	
	// Rotation variables
	private float   rotY,
					rotX,
					sensitivity = 10.0f;
	
	// Speed variables
	private float   speed = 10f,
	 				speedHalved = 7.5f,
	 				speedOrigin = 10f;
	
	// Jump!
	private float distToGround;
	
	void Start()
	{
		anim = GetComponent<CharacterAnimation>(); // Get the animation script
	}
	
	// FixedUpdate is used for physics based movement
	void FixedUpdate ()
	{
		float horizontal = Input.GetAxis("Horizontal"); // set a float to control horizontal input
		float vertical = Input.GetAxis("Vertical"); // set a float to control vertical input
        MouseLook(); // Call the player look function which controls the mouse
		PlayerMove(horizontal,vertical); // Call the move player function sending horizontal and vertical movements
		Jump(); // Call the Jump function! Woot!
	}
	
	private void MouseLook()
	{
		rotX += Input.GetAxis("Mouse X")*sensitivity; // set a float to control Mouse X input
		rotY += Input.GetAxis("Mouse Y")*sensitivity; // set a float to control Mouse Y input
		rotY = Mathf.Clamp (rotY, -90f, 90); // Lock rotY to a 90 degree angle for looking up and down
		transform.localEulerAngles = new Vector3(0,rotX,0); // Rotate the player mode left and right
		Camera.main.transform.localEulerAngles = new Vector3(-rotY,0,0); // Rotate the camera up and down rather than the player model
	}
	
	private void PlayerMove(float h, float v)
	{
		if (h != 0f || v != 0f) // If horizontal or vertical are pressed then continue
		{
			if(h != 0f && v != 0f) // If horizontal AND vertical are pressed then continue
			{
				speed = speedHalved; // Modify the speed to adjust for moving on an angle
			}
			else // If only horizontal OR vertical are pressed individually then continue
			{
				speed = speedOrigin; // Keep speed to it's original value
            }
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + (transform.right * h) * speed * Time.deltaTime); // Move player based on the horizontal input
			GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + (transform.forward * v) * speed * Time.deltaTime); // Move player based on the vertical input
			anim._animRun = true; // Enable the run animation
		}
		else 	// If horizontal or vertical are not pressed then continue
		{
			anim._animRun = false; // Disable the run animation
		}
	}
	
	private void Jump()
	{
		if(Input.GetKeyDown(KeyCode.Space)) // If the Space bar is pressed down then continue
		{
			if(IsGrounded()) // If the player is grounded, this calls a boolean, then continue
			{
				GetComponent<Rigidbody>().velocity += 5f * Vector3.up; // add velocity to the player on vector UP
			}
		}
	}
	
	private bool IsGrounded()
	{
		return Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f); // Do a ray cast to see if the players collider is 0.1 away from the surface of something
	}
}