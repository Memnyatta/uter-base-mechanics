using System;
using System.Collections.Generic;
using UnityEngine;

class RhytmLine : MonoBehaviour
{
    public KeyCode button;
    public RhytmPlayer rhytmPlayer;
    public Transform noteSpawnTransform;

    private RhytmSynchronizer sync;
    private SortedList<double, double> noteSpawnTimes = new SortedList<double, double>();
    private double lastTime = 0;
    private int lastSpawnedNoteIdx = -1;

    void Update()
    {
        if (sync == null) { return; }
        var dt = sync.time - lastTime;

        // Передвинули назад
        if (sync.time < lastTime)
        {
            DespawnNotes();
            lastSpawnedNoteIdx = -1;
        }

        var nextNoteToSpawn = lastSpawnedNoteIdx + 1;

        while (nextNoteToSpawn < noteSpawnTimes.Count && noteSpawnTimes.Values[nextNoteToSpawn] <= sync.time)
        {
            SpawnNote(noteSpawnTimes.Values[nextNoteToSpawn]);

            nextNoteToSpawn++;
        }

        lastSpawnedNoteIdx = nextNoteToSpawn - 1;

        lastTime = sync.time;
    }

    private void DespawnNotes()
    {
        foreach (Transform child in noteSpawnTransform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public GameObject SpawnNote(double spawnTime)
    {
        var go = GameObject.Instantiate(rhytmPlayer.notePrefab, noteSpawnTransform.position, noteSpawnTransform.rotation, noteSpawnTransform);

        var noteComponent = go.GetComponent<RhytmNote>();

        if (!noteComponent)
        {
            noteComponent = go.AddComponent<RhytmNote>();
        }

        noteComponent.rhytmLine = this;
        noteComponent.startPos = noteSpawnTransform.position;
        noteComponent.endPos = transform.position;
        noteComponent.sync = sync;
        noteComponent.spawnTime = spawnTime;
        noteComponent.targetTime = spawnTime + rhytmPlayer.noteMoveToTargetTime;
        noteComponent.disappearTime = noteComponent.targetTime + rhytmPlayer.noteDisappearAfterTime;
        noteComponent.pressWindow = rhytmPlayer.notePressWindow;

        return go;
    }

    public void AddNote(double time)
    {
        noteSpawnTimes.Add(time, Math.Max(time - rhytmPlayer.noteMoveToTargetTime, 0));
    }

    internal void SynchronizeTo(RhytmSynchronizer sync)
    {
        this.sync = sync;
    }

    public void HandleBeatSuccess()
    {
        rhytmPlayer.HandleBeatSuccess();
    }

    public void HandleBeatFail()
    {
        rhytmPlayer.HandleBeatFail();
    }
}