using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluetoothLEDLineTrigger : MonoBehaviour
{
    static int MAX_LINE = 9;
    NoteManager noteManager = null;
    BluetoothManager bluetoothManager = null;
    [SerializeField] GameObject[] NoteType = null;
    [SerializeField] Image[] ImageType = null;
    double currentTime = 0;
    bool[] LEDStatus = null;
    double[] LEDTime = null;
    public double LEDDuration { get; set; }

    void Start()
    {
        noteManager = GameObject.Find("NoteCanvas").GetComponent<NoteManager>();
        bluetoothManager = GameObject.Find("Player").GetComponent<BluetoothManager>();
        LEDStatus = new bool[MAX_LINE];
        LEDTime = new double[MAX_LINE];
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        for(int line = 0; line < MAX_LINE; line++)
        {
            if(LEDStatus[line] && currentTime >= LEDTime[line])
            {
                ImageType[line].color = new Color(1, 1, 1);
                if (line == 2)
                    ImageType[1].color = new Color(1, 1, 1);
                string noteInfo = "S" + line.ToString() + "FE";
                //Debug.Log(noteInfo);
                LEDStatus[line] = false;
                //bluetoothManager.SendData(noteInfo);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            for (int line = 0; line < MAX_LINE; line++)
            {
                if (NoteType[line].name == collision.gameObject.name)
                {
                    LEDTime[line] = currentTime + LEDDuration;
                    if (!LEDStatus[line])
                    {
                        ImageType[line].color = new Color(155 / 255, 1, 1);
                        if (line == 2)
                        {
                            ImageType[1].color = new Color(155 / 255, 1, 155 / 255);
                            ImageType[2].color = new Color(155 / 255, 1, 155 / 255);
                        }
                        string noteInfo = "S" + line.ToString() + "OE";
                        //Debug.Log(noteInfo);
                        LEDStatus[line] = true;
                        //bluetoothManager.SendData(noteInfo);
                    }
                    return;
                }
            }
        }
    }
}
