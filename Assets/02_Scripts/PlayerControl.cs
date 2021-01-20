using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public Image indexPressure;

    void Start()
    {
        
    }
    void Update(){
        if (OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger))
        {
            float rate = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
            indexPressure.fillAmount = rate;
        }
    }
}
