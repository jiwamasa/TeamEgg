using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector2 velocity = Vector3.zero;


    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
    }

    public void MoveTo(Vector2 destination) {
        endPosition = destination;
        StartCoroutine(Slide());
    }

    private IEnumerator Slide () {
        float dist = Vector2.Distance(transform.position, endPosition);
        while (dist > 0.1f) {
            Debug.Log(dist);
            Vector2 newPos = Vector2.SmoothDamp(transform.position, endPosition, ref velocity, 0.5f);
            dist = Vector2.Distance(newPos, endPosition);
            transform.position = new Vector3(newPos.x, newPos.y, -10f);
            yield return new WaitForEndOfFrame();

        }
    }

}
