using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private float waitToLoad = 4f;
    [SerializeField] private GameObject winLabel;
    [SerializeField] private GameObject loseLabel;
    [SerializeField] private GameObject levelIntroCanvas;
    private int numberOfAttackers = 0;
    private bool levelTimerFinished = false;
    private bool gameStart;
    private bool cr_running;

    private void Start()
    {
        winLabel.SetActive(false);
        loseLabel.SetActive(false);
        gameStart = false;
        levelIntroCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void StartTheGame()
    {
        gameStart = true;
        levelIntroCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void AttackerSpawned()
    {
        numberOfAttackers++;
    }
    
    public void AttackerKilled()
    {
        numberOfAttackers--;

        if (numberOfAttackers <= 0 && levelTimerFinished)
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    IEnumerator HandleWinCondition()
    {
        winLabel.SetActive(true);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(waitToLoad);
        FindObjectOfType<LevelLoader>().LoadNextScene();
    }

    public void HandleLoseCondition()
    {
        loseLabel.SetActive(true);
        Time.timeScale = 0;
    }

    public void LevelTimerFinished()
    {
        levelTimerFinished = true;
        StopSpawners();
    }

    private void StopSpawners()
    {
        AttackerSpawner[] spawnerArray = FindObjectsOfType<AttackerSpawner>();
        foreach (AttackerSpawner spawner in spawnerArray)
        {
            spawner.StopSpawning();
        }
    }
}
