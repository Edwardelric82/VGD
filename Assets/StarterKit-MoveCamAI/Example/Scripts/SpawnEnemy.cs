using UnityEngine;
using System.Collections;

namespace StarterKitMCA{
	public class SpawnEnemy : MonoBehaviour
	{
	
		public GameObject enemy; // Enemy prefab
		private Transform [] spawners; // Spawner transforms
		
		void Start()
		{
			spawners = GetComponentsInChildren<Transform> (); // Get all child transforms within the spawners
		}
		
		public void SpawnRandomEnemy()
		{
			int rand = Random.Range(0, spawners.Length); // Set a random int from 0 to the amount of children within spawners
			Transform spawn = spawners[rand]; // Set a transform equaled to the transform with the random int as the array selection
			Instantiate(enemy, spawn.transform.position, enemy.transform.rotation); // Create a game object where the spawn location is with the rotation of the prefab
		}
	}
}