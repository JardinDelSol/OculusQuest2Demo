using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{

    private RaycastHit hit;
    [Range(3.0f, 20.0f)]
    public float maxDistance = 10.0f;
    private new LineRenderer renderer;
    public Transform laserMarker;

    public Transform playerTr;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            renderer.SetPosition(1, new Vector3(0,0,hit.distance));

            laserMarker.position = hit.point;
            laserMarker.rotation = Quaternion.LookRotation(hit.normal);
            
            if(OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                playerTr.position = hit.point;
            }
        }
        else
        {
            renderer.SetPosition(1, new Vector3(0,0,maxDistance));
            laserMarker.position = transform.position + (transform.forward * maxDistance);
            laserMarker.rotation = Quaternion.LookRotation(transform.forward);
        }

        if(OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            
            StartCoroutine(Teleport());
        }
    }
    IEnumerator Teleport()
    {
    OVRScreenFade.instance.fadeTime = 0.0f;
    OVRScreenFade.instance.FadeOut();
    playerTr.position = laserMarker.position;
    yield return new WaitForSeconds(0.1f);
    OVRScreenFade.instance.fadeTime = 0.3f;
    OVRScreenFade.instance.FadeIn();
    }

}

