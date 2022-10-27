using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FrogOperaHall : MonoBehaviour
{
    public GameObject frogFab;
    public GameObject[] frogList;
    public int numFrogs;
    public static bool isActive = false;
    public static bool first = true;
    private float halfFrogs;
    // Start is called before the first frame update
    void Start()
    {
        frogList = new GameObject[numFrogs];
        halfFrogs = (float)numFrogs / 2 ;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && !isActive)
        {
            float x = 8f;
            float y = 0f;
            float z = 0f;
            if (first)
            {
                for (int i = 0; i < frogList.Length; i++)
                {
                    GameObject frog = Instantiate(frogFab);
                    //cube.GetComponent<Renderer>().material.SetColor("_Color", new Color(0,0,1));
                    //set initial position

                    frog.name = "bin" + i;
                    //set as child of this waveform
                    
                    if (i >= halfFrogs) {
                        frog.transform.localEulerAngles = new Vector3(0, 40, 0);
                        frog.transform.localPosition = new Vector3(x, y, z + 15f * (i - halfFrogs));
                    }
                    else
                    {
                        frog.transform.localEulerAngles = new Vector3(0, -40, 0);
                        frog.transform.localPosition = new Vector3(-x, y, z + 15f * i);
                    }
                    frog.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
                    frog.transform.parent = this.transform;
                    frogList[i] = frog;
                }
                first = false;
            }
            this.transform.localPosition = new Vector3(this.transform.position.x, 0.0f, 25f);
            StartCoroutine(Appear());
            isActive = true;

        }
        else if (Input.GetKeyDown("space") && isActive)
        {
            this.transform.localPosition = new Vector3(this.transform.position.x, -8.0f, 90f);
            isActive = false;
        }
    }

    IEnumerator Appear()
    {
        for (int i = 0; i <= 45; i++)
        {
            this.transform.position = new Vector3(2.4f, i * 0.203f, 98.8f);
            yield return new WaitForSeconds(.02f);
        }
    }
}
