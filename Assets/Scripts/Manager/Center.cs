using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Center : MonoBehaviour

    /* 수정사항
     * 4분의4박자 8분의 6박자등 박자 인식 기능 추가. 이에 따른 코드 변경
     */
{
    //종속된 오브젝트들
    BluetoothManager bluetoothManager = null;
    SongSelector songSelector = null;
    PlayerInformation playerInformation = null;
    NoteManager noteManager = null;
    AudioManager audioManager = null;
    SheetMusicManager sheetMusicManager = null;

    ////에디터
    //NoteManager
    string fname = string.Empty;
    double timeDelay = 0;
    double BPM = 0;
    float NoteSpeed = 0;
    double BeatDistance = 0;
    int Sync = 0;
    double beatRate = 0;

    //AudioManager
    Sound BGM = null;
    Sound MIDI = null;
    
    //SheetMusicManager 
    byte definitionNum = 0;
    Sprite[] sprite = null;

    //Speed Editor Variable
    Text BPMText = null;
    Text SyncText = null;
    double bpmRatio = 0;
    double preBpmRatio = 0;
    float pitch = 0;

    //SendToBluetooth
    bool LED = false;
    float LEDTime = 0;
    double LEDDuration = 0;
    [SerializeField] GameObject beatLine = null;
    [SerializeField] GameObject bluetoothLEDLine = null;

    //Repeat Method Variable
    InputField RepeatA = null;
    InputField RepeatB = null;

    //초기 변수
    double orgBpm = 0;
    float orgNoteSpeed = 0;

    //Repeat
    bool loop = false;
    double currentTime = 0;
    double startTime = 0;
    double endTime = 0;

    bool run = true;
    bool pause = false;


    void Awake()
    {
        Debug.Log("Center Awake");
        bluetoothManager = GameObject.Find("Player").GetComponent<BluetoothManager>();
        songSelector = GameObject.Find("Player").GetComponent<SongSelector>();
        playerInformation = GameObject.Find("Player").GetComponent<PlayerInformation>();
        noteManager = GameObject.Find("NoteCanvas").GetComponent<NoteManager>();
        audioManager = GameObject.Find("AudioBox").GetComponent<AudioManager>();
        sheetMusicManager = GameObject.Find("SheetMusicCanvas").GetComponent<SheetMusicManager>();

        fname = songSelector.midipath;
        timeDelay = songSelector.timeDelay;
        BPM = songSelector.BPM;
        NoteSpeed = playerInformation.NoteSpeed;
        BeatDistance = songSelector.BeatDistance;
        Sync = songSelector.Sync;
        beatRate = (60d / BPM) * (BeatDistance / 100d);

        BGM = songSelector.BGM;
        MIDI = songSelector.MIDI;

        definitionNum = songSelector.definitionNum;
        sprite = songSelector.sprite;

        bpmRatio = 1;
        preBpmRatio = 1;
        pitch = 1;

        LED = playerInformation.LED;
        LEDTime = playerInformation.LEDTime;
        LEDDuration = playerInformation.LEDDuration;

        noteManager.fname = fname;
        noteManager.timeDelay = timeDelay;
        noteManager.NoteSpeed = NoteSpeed;
        noteManager.BeatDistance = BeatDistance;
        noteManager.Sync = Sync;
        noteManager.beatRate = beatRate;
        Debug.Log("NoteManager inicialiting compelete");

        audioManager.noteSpeed = playerInformation.NoteSpeed;
        audioManager.bgm = BGM;
        audioManager.midi = MIDI;
        Debug.Log("audioManager inicialiting compelete");

        sheetMusicManager.noteSpeed = playerInformation.NoteSpeed;
        sheetMusicManager.imageL = GameObject.Find("SheetMusicLeft");
        sheetMusicManager.imageR = GameObject.Find("SheetMusicRight");
        sheetMusicManager.HightLight = GameObject.Find("HightLight");
        sheetMusicManager.timeDelay = timeDelay;
        sheetMusicManager.beatRate = beatRate;
        sheetMusicManager.definitionNum = definitionNum;
        sheetMusicManager.sprite = sprite;
        Debug.Log("sheetMusicManager inicialiting compelete");

        BPMText = GameObject.Find("CurrentBPMText").GetComponent<Text>();
        BPMText.text = string.Format("{0:0.00}", bpmRatio);
        SyncText = GameObject.Find("CurrentSyncText").GetComponent<Text>();
        SyncText.text = string.Format("{0:0.00}", (double)Sync / 10d);

        RepeatA = GameObject.Find("RepeatA").GetComponent<InputField>();
        RepeatB = GameObject.Find("RepeatB").GetComponent<InputField>();

        orgBpm = BPM;
        orgNoteSpeed = NoteSpeed;

        if (LED)
        {
            if (LEDTime < 0) LEDTime = 0;
            if (LEDDuration < 0.1) LEDDuration = 0.1;
            bluetoothLEDLine.transform.localPosition = beatLine.transform.localPosition + Vector3.up * NoteSpeed * LEDTime;
            bluetoothLEDLine.GetComponent<BluetoothLEDLineTrigger>().LEDDuration = LEDDuration;
        }
        else
            bluetoothLEDLine.SetActive(false);
    }

    void Update()
    {
        //일시정지, 구간반복, 재시작 등을 관리한다.
        if(!run)
        {
            if (!pause)
                if (isReady()) Go();
        }
        else
        {
            if (!isReady()) Stop();
            if (loop)
            {
                currentTime += Time.deltaTime;
                if (currentTime > endTime)
                {
                    RepeatClick();
                }
            }
        }
    }

    //모든 항목이 준비 되었는지 확인하는 함수
    bool isReady()
    {
        if (!noteManager.isNoteManagerReady) return false;
        if (!audioManager.isAudioManagerReady) return false;
        if (!sheetMusicManager.isSheetMusicManagerReady) return false;
        //if (!bluetoothManager.isBluetoothManagerReady) return false;
        return true;
    }

    //게임이 일시정지 된 구간부터 시작하는 함수
    void Go()
    {
        noteManager.Go();
        audioManager.Go();
        sheetMusicManager.Go();
        run = true;
    }

    //게임을 일시정지 시키는 함수
    void Stop()
    {
        noteManager.Stop();
        audioManager.Stop();
        sheetMusicManager.Stop();
        run = false;
    }

    //BPM을 증가시키는 버튼을 눌렀을 시 10%만큼 증가시키고 모든 오브젝트에 적용시킨다.
    public void BPMUpClick()
    {
        if (bpmRatio < 1.5d)
        {
            preBpmRatio = bpmRatio;
            bpmRatio += 0.01d;
            pitch += 0.01f;
            ChangeBPM();
        }
        Debug.Log("BPMUpClick");
    }

    //BPM을 줄이는 버튼을 눌렀을 시 10%만큼 줄이고 모든 오브젝트에 적용시킨다.
    public void BPMDownClick()
    {
        if (bpmRatio > 0.5d)
        {
            preBpmRatio = bpmRatio;
            bpmRatio -= 0.01d;
            pitch -= 0.01f;
            ChangeBPM();
        }
        Debug.Log("BPMDownClick");
    }

    //바뀐 BPM을 모든 오브젝트에 적용시키는 함수.
    void ChangeBPM()
    {
        BPM = orgBpm * bpmRatio;
        beatRate = (60d / BPM) * (BeatDistance / 100d);
        NoteSpeed = orgNoteSpeed * (float)bpmRatio;

        noteManager.beatRate = beatRate;
        noteManager.NoteSpeed = NoteSpeed;
        noteManager.ChangeBPM(bpmRatio, preBpmRatio);

        audioManager.ChangeBPM(pitch);

        sheetMusicManager.beatRate = beatRate;
        sheetMusicManager.ChangeBPM(bpmRatio, preBpmRatio);

        BPMText.text = string.Format("{0:0.00}", bpmRatio);
    }

    
    //구간 반복을 실행하는 함수.
    public void RepeatClick()
    {
        Stop();
        try
        {
            int start = Int32.Parse(RepeatA.text);
            if (start < 1) { 
                start = 1; 
                RepeatA.text = "1"; 
            }
            if (start > definitionNum)
            {
                start = definitionNum - 1;
                RepeatA.text = Convert.ToString(definitionNum - 1);
            }

            int end = Int32.Parse(RepeatB.text);
            if (end < start)
            {
                end = start;
                RepeatB.text = Convert.ToString(start);
            }
            if (end > definitionNum)
            {
                end = definitionNum;
                RepeatB.text = Convert.ToString(definitionNum);
            }

            startTime = ((timeDelay / 1000d) + ((double)(start - 1) * 4d)) * beatRate;
            endTime = ((timeDelay / 1000d) + ((double)end * 4d)) * beatRate;
            currentTime = startTime - 626d / NoteSpeed;

            noteManager.Repeat(startTime);
            sheetMusicManager.Repeat(startTime, 626d / NoteSpeed);
            audioManager.Repeat(startTime, 626d / NoteSpeed);

            loop = true;
        }
        catch (NullReferenceException ex) { Debug.Log(ex); Go(); }
        catch (FormatException ex) { Debug.Log(ex); RepeatA.text = "1"; RepeatB.text = "1"; Go(); }
    }

    //구간반복 중 게임 흐름을 정상으로 돌리는 함수
    public void PlayClick()
    {
        loop = false;
    }

    //일시정지 및 정지 해제를 담당하는 함수
    public void PauseClick()
    {
        if (!pause)
        {
            Stop();
            pause = true;
        }
        else
        {
            Go();
            pause = false;
        }
    }

    //싱크 기능은 아직 최적화 안됨.
    public void SyncUpClick()
    {
        if(Sync < 50) {
            Sync += 5;
            SyncUpdate();
        }
    }

    public void SyncDownClick()
    {
        if(Sync > -50) {
            Sync -= 5;
            SyncUpdate();
        }
    }
    
    private void SyncUpdate()
    {
        noteManager.SyncUpdate(Sync);
        SyncText.text = string.Format("{0:0.00}", (double)Sync / 10d);
    }
}
