using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDay : MonoBehaviour
{

    public Animation nextDayAnimation;

    public void PressNextDayButten()
    {
        nextDayAnimation = GetComponent<Animation>();

        nextDayAnimation.GetComponent<Animation>().Play();
    }

}
