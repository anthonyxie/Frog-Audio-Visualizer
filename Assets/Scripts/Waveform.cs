using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waveform : MonoBehaviour
{
    //prefab ref
    public GameObject pfCube;
    //array of game objects
    public GameObject[] pfCubeList = new GameObject[1024];

    // Start is called before the first frame update
    void Start()
    {
        float x = -512;
        float y = 0;
        float z = 0;
        float xIncrement = pfCube.transform.localScale.x;
        for (int i = 0; i < pfCubeList.Length; i++) {
            GameObject cube = Instantiate(pfCube);
            //setting color material
            //cube.GetComponent<Renderer>().material.SetColor("_Color", new Color(0,1,0));
            //set initial position
            cube.transform.position = new Vector3(x, y, z);
            x += xIncrement;
            cube.name = "cube" + i;
            //set as child of this waveform
            cube.transform.parent = this.transform;
            pfCubeList[i] = cube;
        }
        this.transform.position = new Vector3(this.transform.position.x, 100, this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float[] wf = AudioInput.waveform;

        for (int i = 0; i < pfCubeList.Length; i++) {
            pfCubeList[i].transform.localPosition = new Vector3(pfCubeList[i].transform.localPosition.x, 100 * wf[i], pfCubeList[i].transform.localPosition.z);
        }
    }
}
