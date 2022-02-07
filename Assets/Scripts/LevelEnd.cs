using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    WaitForSeconds second;
    float speed = 20f;
    bool spinning = false;
    PlayerMovement playerMovement;

    void Awake()
    {
        second = new WaitForSeconds(0.05f);
        //StartCoroutine("Spin");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerMovement = other.transform.parent.GetComponent<PlayerMovement>();
            StartCoroutine("Spin");
        }
    }

    IEnumerator Spin()
    {
        if (spinning){ yield return null; }

        spinning = true;
        while (speed > 0)
        {
            transform.Rotate(new Vector3(0, speed, 0));

            speed -= 0.6f;

            transform.position += transform.up * 0.1f;
            transform.localScale -= Vector3.one * 0.025f;
            yield return second;
        }

        GetComponent<ParticleSystem>().Play();

        Invoke("DisableMesh",0.08f);

        playerMovement.canMove = false;

        spinning = false;

    }

    void DisableMesh()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        UserInterface.instance.SetEndReached();
    }
}
