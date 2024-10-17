using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SimpleTimer : MonoBehaviour
{
    public float targetTime = 60.0f;

    void Update() // Add void here
    {
        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }
    }

    void timerEnded()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
