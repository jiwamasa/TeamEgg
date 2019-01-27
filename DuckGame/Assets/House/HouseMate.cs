using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Person who can live in a room
[RequireComponent(typeof(SpeechBubbler))]
public class HouseMate : MonoBehaviour
{
    public List<AnimationClip> randomEmotes; // Emotes shown when this person randomly chats
    public SpeechBubbler sb; // Spawns speechbubbles

    void Start()
    {
        StartCoroutine(RandomChatter());
        if (sb == null)
        {
            sb = GetComponent<SpeechBubbler>();
        }
    }

    // Emote randomly every few seconds
    IEnumerator RandomChatter()
    {
        while (true)
        {
            float randomWait = Random.value * 10f + 10f;
            int randomEmote = (int)Random.Range(0, randomEmotes.Count);
            yield return new WaitForSeconds(randomWait);
            sb.SpawnSpeechBubble(randomEmotes[randomEmote], 2f);
        }
    }
}
