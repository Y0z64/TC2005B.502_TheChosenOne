using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // The prefab to spawn, assignable from the Unity Inspector
    public GameObject objectToSpawn;
    // The distance in front of the object where the new object will be spawned
    public float spawnDistance = 2.0f;

    void Update()
    {
        // Check if the 'E' key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnObject();
        }
    }

    public void SpawnObject()
    {
        // Ensure there's an object to spawn
        if (objectToSpawn != null)
        {
            // Calculate the spawn position in front of the object and elevate it by 7 units
            Vector3 spawnPosition = transform.position + transform.forward * spawnDistance + new Vector3(0,1,0);
            // Instantiate the object at the spawn position
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No object assigned to spawn!");
        }
    }
}