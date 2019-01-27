using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Plays SFX
public class SFXPlayer : MonoBehaviour
{
    public static SFXPlayer instance = null;
    public AudioSource source;
    public List<AudioClip> sfx = new List<AudioClip>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySFX(string sfxName)
    {
        foreach(AudioClip clip in sfx)
        {
            if (clip.name == sfxName)
            {
                source.PlayOneShot(clip);
            }
        }
    }
    
}
