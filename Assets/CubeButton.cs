using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeButton : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}
