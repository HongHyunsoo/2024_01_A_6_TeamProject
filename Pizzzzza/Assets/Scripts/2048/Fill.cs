using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fill : MonoBehaviour
{

    public int value;
    public int level;
    
    Animator Topping_animater;
    [SerializeField] float speed; //합쳐지는 속도 

    bool hasCombine;

    void OnEnable()
    {
        //Topping_animater.SetInteger("Value", level);
    }

    private void OnDisable()
    {
        //동글 속성 초기화
        
      
    }
    public void FillValueUpdate(int valueIn)
    {
        value = valueIn;
    }

    private void Start()
    {
        GetComponent<Fill>();
        Topping_animater = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (transform.localPosition != Vector3.zero)
        {
            hasCombine = false;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, speed * Time.deltaTime);
        }
        else if(hasCombine == false)
        {
            if(transform.parent.GetChild(0) != this.transform)
            {
                Destroy(transform.parent.GetChild(0).gameObject);
            }
            hasCombine = true;
        }
    }

    public void Double()
    {
        
        value *= 2;
        //Topping_animater.SetInteger("Value", level = value);

        //Topping_animater.SetInteger("Value", value);
    }
}
