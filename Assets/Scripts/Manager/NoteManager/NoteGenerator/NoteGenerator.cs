using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    public string fname { get; set; }
    public double timeDelay { get; set; }

    public List<MidiNoteInfo> noteList = null;
    // Start is called before the first frame update
    void Awake()
    {
        FileStream fs = new FileStream(fname, FileMode.Open);

        noteList = new List<MidiNoteInfo>(500);
        while (fs.Position < fs.Length)
        {
            Chunk chunk = Chunk.Parse(fs);
            if (chunk is Track)
            {
                NodeGenerator(chunk as Track);
            }
        }

        fs.Close();
    }

    private void NodeGenerator(Track track)
    {
        int time = (int)timeDelay;

        foreach (MDEvent mdevent in track)
        {
            MidiNoteInfo newNote = GenerateNote(mdevent as MidiEvent);
            time += newNote.Time;
            newNote.Time = time;
            if (mdevent is MidiEvent && newNote.Power != 0)
            {
                noteList.Add(newNote);
            }
        }
    }

    private MidiNoteInfo GenerateNote(MidiEvent midiEvent)
    {
        return midiEvent.GenerateNote;
    }
}
