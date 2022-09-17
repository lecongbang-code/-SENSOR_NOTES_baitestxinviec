using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Lane : MonoBehaviour
{
    public int noteType;
    public GameObject notePrefab;
    public List<double> timeStamps;

    int spawnIndex = 0;

    public void SetTimeStamps(double time, int note)
    {
        if (note == noteType)
        {
            timeStamps.Add(time);
        }
    }

    void Update()
    {
        SpawnNote();
    }

    void SpawnNote()
    {
        if (spawnIndex < timeStamps.Count)
        {
            float laneZ = (float)timeStamps[spawnIndex] * SongManager.velocity - (float)SongManager.timeRate * 3f;
            GameObject g = Instantiate(notePrefab, new Vector3(transform.position.x, transform.position.y, laneZ), Quaternion.identity);
            g.transform.SetParent(transform);
            spawnIndex++;
        }
    }
}