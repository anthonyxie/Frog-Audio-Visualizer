using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FrogOpera : MonoBehaviour
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
        float x = -0.5f * (xIncrement * (numFrogs - 1f));
        float y = 0f;
        float z = 4f;
        for (int i = 0; i < frogList.Length; i++)
        {
            GameObject frog = Instantiate(frogFab);
            //cube.GetComponent<Renderer>().material.SetColor("_Color", new Color(0,0,1));
            //set initial position
            frog.transform.localPosition = new Vector3(x, y, z - 2f * Math.Abs(i - (numFrogs / 2f - 0.5f)));
            x += xIncrement;
            frog.name = "bin" + i;
            //set as child of this waveform
            frog.transform.localEulerAngles = new Vector3(0, 40f / halfFrogs * (i - halfFrogs), 0);
            frog.transform.localScale = new Vector3(2f, 2f, 2f);
            frog.transform.parent = this.transform;
            frogList[i] = frog;
        }
        this.transform.localPosition = new Vector3(0f, 1.2f, 25f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && !isActive)
        {

            if (first)
            {
                StartCoroutine(Delay());
            }
            isActive = true;

        }
        else if (Input.GetKeyDown("space") && isActive)
        {
            this.transform.localPosition = new Vector3(this.transform.position.x, -8.0f, 25f);
            isActive = false;
        }
    }

    IEnumerator Appear()
    {
        for (int i = 0; i <= 45; i++)
        {
            this.transform.position = new Vector3(this.transform.position.x, -8.0f + i * 0.208f, 25f);
            yield return new WaitForSeconds(.005f);
        }
    }

    IEnumerator Delay()
    {
        float t = 0f;
        Vector3 startPos = this.transform.position;
        Vector3 endPos = new Vector3(this.transform.position.x, -8.0f, 25f);
        float timeToMove = 1.5f;
        while (t < 1)
        {
            this.transform.position = Vector3.Lerp(startPos, endPos, t);
            t += Time.deltaTime / timeToMove;
            yield return null;
        }
        
        yield return new WaitForSeconds(23.5f);
        first = false;
        StartCoroutine(Appear());
    }
}
