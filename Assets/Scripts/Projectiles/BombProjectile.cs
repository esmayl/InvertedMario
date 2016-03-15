using UnityEngine;
using System.Collections;

public class BombProjectile : MonoBehaviour
{
    public AudioClip ExplosionClip;
    public ParticleSystem ExplosionParticleSystem;
    public string audioSourceName;
    internal int damage;

    GameObject temp;

	void Start ()
	{
	    temp = transform.GetChild(0).gameObject;
	}
	
	void Update () 
    {
	    if (temp)
	    {
	        Destroy(temp,1f);
            Destroy(gameObject,1.1f);
	    }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player") && gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            col.SendMessageUpwards("RemoveHp", damage);

            temp.transform.position = transform.position;
            temp.GetComponent<ParticleSystem>().Play();
            temp.transform.parent = null;
            temp = null;

            transform.GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            PlayAudio();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (temp)
        {
            temp.transform.position = transform.position;
            temp.GetComponent<ParticleSystem>().Play();
            temp.transform.parent = null;
            temp = null;
            transform.GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            PlayAudio();
        }
    }

    void OnDestroy()
    {
        Destroy(temp);
    }

    void PlayAudio()
    {
        if (AudioMixClass.mixReff.GetSource(audioSourceName))
        {
            AudioSource tempAudio = AudioMixClass.mixReff.GetSource(audioSourceName);
            tempAudio.clip = ExplosionClip;
            tempAudio.Play();
        }
    }

}
