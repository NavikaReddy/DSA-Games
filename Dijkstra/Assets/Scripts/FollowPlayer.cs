using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Update is called once per frame
    public Transform player;
    public Vector3 offset;
    void Update()
    {
        transform.position = player.position + offset;

    }
}