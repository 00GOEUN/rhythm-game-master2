using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton
{
    private static PlayerSingleton Instance = null;

    public static PlayerSingleton GetInstance
    {
        get
        {
            if (Instance == null)
                Instance = new PlayerSingleton();

            return Instance;
        }
    }

    public int CheckScene;
    public GameObject NoteCanvas;

}
