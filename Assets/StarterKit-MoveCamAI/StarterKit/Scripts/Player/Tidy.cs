using UnityEngine;
using System.Collections;

public class Tidy : MonoBehaviour {

	public float tidyDelay = 2.0f;

	void Start () 
	{
		Destroy(gameObject, tidyDelay); // Destroy this game object in a specific amount of time using tidyDelay float
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue; // Set Gizmo color to blue
		Gizmos.DrawCube(transform.position,transform.localScale); // Draw a cube at the position of this game object and scale it the same size of this game object
	}
}
