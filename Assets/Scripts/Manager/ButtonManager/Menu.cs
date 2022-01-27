using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.WSA;

public class Menu : MonoBehaviour
{
    public void ChooseMusicClick()
    {
        GameObject.Find("ScrollViewCanvas").transform.Find("ScrollView").gameObject.SetActive(true);
    }

    public void SongNameClick()
    {
        string SongName = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        SongName = SongName.Replace(" ", "_");
        GameObject.Find("Player").GetComponent<SongSelector>().Go(SongName);
        SceneManager.LoadScene("mainGame");
    }
}
