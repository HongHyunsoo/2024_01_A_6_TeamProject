using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{

    public void ExitGame()
    {
        //게임 종료
        SoundManager.instance.PlaySound("Butten_1");
        Application.Quit();
    }
}
