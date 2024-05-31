using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PizzaImage : MonoBehaviour
{
    Animator pizzaBase_Animator;
    // Start is called before the first frame update
    void Start()
    {
        
        pizzaBase_Animator = GetComponent<Animator>();
        pizzaBase_Animator.SetInteger("PizzaNumber", OrderSystenManager.pizzaNumber);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
