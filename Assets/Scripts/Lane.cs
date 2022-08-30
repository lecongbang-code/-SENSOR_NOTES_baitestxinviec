using UnityEngine;
using MidiParser;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Lane : MonoBehaviour
{
    public static Lane Instance;
    public int noteType;
    public GameObject notePrefab;
    public int laneX;
    int laneY = 1;

    public List<double> timeStamps = new List<double>();

    int spawnIndex = 0;

    void Start()
    {
        Instance = this;
    }

    public void SetTimeStamps(double time, int note)
    {
        if (note == noteType)
        {
            timeStamps.Add(time/1000);
        }
    }

    private void Update()
    {
        if (spawnIndex < timeStamps.Count)
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime)
            {
                var laneZ = timeStamps[spawnIndex] * SongManager.velocity;
                int _laneX = Random.Range(0, 2);
                var g = Instantiate(notePrefab, new Vector3(_laneX == 0?laneX:-laneX, laneY, (float)laneZ), Quaternion.identity);
                g.transform.parent = gameObject.transform;
                g.GetComponent<Note>().assignedTime = timeStamps[spawnIndex];
                spawnIndex++;
            }
        }
    }
}