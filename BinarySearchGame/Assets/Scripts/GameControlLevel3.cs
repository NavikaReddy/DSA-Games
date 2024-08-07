using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameControlLevel3 : MonoBehaviour
{
    public int low, high, mid, ans ;
    public DialogueBox dialogueBox;
    public TextMeshProUGUI rightTxt;
    public TextMeshProUGUI wrongTxt;
    public int[] numbers = new int[13];
    public GameObject firstElement;
    void Start()
    {
        rightTxt.text = ScoreTrack.crct.ToString();
        wrongTxt.text = ScoreTrack.wrong.ToString();
        UpdateMid();
        if (dialogueBox == null) Debug.LogError("DialogueBox is not assigned!");
        if (rightTxt == null) Debug.LogError("rightTxt is not assigned!");
        if (wrongTxt == null) Debug.LogError("wrongTxt is not assigned!");
    }

    void UpdateMid()
    {
        mid = (low + high) / 2;
    }

    void RevealFirstElement()
    {
        firstElement = GameObject.Find(low.ToString() + 'i');
        firstElement.SetActive(false);
    }

    public void Moves()
    {
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;
        if (clickedObject == null) return;

        if (clickedObject.name == mid.ToString() + 'i')
        {
            ScoreTrack.crct++;
            rightTxt.text = ScoreTrack.crct.ToString();
            Destroy(clickedObject);
            RevealFirstElement();

            if (mid == ans)
            {
                SceneManager.LoadScene("Winner");
            }
            else
            {
                dialogueBox.ShowDialogue("Search on", DestroyRight, DestroyLeft);
            }
        }
        else
        {
            ScoreTrack.wrong++;
            wrongTxt.text = ScoreTrack.wrong.ToString();
            if (ScoreTrack.wrong == 3)
                SceneManager.LoadScene("Lost");
            dialogueBox.ShowDialogue("Use Binary Search");
        }

    }
    void DestroyRight()
    {

        for (int i = mid; i <= high; i++)
        {
            DestroyObjects(i);
        }
        high = mid - 1;
        mid = (low + high) / 2;
        firstElement.SetActive(true);
        if (low>ans || high<ans)
        {
            ScoreTrack.wrong = ScoreTrack.wrong + 1;
            wrongTxt.text = ScoreTrack.wrong.ToString();
            dialogueBox.ShowDialogue("You have destroyed the number");
            SceneManager.LoadScene("Lost");
        }
        else
        {
            ScoreTrack.crct = ScoreTrack.crct + 1;
            rightTxt.text = ScoreTrack.crct.ToString();
        }


    }

    void DestroyLeft()
    {
        for (int i = low; i <= mid; i++)
        {
            DestroyObjects(i);
        }
        low = mid + 1;
        mid = (high + low) / 2;
        if (low > ans || high < ans)
        {
            ScoreTrack.wrong = ScoreTrack.wrong + 1;
            wrongTxt.text = ScoreTrack.wrong.ToString();
            dialogueBox.ShowDialogue("You have destroyed the number");
            SceneManager.LoadScene("Lost");
        }
        else
        {
            ScoreTrack.crct = ScoreTrack.crct + 1;
            rightTxt.text = ScoreTrack.crct.ToString();
        }

    }

    void DestroyObjects(int index)
    {
        GameObject obj = GameObject.Find(index.ToString());
        if (obj != null)
        {
            Destroy(obj);
        }

        GameObject objWithI = GameObject.Find(index.ToString() + 'i');
        if (objWithI != null)
        {
            Destroy(objWithI);
        }
    }
}
