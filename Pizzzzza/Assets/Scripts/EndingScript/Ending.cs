using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{

    public static bool isBadEnding;
    public GameObject badEndingGroup;
    public GameObject goodEndingGroup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isBadEnding == true)
        {
            badEndingGroup.SetActive(true);
        }
        else if (isBadEnding == false)
        {
            goodEndingGroup.SetActive(true);
        }
    }
}
