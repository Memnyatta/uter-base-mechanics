using UnityEngine;
using UnityEngine.Playables;

class RhytmNoteHoldAsset : PlayableAsset
{
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return Playable.Null;
    }
}