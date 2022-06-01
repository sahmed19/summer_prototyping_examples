using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restarter : MonoBehaviour
{
    public Slider RestartBar;
    float mRestartTimer = 0.0f;
    
    void Update()
    {
        // if R key pressed...
        if (Input.GetKey(KeyCode.R))
            mRestartTimer += Time.deltaTime;
        else
            mRestartTimer = 0.0f;

        if (mRestartTimer > 1.0f)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        RestartBar.value = mRestartTimer;
    }
}
