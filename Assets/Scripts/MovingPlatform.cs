using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 3f;
    Transform nextWaypoint;
    List<Transform> waypoints = new List<Transform>();
    Transform platform;

    int currentWaypoint;

    void Start()
    {
        int i = 0;

        foreach (Transform t in transform)
        {
            if (t.name.Contains("Point"))
            {
                waypoints.Add(t);
                if (Vector3.Distance(transform.position, t.position) > 1f)
                {
                    nextWaypoint = waypoints[i];
                    currentWaypoint = i;
                }
            }
            else
            {
                platform = t;
            }
        }

    }


	void Update () 
    {
	    platform.position = Vector3.MoveTowards(platform.position,nextWaypoint.position,moveSpeed*Time.deltaTime);

        if (Vector3.Distance(platform.position, nextWaypoint.position) < 0.4f)
        {
            if (currentWaypoint < waypoints.Count-1)
            {
                currentWaypoint++;
                nextWaypoint = waypoints[currentWaypoint];
            }
            else
            {
                currentWaypoint = 0;
                nextWaypoint = waypoints[currentWaypoint];
            }
        }
	}
}
