using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{

    public void ExitGame()
    {
        //���� ����
        SoundManager.instance.PlaySound("Butten_1");
        Application.Quit();
    }
}
