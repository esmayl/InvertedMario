using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {


    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Application.LoadLevel(Application.loadedLevel);

        }
        else
        {
            if (col.transform.parent)
            {
                Destroy(col.transform.parent.gameObject);
            }
            else
            {
                Destroy(col.gameObject);
            }
        }
    }
}
