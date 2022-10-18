using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectrum : MonoBehaviour
{
    public GameObject pfCube;

    public GameObject[] pfCubeList = new GameObject[512];
    // Start is called before the first frame update
    void Start()
    {
        float scale = 1.0f;
        float x = -512;
        float y = 0;
        float z = 0;
        float xIncrement = 2;
        for (int i = 0; i < pfCubeList.Length; i++) {
            GameObject cube = Instantiate(pfCube);
            //cube.GetComponent<Renderer>().material.SetColor("_Color", new Color(0,0,1));
            //set initial position
            cube.transform.position = new Vector3(x, y, z);
            x += xIncrement;
            cube.transform.localScale = new Vector3(2f * scale, 0f * scale , 1f * scale);
            cube.name = "bin" + i;
            //set as child of this waveform
            cube.transform.parent = this.transform;
            pfCubeList[i] = cube;
        }
        this.transform.position = new Vector3(this.transform.position.x, -100, this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float[] spectrum = AudioInput.spectrum;
        
        for (int i = 0; i < pfCubeList.Length; i++) {
            float y = 600 * Mathf.Sqrt(spectrum[i]);
            pfCubeList[i].transform.localScale = new Vector3(pfCubeList[i].transform.localScale.x, y , pfCubeList[i].transform.localScale.z);
            pfCubeList[i].transform.localPosition = new Vector3(pfCubeList[i].transform.localPosition.x, y / 2, pfCubeList[i].transform.localScale.z);
        }
    }
}
