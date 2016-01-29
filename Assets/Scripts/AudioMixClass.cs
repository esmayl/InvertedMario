using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class AudioMixClass : MonoBehaviour
{
    public AudioMixer mixer;

    public static AudioMixClass mixReff;

    List<GameObject> sources = new List<GameObject>();

	void Start ()
	{
	    mixReff = this;

        foreach (Transform t in transform)
        {
            sources.Add(t.gameObject);
        }
	}

    public AudioSource GetSource(string sourceName)
    {
        foreach (var s in sources)
        {
            if (s.name == sourceName)
            {
                if (s.GetComponent<AudioSource>())
                    return s.GetComponent<AudioSource>();
            }
        }
        return null;
    }
}
