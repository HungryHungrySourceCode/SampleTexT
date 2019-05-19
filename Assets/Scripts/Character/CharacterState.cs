using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{

    [HideInInspector]
    public bool grounded;

    [HideInInspector]
    public float xvel;

    [HideInInspector]
    public float yvel;

    private ContactFilter2D filter;
    private Vector2 adjust;

    // Start is called before the first frame update
    void Start()
    {
        xvel = 0;
        yvel = 0;
        checkGrounded();
        filter = new ContactFilter2D();
        adjust = new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        adjust.x = 0;
        adjust.y = 0;
        checkGrounded();
        applyGravity(Time.fixedDeltaTime);
        Vector3 move = new Vector3(xvel * Time.fixedDeltaTime + adjust.x, yvel * Time.fixedDeltaTime + adjust.y, 0);
        // These two lines make sure that we remove any floating point error that has crept in
        if(Math.Abs(move.x) < .01) move.x = 0f;
        if(Math.Abs(move.y) < .01) move.y = 0f;
        transform.Translate(move);
    }

    // TODO: Rework this. Collider2D.Raycast only sends one ray from the center of the collider,
    // Which means that the center of the object must be supported to be considered grounded.
    private void checkGrounded()
    {
        Collider2D collider = GetComponent<Collider2D>();
        float csize = collider.bounds.extents.y;
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        collider.Raycast(Vector2.down, // Cast it in direction of gravity
            filter.NoFilter(), // Maybe change this to use a filter, i.e. only catch ground
            hits, // results list
            GameSettings.instance.GroundTolerance + csize); // Add csize, because the ray starts in the center of the collider

        // we may need this to be a bit more robust but for now, in theory, it should do.
        grounded = hits.Count > 0;
        if(grounded)
        {
            // Put ourselves on the ground
            adjust.y = -(hits[0].distance - csize);
        }
    }

    private void applyGravity(float deltaTime)
    {
        if(grounded && yvel < 0) // we've hit a floor
            yvel = 0;
        else
            yvel -= GameSettings.instance.Gravity * deltaTime; 
    }
}
