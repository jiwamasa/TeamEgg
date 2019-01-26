using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Single tetris cube. Sends collision events up to parent
public class TetrisCube : MonoBehaviour
{
    MoveTetrisBlock mtb;

    void Start()
    {
        mtb = GetComponentInParent<MoveTetrisBlock>();
    }

    //We have touched the duck
    public void OnCollisionEnter2D(Collision2D collision)
    {
        mtb.CubeCollision(collision);
    }
}
