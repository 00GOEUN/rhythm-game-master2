using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SheetMusicManager : MonoBehaviour
{
    public bool isSheetMusicManagerReady { get; set; }
    public GameObject imageL { get; set; }
    public GameObject imageR { get; set; }
    public GameObject HightLight { get; set; }
    public Sprite[] sprite { get; set; }
    public double timeDelay { get; set; }
    public double beatRate { get; set; }
    public byte definitionNum { get; set; }
    public double noteSpeed { get; set; }

    double currentTime = 0;

    [SerializeField] float SheetMusicMoveFrame = 0;
    [SerializeField] float orgHightLightPosx = 0;
    [SerializeField] float orgHightLightPosy = 0;
    [SerializeField] float HightLightMoveFrame = 0;
    double[] lineTimeL = null;
    byte currentLineL = 0;
    byte currentSpriteL = 0;
    Vector3 orgPositionL = Vector3.zero;

    double[] lineTimeR = null;
    byte currentLineR = 0;
    byte currentSpriteR = 0;
    Vector3 orgPositionR = Vector3.zero;

    Vector3[] hightLightPos = null;
    Vector3 orgHightLightPos = Vector3.zero;
    byte hightLightCount = 0;

    bool run = true;

    void Awake()
    {
        currentTime = -(GameObject.Find("NoteAppearLine").transform.localPosition.y - GameObject.Find("BeatLine").transform.localPosition.y) / noteSpeed;
        isSheetMusicManagerReady = false;
        orgPositionL = imageL.transform.localPosition + Vector3.up * SheetMusicMoveFrame;
        orgPositionR = imageR.transform.localPosition + Vector3.up * SheetMusicMoveFrame;

        imageL.GetComponent<Image>().sprite = sprite[currentSpriteL];
        imageR.GetComponent<Image>().sprite = sprite[currentSpriteR];

        lineTimeL = new double[definitionNum / 4];
        lineTimeR = new double[definitionNum / 4];

        hightLightPos = new Vector3[4];
        orgHightLightPos = new Vector3(orgHightLightPosx, orgHightLightPosy, 0f);
        isSheetMusicManagerReady = true;
        Debug.Log("SheetManager Ready");
    }


    void Start()
    {
        PlayerSingleton.GetInstance.CheckScene = 1;
        lineTimeL[0] = timeDelay / 1000;
        lineTimeR[0] = timeDelay / 1000;
        for (int i = 2; i < definitionNum / 2; i++)
        {
            if (i % 2 == 0)
                lineTimeL[i / 2] = 8 + lineTimeR[(i / 2) - 1];
            else
                lineTimeR[i / 2] = 8 + lineTimeL[i / 2];
        }
        for(int i = 0; i < 4; i++)
        {
            hightLightPos[i] = orgHightLightPos + (Vector3.right * HightLightMoveFrame) * i;
        }
    }

    /* 기본적으로 2 마디마다 왼쪽과 오른쪽 악보가 서로 번갈아가면서 넘어가는 코드다.
     * 매 페이지마다 10개의 라인이 있으며 10개를 다 읽고나면 다음 악보로 넘어가야한다.
     * 각 라인마다 8박자를 배정했으며 (나중에 박자에 따라서 자동으로 바뀌게끔 코드 재작성 필요)
     * 이 8박자를 현재 BPM에 맞춰 시간을 계산한다.
     * 또한 하이라이터는 첫마디부터 시작해서 매 4 마디마다 다시 처음으로 돌아가며 이는 곡이 끝날 때 까지 반복된다.
     * 
     */
    void Update()
    {
        if (run)
        {
            currentTime += Time.deltaTime;
            if ((currentLineL + currentSpriteL * 10) < (definitionNum / 4) && currentTime + 0.18f > lineTimeL[currentLineL + currentSpriteL * 10] * beatRate)
            {
                if (currentLineL < 10)
                    imageL.transform.localPosition += Vector3.up * SheetMusicMoveFrame;
                else
                {
                    currentLineL = 0;
                    currentSpriteL++;
                    imageL.GetComponent<Image>().sprite = sprite[currentSpriteL];
                    imageL.transform.localPosition = orgPositionL;
                }
                currentLineL++;
            }
            if ((currentLineR + currentSpriteR * 10) < (definitionNum / 4) && currentTime + 0.18f > lineTimeR[currentLineR + currentSpriteR * 10] * beatRate)
            {
                if (currentLineR < 10)
                    imageR.transform.localPosition += Vector3.up * SheetMusicMoveFrame;
                else
                {
                    currentLineR = 0;
                    currentSpriteR++;
                    imageR.GetComponent<Image>().sprite = sprite[currentSpriteR];
                    imageR.transform.localPosition = orgPositionR;
                }
                currentLineR++;
            }
            if (hightLightCount < definitionNum && currentTime + 0.2f > (timeDelay / 1000) + beatRate * 4 * hightLightCount)
            {
                HightLight.transform.localPosition = hightLightPos[hightLightCount++ % 4];
            }
        }
    }

    //구간 반복을 위한 함수. 해당 구간보다 조금 전으로 악보를 되돌린다. 이는 노트가 내려오는 시간에 맞추기 위함이다.
    public void Repeat(double startTime, double noteTime)
    {
        currentTime = startTime - 4 * beatRate;
        currentLineL = 0;
        currentLineR = 0;
        currentSpriteL = 0;
        currentSpriteR = 0;
        imageL.GetComponent<Image>().sprite = sprite[0];
        imageL.transform.localPosition = orgPositionL + Vector3.down * SheetMusicMoveFrame;
        imageR.GetComponent<Image>().sprite = sprite[0];
        imageR.transform.localPosition = orgPositionR + Vector3.down * SheetMusicMoveFrame;
        hightLightCount = 0;

        while ((currentLineL + currentSpriteL * 10) < (definitionNum / 4) && currentTime > lineTimeL[currentLineL + currentSpriteL * 10] * beatRate)
        {
            if (currentLineL < 10)
                imageL.transform.localPosition += Vector3.up * SheetMusicMoveFrame;
            else
            {
                currentLineL = 0;
                currentSpriteL++;
                imageL.GetComponent<Image>().sprite = sprite[currentSpriteL];
                imageL.transform.localPosition = orgPositionL;
            }
            currentLineL++;
        }
        while ((currentLineR + currentSpriteR * 10) < (definitionNum / 4) && currentTime > lineTimeR[currentLineR + currentSpriteR * 10] * beatRate)
        {
            if (currentLineR < 10)
                imageR.transform.localPosition += Vector3.up * SheetMusicMoveFrame;
            else
            {
                currentLineR = 0;
                currentSpriteR++;
                imageR.GetComponent<Image>().sprite = sprite[currentSpriteR];
                imageR.transform.localPosition = orgPositionR;
            }
            currentLineR++;
        }
        while (hightLightCount < definitionNum && currentTime + 0.2f > (timeDelay / 1000) + beatRate * 4 * (hightLightCount))
        {
            HightLight.transform.localPosition = hightLightPos[hightLightCount++ % 4];
        }
        Go();
    }

    public void Go()
    {
        run = true;
    }

    public void Stop()
    {
        run = false;
    }

    public void ChangeBPM(double bpmRatio, double preBpmRatio)
    {
        currentTime = currentTime * preBpmRatio / bpmRatio;
    }
}
