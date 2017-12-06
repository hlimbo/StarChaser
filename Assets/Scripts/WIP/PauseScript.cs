using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour {

    public Text countdown;
    public Text pauseText;
    public GameObject resumeButton;
    public bool firstRun = true;//used to stop the game from pausing at the very beginning

    [Tooltip("Amount of seconds before gameplay resumes")]
    public float delayDuration;

    private IEnumerator Countdown()
    {
        countdown.enabled = true;
        pauseText.enabled = false;
        resumeButton.SetActive(false);
        float startTime = Time.unscaledTime;
        float elapsedTime = Time.unscaledTime - startTime;
        int counter = 0;
        while (elapsedTime < delayDuration)
        {
            Debug.Log("Resuming game in: " + (delayDuration - elapsedTime));
            yield return new WaitForSecondsRealtime(1f);
            countdown.text = (delayDuration - ++counter).ToString();
            elapsedTime = Time.unscaledTime - startTime;
        }

        pauseText.enabled = true;
        resumeButton.SetActive(true);
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        yield return null;
    }

    public void PauseDelay()
    {
        StartCoroutine(Countdown());
    }

    void OnEnable()
    {
        if (!firstRun)
        {
            countdown.text = delayDuration.ToString();
            countdown.enabled = false;
            Time.timeScale = 0f;
        }
    }

    void OnDisable()
    {
        //Time.timeScale = 1f;
        firstRun = false;
    }

}
