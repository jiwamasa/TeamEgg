using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatySpawn : MonoBehaviour
{
    public float timer = 0.0f;
    public float timeIntervalMin = 3.0f;
    public float timeIntervalMax = 10.0f;
    private float randomInterval;
   
    public List<GameObject> housePieces = new List<GameObject>();
    public List<GameObject> houseMates = new List<GameObject>();

    int pieceCount = 0;

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
            randomIndex = Random.Range(0, housePieces.Count);

            

            if (pieceCount > 1)
            {
                var toSpawn = houseMates[0];
                //housePieces.RemoveAt(0);
                GameObject instance = Instantiate(toSpawn, new Vector2(transform.position.x, Constants.WaterLevel), Quaternion.identity);
                pieceCount = 0;
            }
            else
            {
                var toSpawn = housePieces[randomIndex];
                housePieces.RemoveAt(randomIndex);
                GameObject instance = Instantiate(toSpawn, new Vector2(transform.position.x, Constants.WaterLevel), Quaternion.identity);
                pieceCount++;
            }

            //instance.GetComponent<SpriteRenderer>().sprite = Resources.Load(names[randomIndex], typeof(Sprite)) as Sprite;
        }
	}

    void GenerateInterval ()
    {
        randomInterval = Random.Range(timeIntervalMin, timeIntervalMax);
    }
}
