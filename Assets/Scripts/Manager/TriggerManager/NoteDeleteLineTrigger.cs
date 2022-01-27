using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDeleteLineTrigger : MonoBehaviour
{
    static int MAX_LINE = 9;
    NoteManager noteManager = null;
    [SerializeField] GameObject[] NoteType = null;

    void Start()
    {
        noteManager = GameObject.Find("NoteCanvas").GetComponent<NoteManager>();
    }

    //치지 못하고 넘어온 노드를 삭제하는 코드
    private void OnTriggerExit2D(Collider2D collision)
    {
        noteManager.DebugLogNote(-1);
        if (collision.CompareTag("Note"))
        {
            for (int line = 0; line < MAX_LINE; line++)
            {
                if (NoteType[line].name == collision.gameObject.name)
                {
                    noteManager.InsertQueue(collision.gameObject, line);
                    return;
                }
            }
        }
    }
}
