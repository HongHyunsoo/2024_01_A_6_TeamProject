using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay_Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.StopSound("PizzaBGM");
        SoundManager.instance.PlaySound("MainBGM");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
