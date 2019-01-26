﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Attach to object that can spawn speech bubbles
public class SpeechBubbler : MonoBehaviour
{
    public Canvas speechCanvas; // Main canvas for speech bubbles
    public GameObject speechBubblePrefab; // Prefab of speech bubble object

    // Spawn a speech bubble with text
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
        speechBubble.GetComponentInChildren<SpeechBubble>().text.text = speechText;
    }

    // Spawn a speech bubble with an image
    // IN:
    //   speechImage: Sprite to display
    //   speechTime: Time before speech bubble disappears
    public void SpawnSpeechBubble(Sprite speechImage, float speechTime)
    {
        // Create speech bubble
        GameObject speechBubble = Instantiate(speechBubblePrefab, speechCanvas.transform);
        // Set speech bubble position
        speechBubble.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        // Set speech time (which actually sets speed, so we use the inverse)
        speechBubble.GetComponent<Animator>().SetFloat("SpeechTime", 1f / speechTime);
        // Set speech text
        speechBubble.GetComponentInChildren<SpeechBubble>().image.enabled = true;
        speechBubble.GetComponentInChildren<SpeechBubble>().image.sprite = speechImage;
    }
}