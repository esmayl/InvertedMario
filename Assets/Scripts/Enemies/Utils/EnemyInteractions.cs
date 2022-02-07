using UnityEngine;
using System.Collections;

public class EnemyInteractions : MonoBehaviour 
{

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SendMessageUpwards("Death");

            col.gameObject.SendMessageUpwards("Jump",200f);
            
        }
    }

}
