using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows for playing multiple tracks in layers
public class LayeredAudioController : MonoBehaviour
{
    public AudioClip[] clips; // Clips to layer
    public GameObject audioPlayer; // Player to clone

    public AudioSource[] sources; 

	// Use this for initialization
	void Start () {
        sources = new AudioSource[clips.Length];
		for(int i = 0; i < clips.Length; i++)
        {
            sources[i] = Instantiate(audioPlayer).GetComponent<AudioSource>();
            sources[i].clip = clips[i];
            sources[i].Play();
        }
        FadeIn(0); // Start base layer
	}

    // Fades in music layer
    public void FadeIn(int layer)
    {
        StartCoroutine(FadeInCR(sources[layer]));
    }

    IEnumerator FadeInCR(AudioSource source)
    {
        while (source.volume < 1)
        {
            source.volume += 0.02f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Fades out music layer
    public void FadeOut(int layer)
    {
        StartCoroutine(FadeOutCR(sources[layer]));
    }

    IEnumerator FadeOutCR(AudioSource source)
    {
        while (source.volume > 0)
        {
            source.volume -= 0.02f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
