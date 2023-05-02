using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
class RhytmNote : MonoBehaviour
{
    public RhytmLine rhytmLine;
    public Vector3 startPos;
    public Vector3 endPos;
    public RhytmSynchronizer sync;
    public double spawnTime;
    public double targetTime;
    public double disappearTime;
    public double pressWindow;

    bool used;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        UpdatePosition();
    }

    void Update()
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Finished"))
        {
            GameObject.Destroy(this);
        }
        CheckPress();
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        var movementProgress = MathUtils.InvLerpExtrapolate(spawnTime, targetTime, sync.time);
        transform.position = MathUtils.LerpExtrapolate(startPos, endPos, (float)movementProgress);
    }

    private void CheckPress()
    {
        var isAfterPressWindow = sync.time - targetTime > pressWindow;
        var isWithinPressWindow = Math.Abs(sync.time - targetTime) <= pressWindow;

        if (isWithinPressWindow && !used && Input.GetKeyDown(rhytmLine.button))
        {
            used = true;
            rhytmLine.HandleBeatSuccess();
            Debug.Log("HIT!");
            animator.SetInteger("State", 1);
        }

        if (isAfterPressWindow && !used)
        {
            used = true;
            rhytmLine.HandleBeatFail();
            Debug.Log("MISS!");
            animator.SetInteger("State", -1);
        }
    }
}
