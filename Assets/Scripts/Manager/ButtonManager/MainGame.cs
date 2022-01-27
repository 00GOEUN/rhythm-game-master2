using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    [SerializeField] GameObject controlCenter = null;
    private Center center = null;

    void Awake()
    {
        center = controlCenter.GetComponent<Center>();
    }
    // Start is called before the first frame update
    public void BPMUpClick()
    {
        center.BPMUpClick();
    }

    public void BPMDownClick()
    {
        center.BPMDownClick();
    }

    public void RepeatClick()
    {
        center.RepeatClick();
    }

    public void PlayClick()
    {
        center.PlayClick();
    }

    public void PauseClick()
    {
        center.PauseClick();
    }

    public void SyncUpClick()
    {
        center.SyncUpClick();
    }

    public void SyncDownClick()
    {
        center.SyncDownClick();
    }

    public void ReturnClick()
    {
        GameObject.Find("Player").GetComponent<SongSelector>().Stop();
        PlayerSingleton.GetInstance.CheckScene = 0;
        Debug.Log("CheckScene : " + PlayerSingleton.GetInstance.CheckScene);
        SceneManager.LoadScene("Menu");
    }
}
