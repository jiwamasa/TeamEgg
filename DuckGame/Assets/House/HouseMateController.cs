using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls other house mates, and tells them when to emote
public class HouseMateController : MonoBehaviour
{
    public List<HouseMate> houseMates; // List of all house mates

    public float leanThreshold; // Amount of lean until emote

    public AnimationClip leanEmote; // Emote played when leaning far
    public AnimationClip fishEmote; // Emote plated when fishing

    void Start()
    {
        lastFished = null;
    }

    public HousePiece lastFished // Last fished house piece
    {
        set
        {
            StartCoroutine(PlayStaggered(fishEmote));
        }
    }

    public float lean // Amount of boat leaning (in degrees)
    {
        set
        {
            if (Mathf.Abs(value) > leanThreshold)
            {
                StartCoroutine(PlayStaggered(leanEmote));
            }
        }
    }

    // Apply emote to all house mates one after the other
    IEnumerator PlayStaggered(AnimationClip emote)
    {
        foreach (HouseMate hm in houseMates)
        {
            hm.sb.SpawnSpeechBubble(emote, 3f);
            yield return new WaitForSeconds(Random.value * 0.2f);
        }
    }
}
