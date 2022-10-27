using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FrogOperaBoys : MonoBehaviour
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
            float x = -0.5f * (xIncrement * (numFrogs - 1f));
            float y = 0f;
            float z = 4f;
            if (first)
            {
                
                for (int i = 0; i < frogList.Length; i++)
                {
                    GameObject frog = Instantiate(frogFab);
                    //cube.GetComponent<Renderer>().material.SetColor("_Color", new Color(0,0,1));
                    //set initial position
                    frog.transform.localPosition = new Vector3(x, y, z - 2f * Math.Abs(i - (numFrogs / 2f - 0.5f)));
                    x += xIncrement;
                    frog.name = "bin" + i;
                    //set as child of this waveform
                    frog.transform.localEulerAngles = new Vector3(15, 20f / halfFrogs * (i - halfFrogs), 0);
                    frog.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    frog.transform.parent = this.transform;
                    frogList[i] = frog;
                }
                StartCoroutine(Appear());
                first = false;
            }
            this.transform.localPosition = new Vector3(-16.8f, -20.0f, -3.99f); ;
            //StartCoroutine(Appear());
            this.transform.localEulerAngles = new Vector3(0f, -54.13f, 0f);
            isActive = true;
        }
    }

    IEnumerator Appear()
    {
        yield return new WaitForSeconds(64);
        float t = 0f;
        Vector3 startPos = this.transform.localPosition;
        Vector3 endPos = new Vector3(startPos.x, 1.09f, -3.99f);
        float timeToMove = 1.5f;
        while (t < 1)
        {
            this.transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            t += Time.deltaTime / timeToMove;
            yield return null;
        }
        t = 0f;
        timeToMove = 2f;
        while (t < 1)
        {
            this.transform.localPosition = Vector3.Lerp(endPos, endPos, t);
            t += Time.deltaTime / timeToMove;
            yield return null;
        }
        t = 0f;
        timeToMove = 1.5f;
        while (t < 1)
        {
            this.transform.localPosition = Vector3.Lerp(endPos, new Vector3(endPos.x, -20f, endPos.z), t);
            t += Time.deltaTime / timeToMove;
            yield return null;
        }
    }

}
