using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public float waitOnPickup = 0.2f;
    public float breakForce = 35f;
    [HideInInspector] public bool pickedUp = false;
    [HideInInspector] public PlayerInteraction playerInteractions;
    [Header("FMOD event name")]
    public string CollisionSound;
    bool canPlay;
    public GameObject text;

    private void Start()
    {
        canPlay = false;
        Invoke("CanPlaySound", 0.2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (pickedUp)
        {
            if (collision.relativeVelocity.magnitude > breakForce)
            {
                playerInteractions.BreakConnection(true);
            }
        }

        if (CollisionSound != null && canPlay)
        {
            FMODUnity.RuntimeManager.PlayOneShot(CollisionSound, transform.position);
        }
        canPlay = true;
    }

    private void Update()
    {
        text = transform.GetChild(0).gameObject;
        if (text != null)
        {
            var r = GetComponent<Renderer>();
            if (r != null)
            {
                var bounds = r.bounds;
                text.transform.position = new Vector3(transform.position.x, bounds.max.y, transform.position.z);
            }
        }
    }

    //this is used to prevent the connection from breaking when you just picked up the object as it sometimes fires a collision with the ground or whatever it is touching
    public IEnumerator PickUp()
    {
        yield return new WaitForSecondsRealtime(waitOnPickup);
        pickedUp = true;

    }

    public void BreakConnection()
    {
        playerInteractions.BreakConnection(true);
    }
}

