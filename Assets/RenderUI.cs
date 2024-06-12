using UnityEngine;
using SlimUI.ModernMenu;


public class RenderUI : MonoBehaviour
{
    private readonly UISettingsManager uiSettingsManager;

    private CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        // Try to get the CanvasGroup component
        
        // If there isn't one already, add it
        if (!TryGetComponent<CanvasGroup>(out canvasGroup))
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("ShowHUD") == 0)
        {
            // Make the UI transparent
            canvasGroup.alpha = 0;
        }
        else
        {
            // Make the UI opaque
            canvasGroup.alpha = 1;
        }
    }
}