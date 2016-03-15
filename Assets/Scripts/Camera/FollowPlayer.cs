using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour 
{
    public GameObject player;

    float height = 0;

	void FixedUpdate()
	{
	    if (CompareHeight(player.transform))
	    {
            height = Mathf.Lerp(height, player.transform.position.y, Time.deltaTime);	        
	    }

	    if (height < 2)
	    {
	        height = 2;
        }
        transform.position = new Vector3(player.transform.position.x+3f, height, -12);

	}

    bool CompareHeight(Transform tToCompare)
    {
        Vector3 diff = tToCompare.position - transform.position;



        if (Mathf.Abs(diff.y) > 4)
        {
            return true;
        }
        if (diff.y<2)
        {
            height = Mathf.Lerp(height,player.transform.position.y,Time.deltaTime*10);
            return true;
        }
        if (height < 1)
        {
            return false;
        }
        return false;
    }
}
