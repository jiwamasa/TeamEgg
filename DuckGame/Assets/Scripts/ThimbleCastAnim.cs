using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThimbleCastAnim : MonoBehaviour
{
    private Animator anim;
    public GameObject hook;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {}

    public void AnimateCast ()
    {
        anim.Play("Base Layer.Thimble Cast", 0, 0.0f);
    }

    public void AnimateReel ()
    {
        anim.Play("Base Layer.ThimblePull", 0, 0.0f);
    }

    public void AnimateIdle ()
    {
        anim.Play("Base Layer.ThimbleOneHandIdleNoCast", 0, 0.0f);
    }

    public void ThrowCast()
    {
        hook.GetComponent<FishingControls>().ThrowCast();
    }
}
