using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    //public Key keyScript;
    public Rigidbody rb;
    public SphereCollider coll;
    public Transform player, key, fpsCam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    private void Start()
    {
        //Setup
        if (!equipped)
        {
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //Check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

        //Drop if equipped and "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        //Make Rigidbody kinematic and BoxController a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

        //Enable script
        //keyScript.enabled = true;
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        //Make Rigidbody not kinematic and BoxController normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Disable script
        //keyScript.enabled = false;

    }
}
