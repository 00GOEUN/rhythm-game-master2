
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    public bool isNoteManagerReady { get; set; }
    /*
     ���߿� ��ũ �����Ҷ� �����ִ� ��� ��Ʈ�� ��ũ�� �����ϱ� ���� 
     ��� ��Ʈ�� �ϳ��� ����Ʈ�� ���� �ְ� ��ũ�� ��ȭ�� ���� ��� 
     �ش� ��Ʈ���� ��ġ���� �����Ų��.
     */

    public string fname { get; set; }
    public double timeDelay { get; set; }
    public double beatRate { get; set; }
    public float NoteSpeed { get; set; }
    public double BeatDistance { get; set; }
    public int Sync { get; set; }
    public List<MidiNoteInfo> NoteList {
        get
        {
            return generator.noteList;
        }
    }

    [SerializeField] Transform[] NoteAppearLocation = null;
    [SerializeField] GameObject[] NoteType = null;

    //��ũ��Ʈ ����
    NoteGenerator generator = null;

    //��� ����
    static int MAX_LINE = 10;
    static int START_NOTE_NUM = 20;

    //static int MAX_SYNC = 30;
    //static int MIN_SYNC = -30;

    //�����̳� ����
    List<Queue<GameObject>> noteQueue = null;
    public List<List<GameObject>> noteContainer = null;
    public List<GameObject> noteManager = null;

    //��Ʈ ���� ���� ����
    double currentTime = 0;
    int currentNote = 0;

    bool run = true;

    Text panjung = null;

    void Awake()
    {
        isNoteManagerReady = false;
        //NoteGenerator ��ũ��Ʈ���� �̵� ������ �а� ��Ʈ ������ �� ���ͼ� ��Ʈ ����Ʈ�� �����.
        generator = GetComponent<NoteGenerator>();
        generator.fname = fname;
        generator.timeDelay = ((timeDelay / 1000) / beatRate) * 480 ;

        noteQueue = new List<Queue<GameObject>>();
        noteContainer = new List<List<GameObject>>();

        currentTime += 0;

        panjung = GameObject.Find("panjung").GetComponent<Text>();

        //�� ���θ��� ������Ʈ Ǯ�� ����� 20���� ��Ʈ�� ������ �� ��Ȱ��ȭ �� ���·� ť�� ���� �ִ´�.
        for (int line = 0; line < MAX_LINE; line++)
        {
            NoteType[line].GetComponent<Note>().noteSpeed = NoteSpeed;
            noteQueue.Add(new Queue<GameObject>());
            noteContainer.Add(new List<GameObject>());
            for (int num = 0; num < START_NOTE_NUM; num++)
            {
                GameObject note = GenerateNote(line);
                note.SetActive(false);
                noteQueue[line].Enqueue(note); ;
                noteManager.Add(note);
            }
        }
        isNoteManagerReady = true;
        Debug.Log("NoteManager Ready");
    }

    /* �̵��Ʈ���� �� ���ڿ� 480 ��ŸŸ������ �̷���� ������ �̴� BPM�� �����ϴ�.
     * �׷��� �̸� ���� ���ڿ� ���߱� ���ؼ� �켱 480���� ������ �� ���ڸ� �������� ���� �� �ش� ���ڸ� BPM�� �����.
     * beatRate�� 60/BPM�̴�.*/
    void Update()
    {
        if (run)
        {
            currentTime += Time.deltaTime;
            while (currentNote < generator.noteList.Count && currentTime > beatRate * (double)generator.noteList[currentNote].Time / 480d)
            {
                int line = soundNum[generator.noteList[currentNote++].Sound];
                if (line != -1) GetQueue(line);
            }
        }
    }

    //���� �ݺ��� ���� �Լ�. ��Ʈ�� �ش� �������� ���� ��Ų��.
    public void Repeat(double startTime)
    {
        currentTime = startTime;
        currentNote = 0;
        for(int line = 0; line < 10; line++)
        {
            while(noteContainer[line].Count > 0)
                InsertQueue(noteContainer[line][0], line);
        }
        while (currentTime > beatRate * (double)generator.noteList[currentNote + 1].Time / 480d)
            currentNote++;
        Go();
    }

    //��Ʈ�� ���� �� ��Ȱ��ȭ ��Ų��.
    GameObject GenerateNote(int line)
    {
        GameObject noteType = NoteType[line];
        Vector3 position = NoteAppearLocation[line].position + new Vector3(0, Sync, 0); //��Ʈ���� ���ο��� ������ ��ũ��ŭ �������� ���Ѵ�.

        GameObject note = Instantiate(noteType, position, Quaternion.identity);
        
        note.transform.SetParent(GameObject.Find("NoteLineMask").transform);
        note.SetActive(false);
        note.name = noteType.name; //Ŭ���� �̸��� ������ ���� �� ���߿� ���ϱ� ���� �����.
        return note;
    }


    //��Ʈ�� �ٽ� ť�� ���� �ִ´�.
    public void InsertQueue(GameObject note, int line)
    {
        note.SetActive(false);
        noteQueue[line].Enqueue(note);
        noteContainer[line].Remove(note);
    }

    //��Ʈ�� ť���� �����´�. ���ÿ� ��Ʈ�� Ȱ��ȭ ��Ű�� ��Ʈ�� ��ġ�� �ʱ�ȭ��Ų��.
    private void GetQueue(int line)
    {
        //���� Ǯ�� ��Ʈ�� ���ٸ� �ϳ� ������ش�^^.
        if (noteQueue[line].Count == 0)
        {
            GameObject newNote = GenerateNote(line);
            newNote.transform.SetParent(GameObject.Find("NoteLineMask").transform);
            newNote.SetActive(false);
            noteQueue[line].Enqueue(newNote); ;
            noteManager.Add(newNote);
        }
        GameObject note = noteQueue[line].Dequeue();
        noteContainer[line].Add(note);
        note.transform.position = NoteAppearLocation[line].position + new Vector3(0, Sync, 0);
        note.SetActive(true);
    }

    //�ٲ� BPM�� �����ϴ� �Լ�.
    public void ChangeBPM(double bpmRatio, double preBpmRatio)
    {
        currentTime = currentTime * preBpmRatio / bpmRatio;
        ChangeAllNoteSpeed((int)NoteSpeed);
    }

    public void SyncUpdate(int newSync)
    {
        int SyncDiff = this.Sync - newSync;
        foreach(GameObject note in noteManager)
        {
            note.transform.localPosition += Vector3.up * SyncDiff;
        }
        this.Sync = newSync;
    }

    //�Ͻ������� �������� �ٽ� �����ϴ� �Լ�. (��� ��Ʈ�� �ӵ��� �������� ������ �ð��� �ٽ� �帥��.)
    public void Go()
    {
        run = true;
        ChangeAllNoteSpeed((int)NoteSpeed);
    }

    //�Ͻ����� ��Ű�� �Լ�. (��� ��Ʈ�� �ӵ��� 0���� �ٲٸ� �ð��� ������Ų��.)
    public void Stop()
    {
        run = false;
        ChangeAllNoteSpeed(0);
    }

    //��� ��Ʈ�� �ӵ��� �ٲٴ� �Լ�.
    public void ChangeAllNoteSpeed(int NoteSpeed)
    {
        foreach (GameObject note in noteManager)
        {
            note.GetComponent<Note>().noteSpeed = NoteSpeed;
        }
    }

    //Perfect, Good ���� �αװ� ������ �Լ�. �Ŀ� �����������.
    public void DebugLogNote(int score)
    {
        switch (score)
        {
            case 0:
                panjung.text = "Perfect";
                Debug.Log("Perfect");
                break;
            case 1:
                panjung.text = "Great";
                Debug.Log("Great");
                break;
            case 2:
                panjung.text = "Good";
                Debug.Log("Good");
                break;
            case 3:
                panjung.text = "Bad";
                Debug.Log("Bad");
                break;
            default:
                panjung.text = "Miss";
                Debug.Log("Miss");
                break;
        }
    }

    //�̵��Ʈ�� ���� �ش��ϴ� �������� ������ ���� �Լ�.
    Dictionary<byte, int> soundNum = new Dictionary<byte, int>()
        {
            {49, 0 }, //Crash1
            {55, 0 },
            {46, 1 }, //Hi hat open
            {26, 1 },
            {42, 2 }, //Hi hat close
            {22, 2 },
            {44, 2 },
            {38, 3 }, //Smare
            {40, 3 },
            {36, 4 }, //Kick
            {48, 5 }, //Tom1
            {50, 5 },
            {45, 6 }, //Tom2
            {47, 6 },
            {41, 7 }, //Tom3
            {39, 7 },
            {51, 8 }, //Ride
            {59, 8 },
            {53, 8 },
            {57, 9 }, //Crash2
            {52, 9 },
            {255, -1 }
        };
}
