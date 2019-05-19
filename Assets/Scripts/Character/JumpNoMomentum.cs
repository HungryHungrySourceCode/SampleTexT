using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterState))]
public class JumpNoMomentum : JumpHandler
{
    CharacterState state;
    public int TotalJumps = 1;
    public float JumpVelocity = 10f;

    public int jumps = 0;

    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<CharacterState>();
    }

    // Update is called once per frame
    void Update()
    {
        // We've touched the ground, so we can reset air actions
        if(state.grounded)
        {
            jumps = 0;
        }
        else
        {
            jumps = Math.Max(1, jumps);
        }
    }

    public override void Jump()
    {
        // If we're moving up, we can't initiate a second jump, because we're
        // still holding the button.
        if(state.yvel > 0) return;

        if(state.grounded || (TotalJumps - jumps) > 0)
        {
            state.yvel = JumpVelocity;
            ++jumps;
        }
    }

    public override void Fall()
    {
        if(state.yvel < 0) return; // we're already falling
        state.yvel = 0;
    }
}