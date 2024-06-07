using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DynamicObjectManager : MonoBehaviour
{
    public GameObject objectPrefab;
    public Transform spawnPoint;
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

    public DynamicObjectState GetState()
    {
        DynamicObjectState state = new DynamicObjectState();
        state.objectExists = instantiatedObject != null;
        if (instantiatedObject != null)
        {
            state.objectPosition = new float[] { instantiatedObject.transform.position.x, instantiatedObject.transform.position.y, instantiatedObject.transform.position.z };
            state.objectRotation = new float[] { instantiatedObject.transform.rotation.eulerAngles.x, instantiatedObject.transform.rotation.eulerAngles.y, instantiatedObject.transform.rotation.eulerAngles.z };
        }
        return state;
    }

    public void SetState(DynamicObjectState state)
    {
        if (state.objectExists)
        {
            instantiatedObject = Instantiate(objectPrefab, new Vector3(state.objectPosition[0], state.objectPosition[1], state.objectPosition[2]), Quaternion.Euler(state.objectRotation[0], state.objectRotation[1], state.objectRotation[2]));
        }
        else
        {
            if (instantiatedObject != null)
            {
                Destroy(instantiatedObject);
            }
        }
    }
}
