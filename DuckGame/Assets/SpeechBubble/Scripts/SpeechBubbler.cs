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
    //   speechImage: Sprite to display
    //   speechTime: Time before speech bubble disappears
    public void SpawnSpeechBubble(Sprite speechImage, float speechTime)
    {
        // Create speech bubble
        GameObject speechBubble = Instantiate(speechBubblePrefab, transform);
        // Set speech bubble position
        speechBubble.transform.localPosition = Vector3.zero;
        // Set speech time (which actually sets speed, so we use the inverse)
        speechBubble.GetComponent<Animator>().SetFloat("SpeechTime", 1f / speechTime);
        // Set speech text
        speechBubble.GetComponentInChildren<SpeechBubble>().sr.sprite = speechImage;
    }
}
