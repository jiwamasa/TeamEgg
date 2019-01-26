using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Attach to object that can spawn speech bubbles
public class SpeechBubbler : MonoBehaviour
{
    public Canvas speechCanvas; // Main canvas for speech bubbles
    public GameObject speechBubblePrefab; // Prefab of speech bubble object

    void Start()
    {
        SpawnSpeechBubble("hello world", 2f); // TEST
    }

    // Spawn a speech bubble
    // IN:
    //   speechText: Text to print
    //   speechTime: Time before speech bubble disappears
    public void SpawnSpeechBubble(string speechText, float speechTime)
    {        
        // Create speech bubble
        GameObject speechBubble = Instantiate(speechBubblePrefab, speechCanvas.transform);
        // Set speech bubble position
        speechBubble.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        // Set speech time (which actually sets speed, so we use the inverse)
        speechBubble.GetComponent<Animator>().SetFloat("SpeechTime", 1f / speechTime);
        // Set speech text
        speechBubble.GetComponentInChildren<Text>().text = speechText;
    }
}
