using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void StartButtonClicked()
    {
        SoundManager.instance.PlaySound("Butten_1");

        SoundManager.instance.StopSound("MainBGM");
        SoundManager.instance.PlaySound("PizzaBGM");
        SceneManager.LoadScene("Scene_Order");
    }
}
