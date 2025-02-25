﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] GameObject deathFX;


    int hitCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish") StartWinSequence();
        else StartDeathSequence();
    }

    private void StartWinSequence()
    {
        print("game Won");
        gameObject.SendMessage("OnGameWon");
    }

    private void StartDeathSequence()
    {
        print("Player dying");
        Invoke("ReloadLevel", 1f);
        deathFX.SetActive(true);
        gameObject.SendMessage("OnPlayerDeath");
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(1);
    }
}
