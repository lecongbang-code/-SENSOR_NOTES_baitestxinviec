using UnityEngine;
using System;
using MidiParser;
using System.IO;
using System.Collections;
using UnityEngine.Networking;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;
    MidiFile midiFile;

    public Lane[] lanes;
    public AudioSource audioSource;
    public string fileName = "Unity.mid";
    public float songDelayInSeconds = 0f;

    public static float velocityPlayerMove = 0;
    public static float velocityNote = 0;
    float velocity = 0;

    [Obsolete]
    void Start()
    {
        Instance = this;

        if (Application.streamingAssetsPath.StartsWith("D:/") || Application.streamingAssetsPath.StartsWith("C:/"))
            ReadFromFile();
        else StartCoroutine(ReadFromWebsite());
    }

    [Obsolete]
    private IEnumerator ReadFromWebsite()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(Application.streamingAssetsPath + "/" + fileName))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error: " + www.error);
            }
            else
            {
                byte[] results = www.downloadHandler.data;
                using (var stream = new MemoryStream(results))
                {
                    midiFile = new MidiFile(stream);
                    GetDataFromMidi();
                }
            }
        }
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
        var path = Application.streamingAssetsPath + "/" + fileName;
        midiFile = new MidiFile(path);
        GetDataFromMidi();
    }

    public void GetDataFromMidi()
    {
        var ticksPerQuarterNote = midiFile.TicksPerQuarterNote;

        var timeRate = Math.Round(500f / (float)ticksPerQuarterNote, 4);

        foreach (var track in midiFile.Tracks)
        {
            foreach (var midiEvent in track.MidiEvents)
            {
                if (midiEvent.MidiEventType == MidiEventType.NoteOn)
                {
                    var note = midiEvent.Note;
                    double time = Math.Round(midiEvent.Time * timeRate / 1000f, 4);
                    foreach (var lane in lanes) lane.SetTimeStamps(time, note);
                    velocity = midiEvent.Velocity / 3.0f;
                    velocityNote = velocity;
                }
            }
        }

        Invoke(nameof(StartSong), songDelayInSeconds);
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