using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RhytmPlayer))]
public class RhytmScore : MonoBehaviour
{
    public event Action<long> OnScoreUpdate;

    public long score = 0;

    public long increaseBy = 10;
    public long decreaseBy = 5;

    RhytmPlayer rhytmPlayer;

    void Awake()
    {
        rhytmPlayer = GetComponent<RhytmPlayer>();
        rhytmPlayer.OnBeatSuccess += IncreaseScore;
        rhytmPlayer.OnBeatFail += DecreaseScore;
    }

    void OnDestroy()
    {
        rhytmPlayer.OnBeatSuccess -= IncreaseScore;
        rhytmPlayer.OnBeatFail -= DecreaseScore;
    }

    private void IncreaseScore()
    {
        score += increaseBy;
        OnScoreUpdate(score);
    }

    private void DecreaseScore()
    {
        score -= decreaseBy;
        OnScoreUpdate(score);
    }
}
