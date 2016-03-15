using UnityEngine;
using System.Collections;

public class PlayerWeakspot : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            SendMessageUpwards("RemoveHp",col.transform.parent.GetComponent<EnemyStats>().enemyDamage);
            col.gameObject.SendMessageUpwards("JumpOff");
            
        }
    }
}
