﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadFirstScene", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void LoadFirstScene()
    {
        SceneManager.LoadScene(1);
    }
}
