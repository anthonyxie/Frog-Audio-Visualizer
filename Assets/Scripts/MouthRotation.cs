using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MouthRotation : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private float max = 0;
    // Update is called once per frame
    void Update()
    {
        float[] wf = AudioInput.waveform;
        float[] spectrum = AudioInput.spectrum;
        if (spectrum.Max() - spectrum.Min() > max)
        {
            max = spectrum.Max() - spectrum.Min();
            Debug.Log(max);
        }
        float volume = wf.Max() - wf.Min();
        float x = 1;
        if (volume < 0.07)
        {
            volume = 0;
        }
        if (this.gameObject.CompareTag("lower"))
        {
            x = -1;
        }
        float scaler = 1f;
        if (this.transform.parent.gameObject.CompareTag("Highs"))
        {
            scaler = 1f;
        }
        if (this.transform.parent.gameObject.CompareTag("Mids"))
        {
            scaler = 1f;
        }
        if (this.transform.parent.gameObject.CompareTag("Lows"))
        {
            scaler = 1f;
        }

        this.transform.localEulerAngles = new Vector3(scaler * x * 25.677f * volume, 0, 0);



        /**
        if (this.gameObject.CompareTag("lower")) {
            x = -1;
        }
        float currentRotation = this.transform.eulerAngles.x * x;
        Debug.Log(x);
        Debug.Log(currentRotation);
        if ((currentRotation <= 40) && (currentRotation >= 0)) {
            rotateAmount = new Vector3(2f, 0, 0);
            this.transform.Rotate(rotateAmount * Time.deltaTime);
        }
        else {
            rotateAmount = new Vector3(-2f, 0, 0);
            transform.Rotate(rotateAmount * Time.deltaTime);
        }
        */

    }
}
