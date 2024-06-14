using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ReciptMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(OrderSystenManager.day < OrderSystenManager.changePizzaSize)
        {
            transform.DOLocalMoveX(-720, 1f);
        }
        else if (OrderSystenManager.day >= OrderSystenManager.changePizzaSize)
        {
            transform.DOLocalMoveX(-740, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
