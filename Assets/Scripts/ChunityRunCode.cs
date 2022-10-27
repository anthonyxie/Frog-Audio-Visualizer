using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//-----------------------------------------------------------------------------
// name: ChunityRunCode.cs
// desc: run ChucK code from here, either as text or from a .ck file
//-----------------------------------------------------------------------------
public class ChunityRunCode : MonoBehaviour
{
    // the chuck subinstance
    ChuckSubInstance chuck;
    public static bool isActive = false;
    public static bool isFirst = true;

    // Start is called before the first frame update
    void Start()
    {
        // get the chuck subinstance on the object this script is attached to
        chuck = GetComponent<ChuckSubInstance>();
        // run code
    }

    void runMic()
    {   
        // run code -- this is the microphone input from chuck
        chuck.RunFile("runmic.ck", true);
    }

    void runDope()
    {
        // run code -- this constructs a sound loop
        chuck.RunFile("runNarrative.ck", true);
    }

    // Update is called once per frame
    void Update()
    {   
        if (isFirst)
        {
            runMic();
            isFirst = false;
        }
        if (Input.GetKeyDown("space") && !isActive)
        {
            runDope();
            isActive = true;

        }
    }
}
