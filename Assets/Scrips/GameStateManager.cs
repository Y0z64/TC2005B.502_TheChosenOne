using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameStateManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public DynamicObjectManager dynamicObjectManager;

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
        GameState state = new()
        {
            playerState = playerMovement.GetState(),
            dynamicObjectState = dynamicObjectManager.GetState()
        };

        BinaryFormatter formatter = new();
        string path = Application.persistentDataPath + "/gamestate.save";
        FileStream stream = new(path, FileMode.Create);

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

            playerMovement.SetState(state.playerState);
            dynamicObjectManager.SetState(state.dynamicObjectState);

            Debug.Log("Game Loaded from " + path);
        }
        else
        {
            Debug.Log("No save file found at " + path);
        }
    }
}
