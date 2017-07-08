﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class FreePlayMode : MonoBehaviour {

    private Camera _camera;

    [SerializeField]
    private GameObject[] _cars;
    [SerializeField]
    private int _curCar = 0;

    [SerializeField]
    private Canvas inGameUI;
    [SerializeField]
    private Canvas pauseUI;
    [SerializeField]
    private RectTransform speedo;

    void Start () {
        _camera = Camera.main;

        foreach (GameObject c in _cars)
            c.GetComponent<WheelVehicle>().handbreak = true;

        _camera.GetComponent<SmoothFollow>().Target = _cars[_curCar].transform;
        _cars[_curCar].GetComponent<WheelVehicle>().handbreak = false;

    }

    void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            Pause();
        }
        
        float angle = Mathf.Lerp(speedo.rotation.ToEuler().z, -(_cars[_curCar].GetComponent<WheelVehicle>().speed) / 220 * 180, 0.53f);

        

        speedo.rotation = Quaternion.AngleAxis(4 * angle, Vector3.forward);
    }

    public void SelectCar(Int32 c)
    {
        _cars[_curCar].GetComponent<WheelVehicle>().handbreak = true;
        _cars[_curCar].SetActive(false);

        _curCar = c;

        _camera.GetComponent<SmoothFollow>().Target = _cars[_curCar].transform;
        _cars[_curCar].GetComponent<WheelVehicle>().handbreak = false;
        _cars[_curCar].transform.position = Vector3.up;
        _cars[_curCar].transform.rotation = Quaternion.identity;
        _cars[_curCar].SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        
        pauseUI.gameObject.SetActive(true);
        inGameUI.gameObject.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1;

        pauseUI.gameObject.SetActive(false);
        inGameUI.gameObject.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
