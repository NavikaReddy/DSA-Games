using UnityEngine;
using UnityEngine.UI;

public class WorldPositionButton : MonoBehaviour
{
    private RectTransform rectTransform;
    private Image image;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    private void Update()
    {
        // Calculate the position for the top-right corner
        Vector2 topRightPosition = new Vector2(Screen.width - rectTransform.rect.width / 2f, Screen.height - rectTransform.rect.height / 2f);

        // Set the position of the RectTransform to the top-right corner
        rectTransform.position = topRightPosition;

        // Optionally, you can toggle the visibility based on certain conditions
        // For example, you can show the control button only when the player is near a specific object
        // Replace this condition with your own logic as needed
        bool showControlButton = true;
        image.enabled = showControlButton;
    }
}
