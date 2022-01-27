using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour
{
    [SerializeField] GameObject content = null;
    List<GameObject> SongList = null;

    void Start()
    {
        content.transform.localPosition = new Vector3(0, 0, 0);

        string songListTxt = "Assets\\Resources\\SongList.txt";
        FileStream file =  new FileStream(songListTxt, FileMode.Open);
        StreamReader str = new StreamReader(file);

        SongList = new List<GameObject>();

        while(!str.EndOfStream)
        {
            GameObject button = Instantiate(Resources.Load<GameObject>("SongButton"));
            if (button != null)
            {
                button.transform.SetParent(content.transform);
                button.GetComponentInChildren<Text>().text = str.ReadLine();
                button.SetActive(true);
                SongList.Add(button);
            }
        }
    }
}
