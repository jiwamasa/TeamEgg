using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditItemWobble : MonoBehaviour {

    private float timer = 0f;
    public bool direction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime * 5f;
        float wobble = Mathf.Sin(timer) * 3f;
        wobble = direction ? wobble : wobble * -1f;
        transform.rotation = Quaternion.Euler(0f,0f, wobble);
	}
}
