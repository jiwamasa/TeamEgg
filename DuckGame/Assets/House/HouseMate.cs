using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Person who can live in a room
[RequireComponent(typeof(SpeechBubbler))]
public class HouseMate : MonoBehaviour
{
    public List<AnimationClip> randomEmotes; // Emotes shown when this person randomly chats
    public SpeechBubbler sb; // Spawns speechbubbles
    public AnimationClip scared;


    private float xRightMax = -1.5f;
    private float xLeftMax = -14.5f;

    private float counter = 0;
    private float wanderTime = 3f;
    private float wanderWaitTime = 3f;
    private bool chilling = true;

    private float speed = 0.03f;

    private Vector2 startingPosition;
    private Vector2 targetPosition;

    private bool wandering = false;
    private bool goingRight = true;

    void Start()
    {
        StartCoroutine(RandomChatter());
        if (sb == null)
        {
            sb = GetComponent<SpeechBubbler>();
        }
        sb.SpawnSpeechBubble(scared, 5f);
    }

    private void Update()
    {
        if (wandering)
        {
            if (chilling)
            {
                if (counter < wanderWaitTime)
                {
                    counter += Time.deltaTime;
                }
                else
                {
                    chilling = false;
                    counter = 0;
                }
            }
            else
            {
                if (counter < wanderTime)
                {
                    counter += Time.deltaTime;
                    if (goingRight) {
                        if (transform.position.x < xRightMax)
                        {
                            transform.position = new Vector2(transform.position.x + speed, transform.position.y);
                        }
                        else
                        {
                            goingRight = false;
                        }
                      
                    }
                    else
                    {
                        if (transform.position.x > xLeftMax)
                        {
                            transform.position = new Vector2(transform.position.x - speed, transform.position.y);
                        }
                        else
                        {
                            goingRight = true;
                        }
                    }
                }
                else
                {
                    chilling = true;
                    counter = 0;
                }
            }
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

    int count = 1;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Duck")
        {
            var rb = GetComponent<Rigidbody2D>();
            
            rb.velocity = Vector2.zero;
            if (rb.isKinematic == false)
            {
                wandering = true;
            }
            rb.isKinematic = true;

        }
    }
}
