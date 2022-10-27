using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveCaller : MonoBehaviour
{
    public bool isActive = false;
    public Camera cam;
    private Vector3 origPos;
    private Vector3 origRot;
    private float origFOV;
    // Start is called before the first frame update
    void Start()
    {
        origPos = new Vector3(-0.76f, 7.36f, -15.37f);
        origRot = new Vector3(10.7f, 0f, 0f);
        origFOV = cam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && !isActive)
        {
            StartCoroutine(narrativeCameraMovement());
            isActive = true;
        }
    }

    IEnumerator narrativeCameraMovement()
    {
        cam.GetComponent<UnityTemplateProjects.SimpleCameraController>().enabled = false;
        Vector3 oldPos = cam.transform.position;
        Vector3 oldRot = cam.transform.localEulerAngles;
        cam.fieldOfView = origFOV;

        float t = 0f;
        float timeToMove = 0.5f;
        while (t < 1)
        {
            cam.transform.position = Vector3.Lerp(oldPos, origPos, t);
            cam.transform.localEulerAngles = Vector3.Lerp(oldRot, origRot, t);
            t += Time.deltaTime / timeToMove;
            yield return null;
        }
        t = 0f;
        Vector3 startRot = cam.transform.localEulerAngles;
        Vector3 endRot = new Vector3(3.7f, 0f, 0f);
        timeToMove = 3.5f;
        while (t < 1)
        {
            cam.transform.localEulerAngles = Vector3.Lerp(startRot, endRot, t);
            t += Time.deltaTime / timeToMove;
            yield return null;
        }

        t = 0f;
        Vector3 startPos = cam.transform.position;
        Vector3 returnPos = cam.transform.position;
        Vector3 endPos = new Vector3(cam.transform.position.x, cam.transform.position.y, 290);
        timeToMove = 25f;
        while (t < 1)
        {
            cam.transform.position = Vector3.Lerp(startPos, endPos, t);
            cam.transform.localEulerAngles = endRot;
            t += Time.deltaTime / timeToMove;
            yield return null;
        }

        t = 0f;
        timeToMove = 2f;
        while (t < 1)
        {
            cam.transform.position = Vector3.Lerp(endPos, returnPos, t);
            t += Time.deltaTime / timeToMove;
            yield return null;
        }
        t = 0f;
        timeToMove = 1f;
        endPos = new Vector3(-0.76f, 7.36f, -30.68f);
        startRot = new Vector3(2.6f, 0f, 0f);
        while (t < 1)
        {
            cam.transform.position = Vector3.Lerp(returnPos, endPos, t);
            cam.transform.localEulerAngles = Vector3.Lerp(endRot, startRot, t);
            t += Time.deltaTime / timeToMove;
            yield return null;
        }
        yield return new WaitForSeconds(78);
        t = 0;
        timeToMove = 5f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            cam.transform.localEulerAngles = Vector3.Lerp(startRot, new Vector3(-54f, 0f, 0f), t);
            yield return null;
        }
        cam.GetComponent<UnityTemplateProjects.SimpleCameraController>().enabled = true;
    }
}
