using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float noteSpeed = 200;
    // Update is called once per frame

    void Update()
    {
        transform.localPosition += Vector3.down * noteSpeed * Time.deltaTime;
    }
}
