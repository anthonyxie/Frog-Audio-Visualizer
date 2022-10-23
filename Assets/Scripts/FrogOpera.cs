using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogOpera : MonoBehaviour
{
    public GameObject frogFab;
    public GameObject[] frogList = new GameObject[8];
    // Start is called before the first frame update
    void Start()
    {
        float x = -32;
        float y = 0;
        float z = 10;
        float xIncrement = 2;

        for (int i = 0; i < frogList.Length; i++)
        {
            GameObject frog = Instantiate(frogFab);
            //cube.GetComponent<Renderer>().material.SetColor("_Color", new Color(0,0,1));
            //set initial position
            frog.transform.position = new Vector3(x, y, z);
            x += xIncrement;
            frog.name = "bin" + i;
            //set as child of this waveform
            frog.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            frog.transform.parent = this.transform;
            frogList[i] = frog;
        }
        this.transform.position = new Vector3(this.transform.position.x, 0, 30);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
