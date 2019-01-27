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

    // Start is called before the first frame update
    void Start()
    {
        
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
                carryingObject = false;
                //Turn on Tetris Movement
                if (carriedObject.transform.tag == "TetrisBlock")
                {
                    carriedObject.GetComponent<moveTetrisBlock>().enabled = true;
                }
            }

        }
    }

    //Take an object, lerp them to entry point and turn on their tetris functionality
    public void sendToTetris(GameObject targetObject)
    {
        //Reset used variables
        counter = 0;

        //Lerp the object to the position of this object
        carryingObject = true;
        carriedObject = targetObject;

        startingPosition = targetObject.transform.position;
        targetPosition = transform.position;

        //Turn off unneeded functionality
        carriedObject.GetComponent<FloatyMove>().enabled = false;
        carriedObject.GetComponent<FishBehavior>().enabled = false;

        //Turn back to kinematic
        var rb2D = carriedObject.GetComponent<Rigidbody2D>();
        rb2D.isKinematic = true;
        rb2D.velocity = Vector2.zero;

        carriedObject.transform.GetChild(0).gameObject.SetActive(true);

    }

    public void sendHouseMateToDuck(GameObject targetObject)
    {
        //Reset used variables
        counter = 0;

        //Lerp the object to the position of this object
        carryingObject = true;
        carriedObject = targetObject;

        startingPosition = targetObject.transform.position;
        targetPosition = transform.position;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "TetrisBlock")
        {
            sendToTetris(collision.gameObject);
        }
        if(collision.tag == "HouseMate")
        {
            sendHouseMateToDuck(collision.gameObject);
            HouseMateController.instance.houseMates.Add(collision.gameObject.GetComponent<HouseMate>());
            HouseMateController.instance.HappyAll();
        }
    }
}
