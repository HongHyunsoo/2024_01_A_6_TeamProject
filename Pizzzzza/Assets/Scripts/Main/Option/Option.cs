using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public GameObject OptionButten;
    public void ClickOptionButten()
    {
        OptionButten.SetActive(true);
    }

    public void ClickBackButten()
    {
        OptionButten.SetActive(false);
    }
}
