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

    public static float velocityPlayerMove = 0;
    public static float velocityNote = 0;
    float velocity = 0;
    public static double timeRate = 0;

    void Start()
    {
        Instance = this;
        ReadFromFile();
        Invoke(nameof(StartSong), songDelayInSeconds);
    }

    void Update()
    {
        if(songDelayInSeconds > 0)
        {
            songDelayInSeconds -= Time.deltaTime;
            ScoreManager.TextSongDelayInSeconds(songDelayInSeconds);
        }    
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
                    velocityNote = velocity;
                }
            }
        }
    }

    void StartSong()
    {
        audioSource.Play();
        velocityPlayerMove = velocity;
    }

    public void StopSong()
    {
        audioSource.Stop();
    }
}