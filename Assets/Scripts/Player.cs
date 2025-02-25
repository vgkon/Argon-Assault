﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("General")] 
    [Tooltip("In m/s^-1")][SerializeField] float speed = 35f;
    [Tooltip("In m")] [SerializeField] float xRange = 22f;
    [Tooltip("In m")] [SerializeField] float yRange = 17f;
    [SerializeField] GameObject[] guns;

    [Header("Screen-position Based")]
    [SerializeField] float positionPitchFactor = -2.2f;
    [SerializeField] float positionYawFactor = -2.2f;

    [Header("Control-throw Based")]
    [SerializeField] float controlPitchFactor = -18f;
    [SerializeField] float controlRollFactor = -30f;
    [SerializeField] float controlYawFactor = -10f;

    [SerializeField] Canvas escMenu;
    [SerializeField] Canvas winMenu;
    [SerializeField] Canvas points;
    bool controlsActive = true;

    float yThrow;
    float prevYThrow;
    float xThrow;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (controlsActive)
        {
            ReactEscapeInput();
            ReactHorizontalInput();
            ReactVerticalInput();
            Rotate();
            ReactFiring();
        }
    }

    private void OnGameWon()
    {
        points.enabled = false;
        winMenu.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void ReactEscapeInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            points.enabled = false;
            escMenu.enabled = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void ReactFiring()
    {
        if (Input.GetButton("Fire"))
        { 
            print("FIRING");
            ActivateGuns(true);
        }
        else
        {
            ActivateGuns(false);
        }
    }

    private void DeactivateGuns()
    {
        foreach(GameObject gun in guns)
        {
            gun.SetActive(false);
        }
    }

    private void ActivateGuns(bool emission)
    {
        foreach (GameObject gun in guns)
        {
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = emission;
            print(emissionModule.enabled);
        }
    }

    void OnPlayerDeath()
    {
        controlsActive = false;
        print("controls disabled");
    }

    bool SameSign(float a, float b)
    {
        if ((a < 0 && b < 0) || (a > 0 && b > 0) || (a >= b - Mathf.Epsilon && a <= b + Mathf.Epsilon))
        {
            print("Same sign: pitchDueToThrow = " + a + " yThrow = " + b);
            return true;
        }
        else
        {
            print("Different sign: prevYThrow = " + prevYThrow + " yThrow = " + yThrow);
            return false; 
        }
    }

    private void Rotate()
    {
        float pitch = ComputatePitch();
        float yaw = ComputateYaw();
        float roll = ComputateRoll();
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);


        /*//print("pitchDueToPosition = " + transform.localPosition.y + " * " + positionPitchFactor + " = " + pitchDueToPosition);
        if (!SameSign(prevYThrow, yThrow))
        {
            print("Different sign: prevYThrow = " + prevYThrow + " yThrow = " + yThrow);
            print("New pitchDueToThrow = " + yThrow + " * " + controlPitchFactor + " = " + pitchDueToThrow);
            yThrow = Mathf.Clamp(Math.Abs(prevYThrow - yThrow), -1, 1);
            //print("Different sign: pitchDueToThrow = " + pitchDueToThrow + " yThrow = " + yThrow);
        }
        else
        {
            //double temp= Math.Pow((1 - Math.Pow(yThrow, 2)),0.5) * controlPitchFactor;
            *//*print("Same sign: pitchDueToThrow = " + pitchDueToThrow + " yThrow = " + yThrow);
            print("Same sign: prevYThrow = " + prevYThrow + " yThrow = " + yThrow);
            print("New pitchDueToThrow = " + -yThrow + " * " + controlPitchFactor + " = " + pitchDueToThrow);*//*
        }*/


        /*
        float rotX = -Input.GetAxis("Vertical") * 30f;
        float rotZ = -Input.GetAxis("Horizontal") * 30f;

        transform.localEulerAngles = new Vector3(rotX, 0, rotZ);*//*
        prevYThrow = yThrow;
        print("prevYThrow = yThrow : " + prevYThrow + " " + yThrow);*/
    }

    private float ComputatePitch()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;

        float pitchDueToThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToThrow;
        return pitch;
    }

    private float ComputateYaw()
    {
        float yawDueToPitch = -transform.localPosition.x * positionYawFactor;
        float yawDueToThrow = xThrow * controlYawFactor*.8f;
        float yaw = yawDueToPitch + yawDueToThrow;
        return yaw;
    }
    
    private float ComputateRoll()
    {
        float roll = xThrow * controlRollFactor;
        return roll;
    }

    private void ReactVerticalInput()
    {
        yThrow = Input.GetAxis("Vertical");
        float yOffsetThisFrame = yThrow * speed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffsetThisFrame;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange+4, yRange);


        transform.localPosition = new Vector3(transform.localPosition.x, clampedYPos, transform.localPosition.z);
    }

    private void ReactHorizontalInput()
    {
        xThrow = Input.GetAxis("Horizontal");
        float xOffsetThisFrame = xThrow * speed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffsetThisFrame;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        transform.localPosition = new Vector3(clampedXPos, transform.localPosition.y, transform.localPosition.z);
    }

}
