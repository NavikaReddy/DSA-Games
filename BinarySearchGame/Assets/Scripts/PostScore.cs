using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class User
{
    public string binarySearchScore;

    public string Stringify()
    {
        return JsonUtility.ToJson(this);
    }

    public static User Parse(string json)
    {
        return JsonUtility.FromJson<User>(json);
    }
}

public class PostScore : MonoBehaviour
{
    public TextMeshProUGUI scoreTxt;

    void Start()
    {
        CalculateScore();
        UpdateScoreText();
    }

    public void CalculateScore()
    {
        int correctMoves = ScoreTrack.crct;
        int wrongMoves = ScoreTrack.wrong;
        int maxScore = 100;
        int perfectMoves = 19;
        float correctPercentage = Mathf.Clamp01((float)correctMoves / perfectMoves);
        float wrongPenalty = Mathf.Clamp01((float)wrongMoves / (perfectMoves * 0.5f)); // Penalty for wrong moves
        float totalPercentage = Mathf.Clamp01(correctPercentage - wrongPenalty);
        int score = Mathf.RoundToInt(totalPercentage * maxScore);
        ScoreTrack.score = score;
    }

    public void UpdateScoreText()
    {
        scoreTxt.text = ScoreTrack.score.ToString();
        User u = new User();
        u.binarySearchScore = ScoreTrack.score.ToString();
        StartCoroutine(Upload(u.Stringify(), result => {
            Debug.Log(result);
        }));
    }

    IEnumerator Upload(string score, System.Action<bool> callback = null)
    {
        using (UnityWebRequest request = new UnityWebRequest("http://localhost:3500/user-api/updateDScore", "POST"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(score.ToString());
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                if (callback != null)
                {
                    callback.Invoke(false);
                }
            }
            else
            {
                if (callback != null)
                {
                    callback.Invoke(request.downloadHandler.text != "{}");
                }
            }
        }
    }
}
