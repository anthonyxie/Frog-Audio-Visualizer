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
    private float[] lowBin = new float[64];
    private float[] midBin = new float[256];
    private float[] highBin = new float[192];
    public static float lower;
    public static float midder;
    public static float higher;
    public static float volume;
    void Update()
    {
        float[] wf = ChunityAudioInput.the_waveform;
        float[] spectrum = ChunityAudioInput.the_spectrum;


        
        for (int i = 0; i < spectrum.Length; i++)
        {
            if (i < 64)
            {
                lowBin[i] = spectrum[i];
            }
            else if (i < 320)
            {
                midBin[i - 64] = spectrum[i];
            }
            else
            {
                highBin[i - 320] = spectrum[i];
            }
        }
        lower = Mathf.Sqrt(lowBin.Sum());
        midder = Mathf.Sqrt(midBin.Sum());
        higher = Mathf.Sqrt(highBin.Sum());

        if (spectrum.Max() - spectrum.Min() > max)
        {
            max = spectrum.Max() - spectrum.Min();
        }
        volume = wf.Max() - wf.Min();
        float x = 1;
        if (this.gameObject.CompareTag("lower"))
        {
            x = -1;
        }
        float scaler = 1f;
        if (this.transform.parent.gameObject.CompareTag("Highs"))
        {
            scaler = Mathf.Sqrt(higher * 6.2f);
        }
        if (this.transform.parent.gameObject.CompareTag("Mids"))
        {
            scaler = Mathf.Sqrt(midder * 3.2f);
        }
        if (this.transform.parent.gameObject.CompareTag("Lows"))
        {
            scaler = Mathf.Sqrt(lower * 1.6f);
        }

        this.transform.localEulerAngles = new Vector3((scaler * x * 25.677f * volume), 0, 0);



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
