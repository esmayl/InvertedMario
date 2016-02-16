using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour {


    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

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
