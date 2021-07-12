using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float restartDelay = 1f;
    public GameObject completeLevelUI;
    
    [HideInInspector]
    public bool isRunning = true;

    private void Start()
    {
        PlayStartSound();
    }

    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
    }
    
    public void EndGame()
    {
        if (!isRunning)
            return;
        isRunning = false;
        Invoke("Restart", restartDelay);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayStartSound();
    }

    private void PlayStartSound()
    {
        FindObjectOfType<AudioManager>().Play("Lets Go");
    }
}
