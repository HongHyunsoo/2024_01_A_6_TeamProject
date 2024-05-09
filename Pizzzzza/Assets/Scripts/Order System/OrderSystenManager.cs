using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OrderSystenManager : MonoBehaviour
{
    public static int day; //일차
    public int orderLevel; //손님의 주문 난이도
    public static int orderValue;   //피자의 값
    public bool isCustomerHere; //화면에 손님이 존재하는지
    public static bool isGameOver;

    public GameObject orderGroup;   //주문을 할 때 필요한 UI그룹
    public GameObject orderButten;
    public GameObject nextOrderButten;
    public Text valueDisplay;       //손님의 대사를 출력하는 텍스트


    public GameObject customerPrefab;   //손님의 프리팹
    public Transform customerGroup;    //손님의 프리팹을 할당 할 그룹

    void Start()
    {
        isCustomerHere = false; // 손님 존재 여부 false
        day = 1;    //시작 시 일차를 1로 설정

        //만약 게임 오버 상태라면 요리 끝 로직으로
        if (isGameOver == true)
        {
            Debug.Log("게임 오버 투르 됨");
            CookingFinish();
        }
        //만약 게임 오버 상태가 아니라면 첫 손님 생성
        else
        {
            Invoke("MakeCustomer", 1.5f);   //1.5초 뒤에 첫 손님을 생성하기
        }

    }


    void ValueSetting()     //손님이 주문 할 피자의 값어치 세팅
    {
        if (orderLevel == 0)      //만약 orderLevel이 0이면
        {
            orderValue = Random.Range(10000, 20000);     //50~200사이에서 렌덤으로 주문 할 피자 값어치를 정한다
        }

        if (orderLevel == 1)      //만약 orderLevel이 0이면
        {
            orderValue = Random.Range(200, 400);     //200~400사이에서 렌덤으로 주문 할 피자 값어치를 정한다
        }

    }

    void StartOrder()
    {
        if (isCustomerHere == true)     //손님이 화면에 있다면
        {
            valueDisplay.text = "$ " + orderValue.ToString();   //말풍선에 값 출력하기
        }
    }
    void LevelSetting()
    {
        if (day == 1)
        {
            orderLevel = 0;
        }
    }
    public void NextCustomer() //다음 손님 생성
    {
        
         Invoke("MakeCustomer", 1.5f);    //1.5초 뒤에 첫 손님을 생성하기
        
        
    }

    public void CookingFinish()
    {
        if(isGameOver == true)
        {
            if(GameController2048.myScore < orderValue)
            {
                valueDisplay.text = ":(".ToString();
            }
            else if(GameController2048.myScore >= orderValue)
            {
                valueDisplay.text = ":)".ToString();
            }
    
        }
        orderGroup.SetActive(true);
        orderButten.SetActive(false);
        nextOrderButten.SetActive(true);

    }

    public void DeleteCustomer()
    {
        //Destroy(customerPrefab);
        isGameOver = false;
        orderButten.SetActive(false);
        nextOrderButten.SetActive(false);
        orderGroup.SetActive(false);
    }

    void MakeCustomer() //손님 생성 로직
    {
        //게임 오브젝트에서 손님 프리팹을 인스턴스화 해서 생성하기
        GameObject instantCustomerObj = Instantiate(customerPrefab, customerGroup); 
        //인스턴스화 된 손님 오브젝트에 customer스크립트를 할당하기 
        Customer instantCustomer = instantCustomerObj.GetComponent<Customer>();

        orderButten.SetActive(true);
        nextOrderButten.SetActive(false);
        isCustomerHere = true;      //손님이 생성되었기 때문에 손님의 존재 여부를 참으로 설정
        orderGroup.SetActive(true);
        
        ValueSetting();
        StartOrder();
    }

    
    public void ToCookginScene()
    {
        SceneManager.LoadScene(0);
    }
}
