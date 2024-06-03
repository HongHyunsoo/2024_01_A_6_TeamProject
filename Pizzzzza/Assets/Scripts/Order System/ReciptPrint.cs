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
            randomY_4 = Random.Range(800, 850);

            randomTime_1 = Random.Range(0.2f, 0.4f);
            randomTime_2 = Random.Range(0.08f, 0.15f);
            randomTime_3 = Random.Range(0.4f, 0.55f);

            randomLateTime_1 = Random.Range(0.5f, 1f);
            randomLateTime_2 = Random.Range(1.3f, 1.8f);
            randomLateTime_3 = Random.Range(2.8f, 3.3f);

            transform.DOLocalMoveY(randomY_1, randomTime_1);
            transform.DOLocalMoveY(randomY_2, randomTime_2).SetDelay(randomLateTime_1);
            transform.DOLocalMoveY(randomY_3, randomTime_2).SetDelay(randomLateTime_2);
            transform.DOLocalMoveY(randomY_4, randomTime_3).SetDelay(randomLateTime_3);

            upperRecipt.transform.DOMoveY(500, 0.3f).SetDelay(4);
            upperRecipt.transform.DOMoveX(2500, 0.3f).SetDelay(5);
        }
    }
}
