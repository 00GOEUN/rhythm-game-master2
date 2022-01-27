
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    public bool isNoteManagerReady { get; set; }
    /*
     나중에 싱크 조절할때 나와있는 모든 노트의 싱크를 조절하기 위해 
     모든 노트를 하나의 리스트에 집어 넣고 싱크의 변화가 있을 경우 
     해당 노트들의 위치값을 변경시킨다.
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

    //스크립트 모음
    NoteGenerator generator = null;

    //상수 모음
    static int MAX_LINE = 10;
    static int START_NOTE_NUM = 20;

    //static int MAX_SYNC = 30;
    //static int MIN_SYNC = -30;

    //컨테이너 모음
    List<Queue<GameObject>> noteQueue = null;
    public List<List<GameObject>> noteContainer = null;
    public List<GameObject> noteManager = null;

    //노트 관련 변수 모음
    double currentTime = 0;
    int currentNote = 0;

    bool run = true;

    Text panjung = null;

    void Awake()
    {
        isNoteManagerReady = false;
        //NoteGenerator 스크립트에서 미디 파일을 읽고 노트 정보만 쏙 빼와서 노트 리스트를 만든다.
        generator = GetComponent<NoteGenerator>();
        generator.fname = fname;
        generator.timeDelay = ((timeDelay / 1000) / beatRate) * 480 ;

        noteQueue = new List<Queue<GameObject>>();
        noteContainer = new List<List<GameObject>>();

        currentTime += 0;

        panjung = GameObject.Find("panjung").GetComponent<Text>();

        //각 라인마다 오브젝트 풀을 만들어 20개씩 노트를 생성한 뒤 비활성화 한 상태로 큐에 집어 넣는다.
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

    /* 미디노트들은 한 박자에 480 델타타임으로 이루어져 있으며 이는 BPM과 무관하다.
     * 그래서 이를 실제 박자에 맞추기 위해서 우선 480으로 나누어 한 박자를 기준으로 맞춘 후 해당 박자를 BPM에 맞춘다.
     * beatRate는 60/BPM이다.*/
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

    //구간 반복을 위한 함수. 노트를 해당 구간으로 리셋 시킨다.
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

    //노트를 생성 후 비활성화 시킨다.
    GameObject GenerateNote(int line)
    {
        GameObject noteType = NoteType[line];
        Vector3 position = NoteAppearLocation[line].position + new Vector3(0, Sync, 0); //노트생성 라인에서 설정된 싱크만큼 프레임을 더한다.

        GameObject note = Instantiate(noteType, position, Quaternion.identity);
        
        note.transform.SetParent(GameObject.Find("NoteLineMask").transform);
        note.SetActive(false);
        note.name = noteType.name; //클론의 이름을 원본과 같게 해 나중에 비교하기 쉽게 만든다.
        return note;
    }


    //노트를 다시 큐에 집어 넣는다.
    public void InsertQueue(GameObject note, int line)
    {
        note.SetActive(false);
        noteQueue[line].Enqueue(note);
        noteContainer[line].Remove(note);
    }

    //노트를 큐에서 꺼내온다. 동시에 노트를 활성화 시키며 노트의 위치를 초기화시킨다.
    private void GetQueue(int line)
    {
        //만약 풀에 노트가 없다면 하나 만들어준다^^.
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

    //바뀐 BPM을 적용하는 함수.
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

    //일시정지된 구간부터 다시 시작하는 함수. (모든 노트의 속도를 정상으로 돌리며 시간이 다시 흐른다.)
    public void Go()
    {
        run = true;
        ChangeAllNoteSpeed((int)NoteSpeed);
    }

    //일시정지 시키는 함수. (모든 노트의 속도를 0으로 바꾸며 시간을 정지시킨다.)
    public void Stop()
    {
        run = false;
        ChangeAllNoteSpeed(0);
    }

    //모든 노트의 속도를 바꾸는 함수.
    public void ChangeAllNoteSpeed(int NoteSpeed)
    {
        foreach (GameObject note in noteManager)
        {
            note.GetComponent<Note>().noteSpeed = NoteSpeed;
        }
    }

    //Perfect, Good 등의 로그가 나오는 함수. 후에 변경해줘야함.
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

    //미디노트에 따라 해당하는 라인으로 보내기 위한 함수.
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
