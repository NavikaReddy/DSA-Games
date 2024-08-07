using TMPro;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System;

public class User
{
    public string dijkstraScore;
    public string Stringify()
    {
        return JsonUtility.ToJson(this);
    }
    public static User Parse(string json)
    {
        return JsonUtility.FromJson<User>(json);
    }
}

public class ScoreTrack : MonoBehaviour
{
    public static int score;
    public TMP_Text scoreText;
    void Start()
    {
        UpdateScoreText();
    }


    public static void IncrementScore(int amount)
    {
        score += amount;
        ScoreTrack s=new ScoreTrack();
        s.UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        ScoreTrack scoreTrack = FindObjectOfType<ScoreTrack>();

        if (scoreTrack != null && scoreTrack.scoreText != null)
        {
            scoreTrack.scoreText.text = "Score: " + score.ToString();
            User u= new User();
            u.dijkstraScore = score.ToString();
            StartCoroutine(Upload(u.Stringify(), result => {
                Debug.Log(result);
            }));
        }
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