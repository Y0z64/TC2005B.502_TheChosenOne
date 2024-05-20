using UnityEngine;

public class DynamicObjectManager : MonoBehaviour
{
    public GameObject objectPrefab; // Reference to the prefab to be instantiated
    public Transform spawnPoint; // Reference to the spawn point
    private GameObject instantiatedObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CreateObject();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DestroyObject();
        }
    }

    void CreateObject()
    {
        if (objectPrefab != null && spawnPoint != null && instantiatedObject == null)
        {
            instantiatedObject = Instantiate(objectPrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("Object created at " + spawnPoint.position);
        }
        else
        {
            Debug.Log("Cannot create object. Either prefab or spawn point is not set, or object already exists.");
        }
    }

    void DestroyObject()
    {
        if (instantiatedObject != null)
        {
            Destroy(instantiatedObject);
            instantiatedObject = null; // Reset the reference
            Debug.Log("Object destroyed");
        }
        else
        {
            Debug.Log("No object to destroy");
        }
    }
}
