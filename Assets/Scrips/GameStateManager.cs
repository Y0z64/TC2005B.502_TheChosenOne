using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class GameState
{
    public Vector3 playerPosition;
    public List<PrefabState> prefabStates;
}

[System.Serializable]
public class PrefabState
{
    public string prefabIdentifier;
    public Vector3 position;
}

public class GameStateManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public List<GameObject> prefabTemplates; // Assign prefabs in the inspector
    private Dictionary<string, GameObject> prefabDictionary;

    void Awake()
    {
        // Initialize prefab dictionary for quick access
        prefabDictionary = new Dictionary<string, GameObject>();
        foreach (var prefab in prefabTemplates)
        {
            prefabDictionary[prefab.name] = prefab;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            LoadGame();
        }
    }

    public void SaveGame()
    {
        GameState state = new GameState
        {
            playerPosition = playerMovement.transform.position,
            prefabStates = new List<PrefabState>()
        };

        // Assuming you have a way to track spawned prefabs
        foreach (var prefab in FindObjectsOfType<GameObject>()) // Simplified; use a specific method to track your prefabs
        {
            if (prefabDictionary.ContainsKey(prefab.name))
            {
                state.prefabStates.Add(new PrefabState { prefabIdentifier = prefab.name, position = prefab.transform.position });
            }
        }

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gamestate.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, state);
        stream.Close();

        Debug.Log("Game Saved to " + path);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/gamestate.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameState state = formatter.Deserialize(stream) as GameState;
            stream.Close();

            playerMovement.transform.position = state.playerPosition;

            // Clear current prefabs
            foreach (var prefab in FindObjectsOfType<GameObject>())
            {
                if (prefabDictionary.ContainsKey(prefab.name))
                {
                    Destroy(prefab);
                }
            }

            // Spawn prefabs at saved positions
            foreach (var prefabState in state.prefabStates)
            {
                if (prefabDictionary.TryGetValue(prefabState.prefabIdentifier, out var prefabTemplate))
                {
                    Instantiate(prefabTemplate, prefabState.position, Quaternion.identity);
                }
            }

            Debug.Log("Game Loaded from " + path);
        }
        else
        {
            Debug.Log("No save file found at " + path);
        }
    }
}