using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DashCameraShake : MonoBehaviour
{

    CinemachineFreeLook cam;
    CinemachineBasicMultiChannelPerlin shakePerlinRig0, shakePerlinRig1, shakePerlinRig2;
    public float shakeTimer = 0.0f;
    public float shakeMagnitude = 2f;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineFreeLook>();
        shakePerlinRig0 = cam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        shakePerlinRig1 = cam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        shakePerlinRig2 = cam.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        shakePerlinRig0.m_AmplitudeGain = 0;
        shakePerlinRig1.m_AmplitudeGain = 0;
        shakePerlinRig2.m_AmplitudeGain = 0;
        shakePerlinRig0.m_FrequencyGain = 0;
        shakePerlinRig1.m_FrequencyGain = 0;
        shakePerlinRig2.m_FrequencyGain = 0;

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift))
        {
            shakeTimer += Time.deltaTime;
            if (shakeTimer < 0.5f)
            {
                shakePerlinRig0.m_AmplitudeGain = shakeMagnitude;
                shakePerlinRig1.m_AmplitudeGain = shakeMagnitude;
                shakePerlinRig2.m_AmplitudeGain = shakeMagnitude;
                shakePerlinRig0.m_FrequencyGain = 1;
                shakePerlinRig1.m_FrequencyGain = 1;
                shakePerlinRig2.m_FrequencyGain = 1;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            shakeTimer = 0.0f;
        }
    }
}
