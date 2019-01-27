using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Globally accessible controller for managing housemates (mainly emotes)
public class HouseMateController : MonoBehaviour
{
    public static HouseMateController instance = null; // Global static ref
    public List<HouseMate> houseMates; // List of all housemates

    public AnimationClip happy;
    public AnimationClip sad;
    public AnimationClip scared;

    void Awake()
    {
        if (instance = null)
        {
            instance = this;
        }
        if (houseMates == null)
        {
            houseMates = new List<HouseMate>();
        }
    }

    // Play happy emote for all housemates
    public void HappyAll()
    {
        StartCoroutine(PlayStaggered(happy));
    }
    public void SadAll()
    {
        StartCoroutine(PlayStaggered(sad));
    }
    public void ScaredAll()
    {
        StartCoroutine(PlayStaggered(scared));
    }

    // Play emote for everyone, but not exactly at the same time
    IEnumerator PlayStaggered(AnimationClip emote)
    {
        foreach (HouseMate hm in houseMates)
        {
            hm.sb.SpawnSpeechBubble(emote, 1.5f);
            yield return new WaitForSeconds(Random.value * 0.5f);
        }
    }

}
