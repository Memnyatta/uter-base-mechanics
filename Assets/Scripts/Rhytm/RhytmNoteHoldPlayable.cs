using UnityEngine;
using UnityEngine.Playables;

class RhytmNoteHoldPlayable : PlayableBehaviour
{
    public override void PrepareData(Playable playable, FrameData info)
    {
        Debug.Log("Prepare data");
        Debug.Log(info);
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        Debug.Log("Prepare frame");
        Debug.Log(info);
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        Debug.Log("Process frame");
        Debug.Log(info);
    }
}
