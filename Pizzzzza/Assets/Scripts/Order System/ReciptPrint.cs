using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

public class ReciptPrint : MonoBehaviour
{
    public GameObject upperRecipt;

    int randomY_1;
    int randomY_2;
    int randomY_3;
    int randomY_4;

    float randomTime_1;
    float randomTime_2;
    float randomTime_3;

    float randomLateTime_1;
    float randomLateTime_2;
    float randomLateTime_3;

    public void Start()
    {
        
    }
    public void PrintRecipt()
    {
        if (OrderSystenManager.isReciptPrint == true)
        {
            randomY_1 = Random.Range(300, 400);
            randomY_2 = Random.Range(450, 500);
            randomY_3 = Random.Range(560, 680);
            randomY_4 = Random.Range(1000, 1050);

            SoundManager.instance.PlaySound("Receipt Printing");

            transform.DOLocalMoveY(randomY_1, 0.3f).SetDelay(1.5f);
            transform.DOLocalMoveY(randomY_2, 0.1f).SetDelay(2.4f);
            transform.DOLocalMoveY(randomY_3, 0.1f).SetDelay(2.9f);
            transform.DOLocalMoveY(randomY_4, 0.4f).SetDelay(3.7f);

            upperRecipt.transform.DOLocalMoveY(-870, 0.3f).SetDelay(4.7f);

            Invoke("PlaySound", 4.5f);
            
            upperRecipt.transform.DOLocalMoveX(1500, 0.3f).SetDelay(5);
        }
    }

    public void PlaySound()
    {
        SoundManager.instance.PlaySound("PaperRipping");
    }

}
