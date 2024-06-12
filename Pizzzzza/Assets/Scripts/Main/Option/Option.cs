using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public GameObject OptionButten;
    public void ClickOptionButten()
    {
        SoundManager.instance.PlaySound("Butten_1");
        OptionButten.SetActive(true);
    }

    public void ClickBackButten()
    {
        SoundManager.instance.PlaySound("Butten_1");
        OptionButten.SetActive(false);
    }
}
