using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tabtale.TTPlugins;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        TTPCore.Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
