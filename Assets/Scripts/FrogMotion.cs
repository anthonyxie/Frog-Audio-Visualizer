using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class FrogMotion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float[] spectrum = AudioInput.spectrum;
        float height = spectrum.Sum();
        if (this.gameObject.CompareTag("Highs")) {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x,  1.71f + OceanGrid.midvolumeHeight * 1.3f, this.transform.localPosition.z);
        }
        else if (this.gameObject.CompareTag("Mids"))
        {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, 2.2f + OceanGrid.highvolumeHeight * 1f, this.transform.localPosition.z);
        }
        else if (this.gameObject.CompareTag("Lows"))
        {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x,  2.46f + OceanGrid.lowvolumeHeight * 0.8f, this.transform.localPosition.z);
        }

    }
}
