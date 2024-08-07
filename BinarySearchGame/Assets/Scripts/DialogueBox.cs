using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Button leftButton;
    public Button rightButton;
    public Button closeButton; // Close button for normal dialogue box
    private Action onLeftButtonClicked;
    private Action onRightButtonClicked;

    void Start()
    {
        dialoguePanel.SetActive(false);
        leftButton.onClick.AddListener(LeftButtonClicked);
        rightButton.onClick.AddListener(RightButtonClicked);
        closeButton.onClick.AddListener(CloseButtonClicked); // Add listener for close button
    }

    public void ShowDialogue(string message)
    {
        dialogueText.text = message;
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(true); // Show close button
        dialoguePanel.SetActive(true);
    }

    public void ShowDialogue(string message, Action leftAction, Action rightAction)
    {
        dialogueText.text = message;
        onLeftButtonClicked = leftAction;
        onRightButtonClicked = rightAction;
        leftButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(true);
        closeButton.gameObject.SetActive(false); // Hide close button
        dialoguePanel.SetActive(true);
    }

    void LeftButtonClicked()
    {
        onLeftButtonClicked?.Invoke();
        StartCoroutine(DelayedCloseDialogue());
    }

    void RightButtonClicked()
    {
        onRightButtonClicked?.Invoke();
        StartCoroutine(DelayedCloseDialogue());
    }

    void CloseButtonClicked()
    {
        StartCoroutine(DelayedCloseDialogue());
    }

    IEnumerator DelayedCloseDialogue()
    {
        yield return new WaitForSeconds(0.5f); // Adjust the delay time here (in seconds)
        dialoguePanel.SetActive(false);
    }
}
