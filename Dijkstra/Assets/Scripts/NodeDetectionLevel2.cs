using UnityEngine;
using UnityEngine.SceneManagement;

public class NodeDetectionLevel2 : MonoBehaviour
{
    public static int[] nodes;
    public int finalNode = 6;
    public static int idx = 0;
    public int[] ans = { 1, 3, 5, 6 };

     void Start()
    {
        // Initialize the nodes array with the required capacity
        Debug.Log(ans.Length);
        nodes = new int[20];
    }

     void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered: " + other.gameObject.name);
        if (int.TryParse(gameObject.name, out int nodeName))
        {
            if (idx==0 ||(idx!=0 &&  nodes[idx - 1] != nodeName))
            {
                if (nodeName == finalNode)
                {
                    nodes[idx] = finalNode;
                    idx++;
                    Debug.Log("Stored Number: " + nodes[idx - 1]);
                    if (CheckLevelOutcome())
                    {
                        ScoreTrack.IncrementScore(35);
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                    else
                    {
                        SceneManager.LoadScene("LevelFail");
                    }
                }
                else
                {
                    nodes[idx] = nodeName;
                    idx++;
                    Debug.Log("Stored Number: " + nodes[idx - 1]);
                }
            }
        }
        else
        {
            Debug.LogError("Failed to parse node name to integer.");
        }
    }

     bool CheckLevelOutcome()
    {
        if(idx!=ans.Length)
            return false;
        for (int i = 0; i < idx; i++)
        {
            if (nodes[i] != ans[i])
            {
                return false;
            }
        }
        return true;
    }
}
