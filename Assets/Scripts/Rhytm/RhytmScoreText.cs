using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(TextMesh))]
public class RhytmScoreText : MonoBehaviour
{
    public RhytmScore rhytmScore;
    private TextMesh textMesh;

    // Start is called before the first frame update
    void Awake()
    {
        textMesh = GetComponent<TextMesh>();
        rhytmScore.OnScoreUpdate += UpdateScoreText;
    }

    void OnDestroy()
    {
        rhytmScore.OnScoreUpdate -= UpdateScoreText;
    }

    private void UpdateScoreText(long score)
    {
        textMesh.text = score.ToString();
    }
}
