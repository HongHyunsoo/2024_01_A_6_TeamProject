using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OrderSystenManager : MonoBehaviour
{
    public static int day; //일차
    public static int dayCount; //일차를 넘어갈 때 얼마만큼의 손님을 받아야 넘어갈 건지
    public static int orderCount;   //현재 일차에서 지금까지 내가 받은 손님의 수  
    public static int orderLevel; //손님의 주문 난이도
    public static int orderValue;   //손님이 요구하는 피자의 값어치
    public static int star;     //가게의 평점
    public static int maxStar = 10;     //가게의 최대 평점

    public bool isCustomerHere; //화면에 손님이 존재하는지
    public static bool isGameOver;  //요리 씬에서 게임 오버 상태인지

    public GameObject orderGroup;   //주문을 할 때 필요한 UI그룹
    public GameObject orderButten;  //주문을 받을 때의 버튼
    public GameObject nextOrderButten;  //다음 손님을 받을 때의 버튼
    public GameObject nextDayGroup;     //다음 날로 넘어갈 때 UI

    public Text valueDisplay;       //손님의 대사를 출력하는 텍스트
    public Text dayText;            //현재 일차를 출력하는 텍스트
    public Text dayTextTitle;            //현재 일차를 출력하는 텍스트 
    public Text orderCountText;     //지금까지 주문 받은 수와, 목표치 표시


    public GameObject customerPrefab;   //손님의 프리팹
    public Transform customerGroup;    //손님의 프리팹을 할당 할 그룹

    public void Start()
    {

        nextDayGroup.SetActive(false);
        


        //만약 게임 오버 상태라면 요리 끝 로직으로
        if (isGameOver == true)
        {
            Debug.Log("게임 오버 투르 됨");
            LevelSetting();

            CookingFinish();

        }
        //만약 게임 오버 상태가 아니라면 첫 손님 생성
        else
        {
            day = 1;    //시작 시 일차를 1로 설정
            isCustomerHere = false; // 손님 존재 여부 false
            star = 5; //평점의 초기 값을 5로 설정

            LevelSetting();

            Invoke("MakeCustomer", 1.5f);   //1.5초 뒤에 첫 손님을 생성하기
        }


    }

    private void Update()
    {
        dayText.text = "Day " + day.ToString();
        orderCountText.text = orderCount + " / " + dayCount.ToString();
    }


    void ValueSetting()     //손님이 주문 할 피자의 값어치 세팅
    {
        if (orderLevel == 0)      //만약 orderLevel이 0이면
        {
            orderValue = Random.Range(100, 500);     //50~200사이에서 렌덤으로 주문 할 피자 값어치를 정한다
        }

        else if (orderLevel == 1)      
        {
            orderValue = Random.Range(500, 2000);     
        }

        else if (orderLevel == 2)      
        {
            orderValue = Random.Range(2000, 4000);    
        }

        else if (orderLevel == 3)     
        {
            orderValue = Random.Range(4000, 10000);     
        }

        else if (orderLevel == 4)      
        {
            orderValue = Random.Range(10000, 20000);     
        }

        else if (orderLevel == 5)     
        {
            orderValue = Random.Range(20000, 40000);    
        }

        else if (orderLevel == 6)      
        {
            orderValue = Random.Range(40000, 50000);     
        }

        else if (orderLevel == 7)      
        {
            orderValue = Random.Range(50000, 60000);    
        }

        else if (orderLevel == 8)     
        {
            orderValue = Random.Range(13500, 15000);     
        }

        else if (orderLevel == 9)      
        {
            orderValue = Random.Range(14500, 16000);     
        }
        
        else if (orderLevel == 10)
        {
            orderValue = Random.Range(14500, 16000);
        }


    }

    void StartOrder()
    {
        if (isCustomerHere == true)     //손님이 화면에 있다면
        {
            valueDisplay.text = "$ " + orderValue.ToString();   
            //말풍선에 손님이 요구하는 값 출력하기
        }
    }
    void LevelSetting()  //일차에 따른 주문 난이도 설정
    {
        if (day == 1)   //1일차 거나 2일차 라면
        {
            orderLevel = 0; //주문 난이도를 0으로
            dayCount = Random.Range(1, 2);
            //다음 일차로 넘어가기 위해 받아야 하는 손님의 수를 2~3 사이에서 랜덤으로 설정
        }
        else if (day == 2 || day == 3)
        {
            orderLevel = 1;
            dayCount = Random.Range(2, 4);
        }

    }
    
    public void NextOrder() //다음 주문
    {
        orderCount++;   //받은 주문 카운트 +1
        if (orderCount == dayCount)
        {
            NextDay();      //다음 날 로직 실행헤서 조건 충족 시 다음날로
        }
        else if(orderCount < dayCount)  
        //내가 지금까지 받은 손님의 수가 다음 일차 조건보다 낮을 경우
        {
            orderButten.SetActive(false);
            nextOrderButten.SetActive(false);
            orderGroup.SetActive(false);
            Invoke("MakeCustomer", 1.5f);   
        }
        
    }

    public void NextDay()
    {
        if(orderCount == dayCount)  
        //지금까지 내가 받은 손님의 수가 다음 일차로 넘어가기 위한 손님의 수를 충족한다면
        {
            day++;  //일차에 +1
            dayText.text = "Day " + day.ToString();
            orderCount = 0;
            orderButten.SetActive(false);
            nextOrderButten.SetActive(false);
            orderGroup.SetActive(false);

            nextDayGroup.SetActive(true);
            dayTextTitle.text = "Day " + day.ToString();

            Debug.Log(day);
        }
        else
        {
            return;
        }
    }

    public void NextDayButten()
    {
        nextDayGroup.SetActive(false);

        LevelSetting();
        Invoke("MakeCustomer", 1.5f);
    }

    public void CookingFinish()     //요리가 끝난 후에 실행
    {
        if(isGameOver == true)  //요리 씬에서 게임 오버 된 것이 True라면
        {
            if(GameController2048.myScore < orderValue)  
            //만약 플레이어의 점수가 손님이 요구하는 점수보다 낮다면
            {
                valueDisplay.text = ":(".ToString();    //:( 출력
                star -= 2;  //평점 2점 감소
                Debug.Log(star);
            }
            else if(GameController2048.myScore >= orderValue)
            //만약 플레이어의 점수가 손님이 요구하는 점수보다 높거나 같다면
            {
                valueDisplay.text = ":)".ToString();    //:) 출력
                star++;     //평점 1점 증가
                Debug.Log(star);
            }
    
        }
        orderGroup.SetActive(true);
        orderButten.SetActive(false);
        nextOrderButten.SetActive(true);

    }

    public void DeleteCustomer()    //현재 손님을 지우고 다음 손님을 받는 로직
    {
        //Destroy(customerPrefab);
        isGameOver = false;     //게임 오버 여부 초기화

        //버튼과 ui 그룹 비활성화
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
        SceneManager.LoadScene(1);
    }
}
