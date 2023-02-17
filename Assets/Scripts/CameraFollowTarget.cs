using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    public Transform target;
    public Vector3 offsets;
    public float dampening;
    void Update()
    {
        if(target!=null)
        {
            Vector3 targetPos = new Vector3(target.position.x + offsets.x, target.position.y + offsets.y, target.position.z + offsets.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * dampening);
        }
    }
}
