using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UterSounds : MonoBehaviour
{
    void SoundWalk()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UterSounds/UterWalk", GetComponent<Transform>().position);
    }

    void SoundJump()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UterSounds/UterJump", GetComponent<Transform>().position);
    }

    void SoundGround()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UterSounds/UterGrounded", GetComponent<Transform>().position);
    }

    void SoundRun()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UterSounds/UterRun", GetComponent<Transform>().position);
    }
}
