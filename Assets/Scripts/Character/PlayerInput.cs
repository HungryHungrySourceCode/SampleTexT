using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CharacterState))]
[RequireComponent(typeof(JumpHandler))]
[RequireComponent(typeof(WalkHandler))]
public class PlayerInput : MonoBehaviour
{
    public string HorzAxis = "Horizontal";
    public string JumpButton = "Jump";

//  private CharacterState state;
    private JumpHandler jump;
    private WalkHandler walk;

    // Start is called before the first frame update
    void Start()
    { 
//      state = GetComponent<CharacterState>();
        jump = GetComponent<JumpHandler>();
        walk = GetComponent<WalkHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        float horz = Input.GetAxisRaw(HorzAxis);
        float j = Input.GetAxisRaw(JumpButton);

        if(j > .5f)
            jump.Jump();
        else
            jump.Fall();

        if(Math.Abs(horz) > 0.01f)
            walk.Walk(horz);
        else
            walk.Stop();
    }
}
