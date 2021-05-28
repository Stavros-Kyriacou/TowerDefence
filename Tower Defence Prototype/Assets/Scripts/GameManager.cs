using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool fps;
    private void Start()
    {
        if (fps)
        {
            Application.targetFrameRate = 60;

        }
    }
}
