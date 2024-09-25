using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public CinemachineVirtualCamera VirtualCamera;

    public float shakeDur;
    private float startTime;
    public float shakeValue;

    // Start is called before the first frame update
    void Start()
    {
        VirtualCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            startTime = Time.time;
            StartCoroutine(shake());
        }
    }

    public void ShakeCamera(int shakeValue)
    {
        this.shakeValue = shakeValue;

        StartCoroutine(shake());
    }

    private IEnumerator shake()
    {
        startTime = Time.time;
        VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = shakeValue;
        while (Time.time <= shakeDur + startTime)
        {
            yield return null;
        }
        VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }
}
