using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatyMove : MonoBehaviour 
{
    public float speed;
    public float timer = 2.0f;

    // Use this for initialization
    void Start() {}
	
	// Update is called once per frame
	void Update () 
    {
        timer -= Time.deltaTime;
        transform.Translate(speed, 0.0f, 0.0f);

        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
	}
}
