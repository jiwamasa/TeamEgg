using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatySpawn : MonoBehaviour 
{
    public float timer = 0.0f;
    public float timeInterval = 1.0f;
    public GameObject spawnable; 

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () 
    {
        timer += Time.deltaTime;

        if (timer >= timeInterval)
        {
            timer -= timeInterval;
            Instantiate(spawnable, new Vector2(-10.0f,Constants.WaterLevel), Quaternion.identity);
        }
	}
}
