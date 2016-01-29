using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour 
{
    public GameObject player;


	void Update () 
    {
        transform.position = new Vector3(player.transform.position.x+7, 0, -12);
	}
}
