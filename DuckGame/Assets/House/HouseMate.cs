using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Person who can live in a room
public class HouseMate : MonoBehaviour
{
    public List<AnimationClip> randomEmotes; // Emotes shown when this person randomly chats
    public SpeechBubbler sb; // Spawns speechbubbles
    public HousePiece hp; // House this person is a resident in

    public float randomWait;
    public float randomLength;

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
            float randomWait = Random.value * this.randomWait + randomLength + 2;
            int randomEmote = (int)Random.Range(0, randomEmotes.Count);
            yield return new WaitForSeconds(randomWait);
            sb.SpawnSpeechBubble(randomEmotes[randomEmote], randomLength);
        }
    }
}
