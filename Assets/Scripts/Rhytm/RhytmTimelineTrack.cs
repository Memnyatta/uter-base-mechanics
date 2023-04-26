using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackClipType(typeof(RhytmNoteHoldAsset))]
[TrackBindingType(typeof(RhytmLine))]
class RhytmTimelineTrack : TrackAsset
{
    protected override Playable CreatePlayable(PlayableGraph graph, GameObject gameObject, TimelineClip clip)
    {
        var playable = base.CreatePlayable(graph, gameObject, clip);

        return playable;
    }
}