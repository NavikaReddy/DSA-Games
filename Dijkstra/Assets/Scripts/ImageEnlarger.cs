using UnityEngine;
using UnityEngine.UI;

public class ImageEnlarger : MonoBehaviour
{
    public Image enlargedImage;
    public Button mapButton; // Reference to the map button
    private bool isEnlarged = false;

    void Start()
    {
        enlargedImage.gameObject.SetActive(false); // Initially hide the enlarged image
    }

    public void ToggleImageSize()
    {
        isEnlarged = !isEnlarged; // Toggle enlargement state

        if (isEnlarged)
        {
            // Enlarge the image to fit the screen
            enlargedImage.gameObject.SetActive(true);
            enlargedImage.rectTransform.sizeDelta = new Vector2(Screen.width-120, Screen.height-120); // Set size to screen size
        }
        else
        {
            // Shrink the image and continue the game
            enlargedImage.gameObject.SetActive(false);
        }
    }
}
