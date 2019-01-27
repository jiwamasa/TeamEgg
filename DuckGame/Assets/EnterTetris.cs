using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTetris : MonoBehaviour
{
    public float arcDuration = 3f;

    private float counter;

    private bool carryingObject = false;
    private GameObject carriedObject;

    private Vector2 startingPosition;
    private Vector2 targetPosition;

    public float impulseValue = 3f;
    private bool forceGiven = false;

    //Testing
    public GameObject testMe;

    // Start is called before the first frame update
    void Start()
    {
        sendToTetris(testMe);
    }

    // Update is called once per frame
    void Update()
    {

        if (carryingObject)
        {
            counter += Time.deltaTime;
            var lerper = Vector2.Lerp(startingPosition, targetPosition, counter / arcDuration);

            carriedObject.transform.position = new Vector2(lerper.x, lerper.y);

            if(counter >= arcDuration)
            {
                carriedObject.GetComponent<moveTetrisBlock>().enabled = true;
                carryingObject = false;
            }

        }
    }

    //Take an object, lerp them to entry point and turn on their tetris functionality
    public void sendToTetris(GameObject targetObject)
    {
        //Lerp the object to the position of this object
        carryingObject = true;
        carriedObject = targetObject;

        startingPosition = targetObject.transform.position;
        targetPosition = transform.position;

    }
}
