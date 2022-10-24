using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInput : MonoBehaviour
{
    public AudioSource theAudioSource;
    //array that represents the waveform - time domain waveform
    //1024 is # of digital samples
    public static float[] waveform = new float[1024];
    public static float[] spectrum = new float[512];
    public bool useMicrophone = true;
    public AudioClip audioClip;



    // Start is called before the first frame update
    void Start()
    {
        theAudioSource = GetComponent<AudioSource>();
        if (useMicrophone)
        {
            if (Microphone.devices.Length > 0)
            {
                //get device name
                string selectedDevice = Microphone.devices[0].ToString();
                theAudioSource.loop = true;

                // set microphone as an audio clip
                theAudioSource.clip = Microphone.Start(selectedDevice, true, 1, AudioSettings.outputSampleRate);



                //latency reduction, uhhhhhhhhhhh what is this doing
                while (!(Microphone.GetPosition(selectedDevice) > 0)) { }

                
            }
        }
        else
        {
            theAudioSource.clip = audioClip;
        }
        theAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
       //get time domain waveform
       theAudioSource.GetOutputData(waveform, 0);
       theAudioSource.GetSpectrumData(spectrum, 0, FFTWindow.Hanning);
    }
}
