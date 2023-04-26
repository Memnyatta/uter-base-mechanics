using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(PlayableDirector))]
public class RhytmPlayer : MonoBehaviour
{
    public GameObject notePrefab;
    /// <summary>
    /// Сколько нота будет двигаться от спавна к цели.
    /// Если тут 1 секунда, то нота заспавниться за секунду и начнёт двигаться к цели.
    /// </summary>
    public double noteMoveToTargetTime = 1f;
    /// <summary>
    /// Через сколько секунд нота исчезнет, после того как она прошла "целевую отметку" где нужно нажимать на кнопку
    /// </summary>
    public double noteDisappearAfterTime = 0.1f;
    /// <summary>
    /// Это время в секундах на сколько можно промахнуться по времени нажатия на кнопку.
    /// Если тут 0.1 значит можно нажать на 0.1 секунду раньше или позже и это засчитаеться как успешное попадание.
    /// </summary>
    public double notePressWindow = 0.1f;

    public event Action OnBeatSuccess;
    public event Action OnBeatFail;

    PlayableDirector director;

    RhytmSynchronizer sync = new RhytmSynchronizer();

    void Start()
    {
        director = GetComponent<PlayableDirector>();
        Assert.IsNotNull(director);

        ConfigureRhytmTrack();
    }

    void Update()
    {
        sync.state = director.state;
        sync.time = director.time;
    }

    private void ConfigureRhytmTrack()
    {
        Assert.IsNotNull(director.playableAsset);
        Assert.AreEqual(director.playableAsset.GetType(), typeof(TimelineAsset));

        var timelineAsset = director.playableAsset as TimelineAsset;
        foreach (var track in timelineAsset.GetOutputTracks())
        {
            foreach (var o in track.outputs)
            {
                if (o.sourceObject != null)
                {
                    var binding = director.GetGenericBinding(o.sourceObject);

                    if (binding != null && binding.GetType() == typeof(RhytmLine))
                    {
                        var rhytmTrack = binding as RhytmLine;
                        foreach (var marker in track.GetMarkers())
                        {
                            if (marker.GetType() == typeof(RhytmNoteMarker))
                            {
                                rhytmTrack.AddNote(marker.time);
                            }
                        }

                        rhytmTrack.SynchronizeTo(sync);
                    }
                }
            }
        }
    }

    internal void HandleBeatFail()
    {
        OnBeatFail();
    }

    internal void HandleBeatSuccess()
    {
        OnBeatSuccess();
    }
}