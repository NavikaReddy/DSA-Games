using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public int low, high, mid, ans;
    public DialogueBox dialogueBox;
    public TextMeshProUGUI rightTxt;
    public TextMeshProUGUI wrongTxt;



    void Start()
    {
        rightTxt.text=ScoreTrack.crct.ToString();
        wrongTxt.text = ScoreTrack.wrong.ToString();
        mid = (low + high) / 2;
        if (dialogueBox == null)
        {
            Debug.LogError("DialogueBox is not assigned!");
        }

        if (rightTxt == null)
        {
            Debug.LogError("rightTxt is not assigned!");
        }

        if (wrongTxt == null)
        {
            Debug.LogError("wrongTxt is not assigned!");
        }
    }

    public void Moves()
    {
        ;
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;
        if (clickedObject.name == mid.ToString() + 'i')
        {
            ScoreTrack.crct = ScoreTrack.crct + 1;
            rightTxt.text= ScoreTrack.crct.ToString();
            Destroy(clickedObject);
            if (mid == ans)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                dialogueBox.ShowDialogue("Search on", DestroyRight, DestroyLeft);
            }

        }
        else
        {
            ScoreTrack.wrong = ScoreTrack.wrong + 1;
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
        if (ans > mid)
        {
            ScoreTrack.wrong = ScoreTrack.wrong + 1;
            wrongTxt.text = ScoreTrack.wrong.ToString();
            dialogueBox.ShowDialogue("You have destroyed the number");
            SceneManager.LoadScene("Lost");
        }
        else
        {
            ScoreTrack.crct= ScoreTrack.crct + 1;
            rightTxt.text=ScoreTrack.crct.ToString();
        }
        high = mid - 1;
        mid = (low + high) / 2;
    }

    void DestroyLeft()
    {
        for (int i = low; i <= mid; i++)
        {
            DestroyObjects(i);
        }
        if(ans<mid)
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
        low = mid+1;
        mid = (high + low) / 2;
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
