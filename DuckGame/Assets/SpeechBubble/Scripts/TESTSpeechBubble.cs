using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Test harness
public class TESTSpeechBubble : MonoBehaviour
{
    public SpeechBubbler speechBubbler;
    public Sprite image;

    void Start()
    {
        speechBubbler.SpawnSpeechBubble(image, 10f);
    }

}
