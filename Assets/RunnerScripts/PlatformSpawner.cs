using UnityEngine;
using System.Collections;

public class PlatformSpawner : MonoBehaviour 
{

    public GameObject[] platforms;
    public float spawnMinTime = 1f;
    public float spawnMaxTime = 3f;

    float timer = 0;

	void Start () 
    {
        SpawnPlatform();
	}

    void Update () 
    {
        timer += Time.deltaTime;
	}

    public void SpawnPlatform()
    {
        if (timer > spawnMinTime)
        {
            Instantiate(platforms[Random.Range(0, platforms.Length - 1)],transform.position,Quaternion.identity);
            timer = 0;
        }


        Invoke("SpawnPlatform", Random.Range(spawnMinTime, spawnMaxTime));
    }
}
