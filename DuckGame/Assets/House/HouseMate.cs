using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Person who can live in a room
public class HouseMate : MonoBehaviour
{
    public List<AnimationClip> randomEmotes; // Emotes shown when this person randomly chats

    HousePiece hp; // House this person is a resident in
    SpeechBubbler sb; // Spawns speechbubbles

    void Start()
    {
        hp = GetComponentInParent<HousePiece>();
        sb = GetComponent<SpeechBubbler>();
        StartCoroutine(RandomChatter());
    }

    // Emote randomly every few seconds
    IEnumerator RandomChatter()
    {
        while (true)
        {
            float randomWait = Random.value * 5f + 5f;
            int randomEmote = (int)Random.Range(0, randomEmotes.Count);
            yield return new WaitForSeconds(randomWait);
            sb.SpawnSpeechBubble(randomEmotes[randomEmote], 4f);
        }
    }
}
