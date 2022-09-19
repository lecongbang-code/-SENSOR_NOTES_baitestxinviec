using UnityEngine;
using System;
using MidiParser;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;
    public static MidiFile midiFile;

    public Lane[] lanes;
    public AudioSource audioSource;
    public string fileLocation = "Unity.mid";
    public float songDelayInSeconds = 0f;

    public static float velocity = 0;
    public static double timeRate = 0;

    void Start()
    {
        Instance = this;
        ReadFromFile();
        Invoke(nameof(StartSong), songDelayInSeconds);
    }

    void ReadFromFile()
    {
        midiFile = new MidiFile(Application.streamingAssetsPath + "/" + fileLocation);

        var ticksPerQuarterNote = midiFile.TicksPerQuarterNote;

        timeRate = Math.Round(500f / (float)ticksPerQuarterNote, 4);

        foreach (var track in midiFile.Tracks)
        {
            foreach (var midiEvent in track.MidiEvents)
            {
                if (midiEvent.MidiEventType == MidiEventType.NoteOn)
                {
                    var note = midiEvent.Note;
                    double time = Math.Round(midiEvent.Time * timeRate / 1000f, 4);
                    foreach (var lane in lanes) lane.SetTimeStamps(time, note);
                    velocity = midiEvent.Velocity / 2.0f;
                }
            }
        }
    }

    void StartSong()
    {
        audioSource.Play();
    }

    public void StopSong()
    {
        audioSource.Stop();
    }
}