using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformObjScript : MonoBehaviour
{
    public GateObjScript[] gateObjArray;


    public void CheckGates()
    {
        for (int i = 0; i < gateObjArray.Length; i++)
        {
            gateObjArray[i].CheckPlayer();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
