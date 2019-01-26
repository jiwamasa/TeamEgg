using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatySpawn : MonoBehaviour 
{
    public float timer = 0.0f;
    public float timeIntervalMin = 3.0f;
    public float timeIntervalMax = 10.0f;
    private float randomInterval;
    public GameObject spawnable; 

	// Use this for initialization
	void Start () 
    {
        GenerateInterval();
    }
	
	// Update is called once per frame
	void Update () 
    {
        timer += Time.deltaTime;

        if (timer >= randomInterval)
        {
            timer -= randomInterval;
            GenerateInterval();
            Instantiate(spawnable, new Vector2(-10.0f,Constants.WaterLevel), Quaternion.identity);
        }
	}

    void GenerateInterval ()
    {
        randomInterval = Random.Range(timeIntervalMin, timeIntervalMax);
    }
}
