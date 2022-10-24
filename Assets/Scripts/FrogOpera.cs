using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FrogOpera : MonoBehaviour
{
    public GameObject frogFab;
    public GameObject[] frogList = new GameObject[9];
    public bool isActive = false;
    public bool first = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && !isActive)
        {
            float x = -32f;
            float y = 0f;
            float z = 4f;
            float xIncrement = 8f;
            if (first)
            {
                for (int i = 0; i < frogList.Length; i++)
                {
                    GameObject frog = Instantiate(frogFab);
                    //cube.GetComponent<Renderer>().material.SetColor("_Color", new Color(0,0,1));
                    //set initial position
                    frog.transform.localPosition = new Vector3(x, y, z - 2f * Math.Abs(i - 4));
                    x += xIncrement;
                    frog.name = "bin" + i;
                    //set as child of this waveform
                    frog.transform.localScale = new Vector3(2f, 2f, 2f);
                    frog.transform.parent = this.transform;
                    frogList[i] = frog;
                }
                first = false;
            }
            this.transform.position = new Vector3(this.transform.position.x, 0.0f, 25f);
            StartCoroutine(Appear());
            isActive = true;

        }
        else if (Input.GetKeyDown("space") && isActive)
        {
            this.transform.position = new Vector3(this.transform.position.x, 0.0f, 25f);
            isActive = false;
        }
    }

    IEnumerator Appear()
    {
        for (int i = 0; i <= 45; i++)
        {
            this.transform.position = new Vector3(this.transform.position.x, i * 0.208f, 25f);
            yield return new WaitForSeconds(.005f);
        }
    }
}
