using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public OVRInput.Controller Lctrl = OVRInput.Controller.LTouch;
    public OVRInput.Controller Rctrl = OVRInput.Controller.RTouch;

    private CharacterController cc;
    // Start is called before the first frame update
    [SerializeField]
    public float moveSpeed = 1.5f;
    [SerializeField]
    public float turnSpeed = 1.0f;

    private Vector2 pos;
    private Vector2 turn;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        1. Combine method
            PrimaryIndexTrigger - left tirgger
            SecondaryIndexTrigger - right trigger

        2. Individual method
            PrimaryIndexTrigger, LTouch
            PrimaryIndexTrigger, RTouch
        */

        // Combine Method
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            Debug.Log("Left Index Trigger");
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            Debug.Log("Right Index Trigger");
            float value = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
            Debug.Log($"right hand value = {value}");
        }
        
        // Individual Method
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, Lctrl))
        {
            Debug.Log("Left Hand Trigger");
            // OVRInput.SetControllerVibration(1.0f, 1.0f, Lctrl);
            // StartCoroutine(Haptic(0.3f));
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, Rctrl))
        {
            Debug.Log("Right Hand Trigger");
            // OVRInput.SetControllerVibration(1.0f, 1.0f, Rctrl);

        }
        
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            // StartCoroutine(Haptic(0.3f));
        }

        if (OVRInput.Get(OVRInput.Touch.PrimaryThumbstick))
        {
            pos = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            Debug.Log($"Pos = {pos.x}, {pos.y}");

        }

        

        if (OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger))
        {
            Debug.Log("Right Index Trigger Toched");
        }

        IEnumerator Haptic(float duration)
        {
            OVRInput.SetControllerVibration(0.8f, 0.8f, Rctrl);

            yield return new WaitForSeconds(duration);

            OVRInput.SetControllerVibration(0,0,Rctrl);
        }

        //Head Rotation
        if(OVRInput.Get(OVRInput.Touch.SecondaryThumbstick))
        {
            turn = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        }

        Vector3 moveDir =  new Vector3(pos.x, transform.position.y, pos.y);
        float speedRate = pos.magnitude; //magnitude of 2D vector
        cc.SimpleMove(moveDir.normalized * speedRate * moveSpeed);
        Vector3 rotation = new Vector3(-turn.y * turnSpeed, turn.x * turnSpeed, 0);
        cc.transform.Rotate(rotation);
    }
}
