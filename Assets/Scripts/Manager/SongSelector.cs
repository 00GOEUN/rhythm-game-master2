using System.IO;
using UnityEngine;

public class SongSelector : MonoBehaviour
{
    private bool Play = false;
    private bool Set = false;

    public string songName { get; set; }
    public string fpath { get; set; }
    public string finfo { get; set; }

    public FileStream file { get; set; }
    public StreamReader str { get; set; }

    public double timeDelay { get; set; }
    public double BPM { get; set; }
    public double BeatDistance { get; set; }
    public int Sync { get; set; }
    public double beatRate { get; set; }
    public byte definitionNum { get; set; }

    public Sprite[] sprite { get; set; }

    public string midipath { get; set; }

    public Sound BGM { get; set; }
    public Sound MIDI { get; set; }


    void Update()
    {
        if (Play && !Set)
        {
            fpath = "Assets\\Resources\\" + songName;
            finfo = fpath + "\\" + songName + ".txt";

            file = new FileStream(finfo, FileMode.Open);
            str = new StreamReader(file);

            timeDelay = double.Parse(str.ReadLine());
            BPM = double.Parse(str.ReadLine());
            BeatDistance = double.Parse(str.ReadLine());
            Sync = int.Parse(str.ReadLine());

            beatRate = (60d / BPM) * (BeatDistance / 100d);

            definitionNum = byte.Parse(str.ReadLine());

            midipath = fpath + "\\Midi\\" + songName + ".mid";

            BGM = new Sound();
            BGM.name = songName + "BGM"; ;
            BGM.clip = Resources.Load(songName + "/Sound/" + songName + "BGM") as AudioClip;

            MIDI = new Sound();
            MIDI.name = songName + "MIDI";
            MIDI.clip = Resources.Load(songName + "/Sound/" + songName + "MIDI") as AudioClip;

            sprite = Resources.LoadAll<Sprite>(songName + "/SheetMusic");
            Set = true;
        }
    }

    public void Go(string songName)
    {
        Play = true;
        this.songName = songName;
    }

    public void Stop()
    {
        Play = false;
        Set = false;
    }
}
