using UnityEngine;

public class PlayerSpawnPosition : MonoBehaviour
{
    private void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = transform.position;
    }
}
