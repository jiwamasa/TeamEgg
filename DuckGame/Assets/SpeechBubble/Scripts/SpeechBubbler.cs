using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Attach to object that can spawn speech bubbles
public class SpeechBubbler : MonoBehaviour
{
    public GameObject speechBubblePrefab; // Prefab of speech bubble object

    // Spawn a speech bubble with an image
    // IN:
    //   emote: Emote to play
    //   speechTime: Time before speech bubble disappears
    public void SpawnSpeechBubble(AnimationClip emote, float speechTime)
    {
        switch(emote.name)
        {
            case "Happy":
                SFXPlayer.instance.PlaySFX("yahah");
                break;
            case "Sad":
                SFXPlayer.instance.PlaySFX("sad");
                break;
            case "Scared":
                SFXPlayer.instance.PlaySFX("crie");
                break;
        }
        // Create speech bubble
        GameObject speechBubble = Instantiate(speechBubblePrefab, transform);
        // Set speech bubble position
        speechBubble.transform.localPosition = new Vector3(2f, 1.5f, 0);
        // Set speech time (which actually sets speed, so we use the inverse)
        speechBubble.GetComponent<Animator>().SetFloat("SpeechTime", 1f / speechTime);
        // Set emote
        speechBubble.GetComponentInChildren<SpeechBubble>().emoteController["Emote"] = emote;
    }
}
