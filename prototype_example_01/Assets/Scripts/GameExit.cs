using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameExit : MonoBehaviour
{
    public Slider QuitBar;
    float mQuitTimer = 0.0f;
    
    void Update()
    {
        // if R key pressed...
        if (Input.GetKey(KeyCode.Escape))
            mQuitTimer += Time.deltaTime;
        else
            mQuitTimer = 0.0f;

        if (mQuitTimer > 1.0f)
        {
            #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }

        QuitBar.value = mQuitTimer;
    }
}
