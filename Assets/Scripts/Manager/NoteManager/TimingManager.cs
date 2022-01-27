using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingManager : MonoBehaviour
{
    NoteManager manager;

    List<List<GameObject>> boxNoteList = null;

    [SerializeField] RectTransform[] timingRect = null;
    Vector2[] timingBoxs = null;
    PlayerInformation PlayerScore = null;
    Text ScoreText = null;
    int[] score = null;


    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<NoteManager>();
        //Debug.Log(manager);
        boxNoteList = manager.noteContainer;
        timingBoxs = new Vector2[timingRect.Length];

        for(int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(timingRect[i].localPosition.y + timingRect[i].rect.height / 2,
                              timingRect[i].localPosition.y - timingRect[i].rect.height / 2);
        }
        PlayerScore = GameObject.Find("Player").GetComponent<PlayerInformation>();
        ScoreText = GameObject.Find("Score").GetComponent<Text>();
        score = new int[4] { 100, 80, 50, 20 };
    }

    //캔버스에 만들어둔 판정선 위치에 따라서 노트를 판정한다. 해당 노트는 다시 풀로 돌아간다.
    // https://ansohxxn.github.io/unity%20lesson%204/ch1/ 참고
    public void CheckTiming(int line)
    {
        for(int i = 0; i < boxNoteList[line].Count; i++)
        {
            float t_notePosY = boxNoteList[line][i].transform.localPosition.y + GameObject.Find("NoteLineMask").transform.localPosition.y;

            for (int y = 0; y < timingBoxs.Length; y++)
            {
                if (timingBoxs[y].x >= t_notePosY && t_notePosY >= timingBoxs[y].y)
                {
                    manager.InsertQueue(boxNoteList[line][i], line);
                    PlayerScore.score += score[y];
                    ScoreInput();
                    manager.DebugLogNote(y);
                    return;
                }
            }
        }
    }

    void ScoreInput()
    {
        string scoreText = PlayerScore.score.ToString();
        string newScoreText = string.Empty;
        for (int i = 0; i < 6 - scoreText.Length; i++)
            newScoreText += "0";
        newScoreText += scoreText;
        ScoreText.text = newScoreText;
    }
}
