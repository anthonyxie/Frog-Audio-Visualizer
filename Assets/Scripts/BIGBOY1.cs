using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIGBOY1 : MonoBehaviour
{
    public GameObject frogFab;
    private GameObject froggy;
    public static bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && !isActive)
        {
            froggy = Instantiate(frogFab);
            froggy.transform.position = new Vector3(froggy.transform.position.x, -17.6f, froggy.transform.position.z);
            StartCoroutine(Appear());
            isActive = true;

        }
        else if (Input.GetKeyDown("space") && isActive)
        {
            isActive = false;
        }
    }
    IEnumerator Appear()
    {
        float y = 11f;
        for (int i = 0; i < 45; i++)
        {
            froggy.transform.position = new Vector3(froggy.transform.position.x, y - 0.7f*(44f - i) , froggy.transform.position.z);
            yield return new WaitForSeconds(.01f);
        }
    }
}
