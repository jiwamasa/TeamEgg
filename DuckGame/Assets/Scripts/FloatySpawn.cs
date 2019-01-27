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
    public List<string> names = new List<string>();

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
            int randomIndex;
            randomIndex = Random.Range(0, names.Count);
            GameObject instance = Instantiate(spawnable, new Vector2(-10.0f,Constants.WaterLevel), Quaternion.identity);
            instance.GetComponent<SpriteRenderer>().sprite = Resources.Load(names[randomIndex], typeof(Sprite)) as Sprite;
        }
	}

    void GenerateInterval ()
    {
        randomInterval = Random.Range(timeIntervalMin, timeIntervalMax);
    }
}
