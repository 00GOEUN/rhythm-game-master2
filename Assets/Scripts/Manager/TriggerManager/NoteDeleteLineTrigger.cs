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

    //ġ�� ���ϰ� �Ѿ�� ��带 �����ϴ� �ڵ�
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
