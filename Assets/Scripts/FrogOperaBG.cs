using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FrogOperaBG : MonoBehaviour
{
    public GameObject frogFab;
    public GameObject[] frogList;
    public int numFrogs;
    public float xIncrement;
    public static bool isActive = false;
    public static bool first = true;
    private float halfFrogs;
    // Start is called before the first frame update
    void Start()
    {
        frogList = new GameObject[numFrogs];
        halfFrogs = (float)numFrogs / 2 - 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && !isActive)
        {
 
            if (first)
            {
                StartCoroutine(Delay());

                first = false;
            }
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
            this.transform.position = new Vector3(this.transform.position.x, -8.0f + i * 0.208f, 90f);
            yield return new WaitForSeconds(.005f);
        }
    }
    IEnumerator Delay()
    {
        float x = -0.5f * (xIncrement * (numFrogs - 1f));
        float y = 0f;
        float z = 4f;
        yield return new WaitForSeconds(25);
        for (int i = 0; i < frogList.Length; i++)
        {
            GameObject frog = Instantiate(frogFab);
            //cube.GetComponent<Renderer>().material.SetColor("_Color", new Color(0,0,1));
            //set initial position
            frog.transform.localPosition = new Vector3(x, y, z - 2f * Math.Abs(i - (numFrogs / 2f - 0.5f)));
            x += xIncrement;
            frog.name = "bin" + i;
            //set as child of this waveform
            frog.transform.localEulerAngles = new Vector3(15, 60f / halfFrogs * (i - halfFrogs), 0);
            frog.transform.localScale = new Vector3(2f, 2f, 2f);
            frog.transform.parent = this.transform;
            frogList[i] = frog;
        }
        StartCoroutine(Appear());

    }
}
