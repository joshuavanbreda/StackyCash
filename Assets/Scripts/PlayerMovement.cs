using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SWS;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float force = 100;
    [Space]
    public splineMove splineMove;
    public PlatformObjScript currentPlatform;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void MoveLeft()
    {
        rb.AddForce(new Vector3(-1, 0, 0) * force);
    }

    public void MoveRight()
    {
        rb.AddForce(new Vector3(1, 0, 0) * force);
    }

    public void MoveForward()
    {
        rb.AddForce(new Vector3(0, 0, 1) * force);

    }

    public void MoveBackward()
    {
        rb.AddForce(new Vector3(0, 0, -1) * force);

    }

    public void SetCurrentPlatform(PlatformObjScript platform)
    {
        currentPlatform = platform;
    }


    public void ResetRotation()
    {
        transform.rotation = new Quaternion();
    }


    public void SetSpline(PathManager spline)
    {
        splineMove.pathContainer = spline;
        splineMove.events[splineMove.events.Count - 1].AddListener(delegate { ResetRotation();});
    }

    public void SetTimer()
    { 

    }

    public void RemoveForce()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
