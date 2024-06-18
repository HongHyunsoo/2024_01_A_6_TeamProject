using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class OrderSystenManager : MonoBehaviour
{

    public static int day; //일차
    public static int dayCount; //일차를 넘어갈 때 얼마만큼의 손님을 받아야 넘어갈 건지

    public static int orderCount;          //현재 일차에서 지금까지 내가 받은 손님의 수  
    public static int orderLevel;          //손님의 주문 난이도
    public static int orderValue;          //손님이 요구하는 피자의 값어치
    public static int customerNumber;      //손님의 번호
    public static int pizzaNumber;         //피자 메뉴 번호

    public float CustomerAppearTime;        // 손님 등장 시간 랜덤

    public static int changePizzaSize = 3;  //5x5가 적용되는 일차
    
    public static float star;     //가게의 평점


    public static bool isCustomerHere; //화면에 손님이 존재하는지
    public static bool isGameOver;   //요리가 끝난 상태인지
    public static bool isNextDay; //일차를 넘어간 후 일차 난이도에 대해 설정을 해야하는 상태인지
    public static bool isReciptPrint;   //영수증을 출력해야하는 상황인지


    [Header("---------------------[ UI ]")]

    public GameObject orderGroup;   //주문을 할 때 필요한 UI그룹
    public GameObject orderButten;  //주문을 받을 때의 버튼
    public GameObject customerGroup;    //손님을 할당하는 그룹
    public GameObject nextOrderButten;  //다음 손님을 받을 때의 버튼

    public GameObject nextDayGroup;     //다음 날로 넘어갈 때 UI



    [Header("---------------------[ Text ]")]

    public Text orderValueText;     //손님이 요구하는 값을 출력하는 텍스트
    public Text orderResultText;    //내가 받은 값을 출력하는 텍스트
    public Text customerNameText;   //손님의 이름 출력
    public Text valueDisplay;       //손님의 대사를 출력하는 텍스트
    public Text pizzaNameText;      //피자의 이름을 출력하는 텍스트

    public Text dayText;            //현재 일차를 출력하는 텍스트
    public Text dayTextTitle;       //다음 일차로 넘어갈 떄 일차를 출력하는 텍스트 
    
    public Text orderCountText;     //지금까지 주문 받은 수와, 목표치 표시
    public Text starText;           //현재 평점을 표시

    public Text orderValueReciptText;     //손님이 요구하는 값을 출력하는 텍스트
    public Text orderResultReciptText;    //내가 받은 값을 출력하는 텍스트
    public Text customerNameReciptText;   //손님의 이름 출력

    [Header("---------------------[ Calculate Text ]")]

    public GameObject DayOverCalculateGroup;
    private Tween fadeTween;

    public Text orderValue_1_Text;
    public Text orderValue_2_Text;
    public Text orderValue_3_Text;
    public Text orderValue_4_Text;
    public Text orderValue_5_Text;

    public Text sumPastEntire_Text;
    public Text sumDay_Text;
    public Text sumEntire_Text; //

    //정산
    public static int orderValue_1;
    public static int orderValue_2;
    public static int orderValue_3;
    public static int orderValue_4;
    public static int orderValue_5;

    public static int sumDayOrderValue;
    public static int sumEntireOrderValue;

    [Header("---------------------[ New Event ]")]

    public GameObject[] NewEventGroup;
    public int NewEventNumber;



    //public Transform customerGroup;    //손님의 프리팹을 할당 할 그룹

    public void Start()
    {
        CustomerAppearTime = Random.Range(0.8f, 2.0f);
        nextDayGroup.SetActive(false);

        //만약 요리가 끝난 상태라면 요리 끝 로직으로
        if (isGameOver == true)
        {
            Debug.Log("게임 오버 투르 됨");
            CookingFinish();

        }
        //만약 요리가 끝난 상태가 아니라면(첫번 째 일차라면) 첫 손님 생성
        else
        {
            day = 1;    //시작 시 일차를 1로 설정
            isCustomerHere = false; // 손님 존재 여부 false
            star = 2.5f; //평점의 초기 값을 2.5로 설정

            LevelSetting();

            Invoke("MakeCustomer", CustomerAppearTime);   //1.5초 뒤에 첫 손님을 생성하기
        }


    }

    private void Update()
    {
        customerNameText.text = customerName[customerNumber] + " 님 주문 감사합니다.".ToString();
        orderValueText.text = "주문 내역: $ " + orderValue.ToString();
        pizzaNameText.text = PizzaName[pizzaNumber];

        customerNameReciptText.text = customerName[customerNumber] + " 님".ToString();
        orderValueReciptText.text = "$" + orderValue.ToString();

        starText.text = "★ :" + star.ToString("N1");    //평점이 몇 점인지 표시
        dayText.text = "Day " + day.ToString(); //몇 일차인지 표시
        orderCountText.text = orderCount + " / " + dayCount.ToString(); //몇 번의 주문을 받아야 하는지 표시
    }

    void StartOrder() //주문 시작
    {
        if (isCustomerHere == true)     //손님이 화면에 있다면
        {
            orderResultText.text = "결과: $ 0".ToString();
            orderResultReciptText.text = "$0".ToString();
            customerNumber = Random.Range(0, 10);
            valueDisplay.text = "아직 없음".ToString();
            
            
            //말풍선에 손님이 요구하는 값 출력하기
        }
    }

    public void NextOrder() //다음 주문
    {
        orderCount++;   //받은 주문 카운트 +1

        SoundManager.instance.PlaySound("Butten_3"); //버튼 소리 재생

        if (orderCount == dayCount && star >= 0.5f)
        {
            NextDay();      //다음 날 로직 실행헤서 조건 충족 시 다음날로
        }
        else if (orderCount < dayCount && star >= 0.5f)
        //내가 지금까지 받은 손님의 수가 다음 일차 조건보다 낮을 경우
        {
            orderButten.SetActive(false);   //주문 버튼 비활성화
            nextOrderButten.SetActive(false);//다음 주문 받기 버튼 비활성화
            orderGroup.SetActive(false);     //주문에 관한 UI그룹 비활성화
            Invoke("MakeCustomer", CustomerAppearTime);    //1.5초 후에 다음 손님 생성
        }
        else if (star <= 0)
        {
            Ending.isBadEnding = true;  //베드 엔딩 조건 True
            SceneManager.LoadScene("Scene_Ending");  //엔딩 씬으로 이동
        }
        
    }

    public void NextDay()   //다음 날
    {
        if (orderCount == dayCount)
        //지금까지 내가 받은 손님의 수가 다음 일차로 넘어가기 위한 손님의 수를 충족한다면
        {
            day++;  //일차에 +1
            dayText.text = "Day " + day.ToString();
            orderCount = 0;

            DayOverCalculate();
            DayOverCalculateGroup.SetActive(true);
            orderButten.SetActive(false);
            nextOrderButten.SetActive(false);
            orderGroup.SetActive(false);
        }
        else
        {
            return;
        }
    }

   

    public void OrderCalculateButten()
    {
        DayOverCalculateGroup.SetActive(false);
        nextDayGroup.SetActive(true);
        customerGroup.SetActive(false);

        dayTextTitle.text = "Day " + day.ToString();

        isNextDay = true;

        NextDayButten();
        Invoke("NewEvent", 5.0f);
    }

    public void NextDayButten() //다음 일차 버튼을 눌렀을 때
    {

        if (day == 14)
        {
            Ending.isBadEnding = false;  //베드 엔딩 조건 false
            SceneManager.LoadScene("Scene_Ending");  //엔딩 씬으로 이동
        }
        SoundManager.instance.PlaySound("Butten_3");


        LevelSetting();
    }

    public void NewEvent()
    {
        if (day == 3)
        {
            SoundManager.instance.PlaySound("NewEvent");
            NewEventNumber = 0;
            NewEventGroup[0].SetActive(true);

            nextDayGroup.SetActive(false);  //다음 일차UI그룹 비활성화
        }
        else if (day == 7)
        {
            SoundManager.instance.PlaySound("NewEvent");
            NewEventNumber = 1;
            NewEventGroup[1].SetActive(true);

            nextDayGroup.SetActive(false);  //다음 일차UI그룹 비활성화
        }
        else if (day == 11)
        {
            SoundManager.instance.PlaySound("NewEvent");
            NewEventNumber = 2;
            NewEventGroup[2].SetActive(true);

            nextDayGroup.SetActive(false);  //다음 일차UI그룹 비활성화
        }
        else
        {

            nextDayGroup.SetActive(false);  //다음 일차UI그룹 비활성화
            Invoke("MakeCustomer", CustomerAppearTime);
        }
        
    }

    public void NewEventButten()
    {
        NewEventGroup[NewEventNumber].SetActive(false);
        SoundManager.instance.PlaySound("Butten_3");
        Invoke("MakeCustomer", CustomerAppearTime);

    }

    public void CookingFinish()     //요리가 끝난 후에 실행
    {
        if (isGameOver == true)  //요리 씬에서 게임 오버 된 것이 True라면
        {

            orderResultText.text = "결과: $ " + GameController2048.myScore.ToString();

            if (GameController2048.myScore < orderValue)
            //만약 플레이어의 점수가 손님이 요구하는 점수보다 낮다면
            {
                OrderFailedArray();
                OrderFailedCalculate();
                star -= 1f;  //평점 1점 감소
                Debug.Log(star);
            }
            else if (GameController2048.myScore >= orderValue)
            //만약 플레이어의 점수가 손님이 요구하는 점수보다 높거나 같다면
            {
                OrderSuccessArray();
                OrderSeccessCalculate();
                if (star == 5)
                {
                    star += 0.0f;     //평점 1점 증가
                    Debug.Log(star);
                }
                else if (star < 5)
                {
                    star += 0.5f;     //평점 1점 증가
                    Debug.Log(star);
                }


            }

        }
        customerGroup.SetActive(true);
        orderGroup.SetActive(true);
        orderButten.SetActive(false);
        nextOrderButten.SetActive(true);

    }

    public void DayOverCalculate()
    {
        sumPastEntire_Text.text = sumEntireOrderValue.ToString();


        orderValue_1_Text.text = orderValue_1.ToString();
        orderValue_2_Text.text = orderValue_2.ToString();
        orderValue_3_Text.text = orderValue_3.ToString();
        orderValue_4_Text.text = orderValue_4.ToString();
        orderValue_5_Text.text = orderValue_5.ToString();

        sumDayOrderValue = orderValue_1 + orderValue_2 + orderValue_3 + orderValue_4 + orderValue_5;    //일일 수익 계산
        sumEntireOrderValue = sumDayOrderValue + sumEntireOrderValue;   //햡계 수익을 일일 수익과 인전 총 수입을 합해서 계산한 수 적용

        sumDay_Text.text = sumDayOrderValue.ToString();
        sumEntire_Text.text = sumEntireOrderValue.ToString();

    }
    public void OrderSeccessCalculate()         //주문 성공 시 정산
    {
        if (orderCount == 0)
        {
            orderValue_1 = GameController2048.myScore;

        }
        else if (orderCount == 1)
        {
            orderValue_2 = GameController2048.myScore;
        }
        else if (orderCount == 2)
        {
            orderValue_3 = GameController2048.myScore;
        }
        else if (orderCount == 3)
        {
            orderValue_4 = GameController2048.myScore;
        }
        else if (orderCount == 4)
        {
            orderValue_5 = GameController2048.myScore;
        }
    }
    public void OrderFailedCalculate()      //주문 실패 시 정산
    {
        if (orderCount == 0)
        {
            orderValue_1 = 0;
            orderValue_1_Text.text = orderValue_1.ToString();

        }
        else if (orderCount == 1)
        {
            orderValue_2 = 0;
            orderValue_2_Text.text = orderValue_2.ToString();
        }
        else if (orderCount == 2)
        {
            orderValue_3 = 0;
            orderValue_3_Text.text = orderValue_3.ToString();
        }
        else if (orderCount == 3)
        {
            orderValue_4 = 0;
            orderValue_4_Text.text = orderValue_4.ToString();
        }
        else if (orderCount == 4)
        {
            orderValue_5 = 0;
            orderValue_5_Text.text = orderValue_5.ToString();
        }
    }

    public void DeleteCustomer()    //현재 손님을 지우고 다음 손님을 받는 로직
    {
        //Destroy(customerPrefab);
        isGameOver = false;     //게임 오버 여부 초기화

        //버튼과 ui 그룹 비활성화
        customerGroup.SetActive(false);
        orderButten.SetActive(false);
        nextOrderButten.SetActive(false);
        orderGroup.SetActive(false);
    }

    void MakeCustomer() //손님 생성 로직
    {
        //게임 오브젝트에서 손님 프리팹을 인스턴스화 해서 생성하기
        //GameObject instantCustomerObj = Instantiate(customerPrefab, customerGroup);
        //인스턴스화 된 손님 오브젝트에 customer스크립트를 할당하기 
        //Customer instantCustomer = instantCustomerObj.GetComponent<Customer>();
        SoundManager.instance.PlaySound("Money");
        PizzaRecipe();      //피자 메뉴 설정

        customerGroup.SetActive(true);
        orderButten.SetActive(true);
        nextOrderButten.SetActive(false);
        isCustomerHere = true;      //손님이 생성되었기 때문에 손님의 존재 여부를 참으로 설정
        orderGroup.SetActive(true);

        ValueSetting();
        StartOrder();
    }

    void PizzaRecipe()      //피자 메뉴 설정
    {
        if (day <= 2)
        {
            pizzaNumber = 0;
        }
        else if (day >= 3 && day <= 6)  //일차가 3보타 크거나 같고 6보다 작거나 같다
        {
            pizzaNumber = Random.Range(0, 2); //0,1번 피자 중 랜덤 생성
        }
        else if (day >= 7 && day <= 14)  //일차가 6보타 크거나 같고 9보다 작거나 같다
        {
            pizzaNumber = Random.Range(0, 3); //0~2번 피자 중 랜덤 생성
        }

    }

    public void PressCookingButten()
    {

        SoundManager.instance.PlaySound("Butten_3");
        orderButten.SetActive(false);
        isReciptPrint = true;
        Invoke("ToCookingScene",6.5f);
    }

    public void ToCookingScene()
    {
        if(day < changePizzaSize)
        {
            SceneManager.LoadScene("Scene_2048");
        }
        else if(day >= changePizzaSize)
        {
            SceneManager.LoadScene("Scene_2048_5x5");
        }
    }

    void OrderFailedArray()
    {
        if (customerNumber == 0)
        {
            valueDisplay.text = orderFailedArray[Random.Range(0, 3)].ToString();
        }
        else if (customerNumber == 1)
        {
            valueDisplay.text = orderFailedArray[Random.Range(3, 6)].ToString();
        }
        else if (customerNumber == 2)
        {
            valueDisplay.text = orderFailedArray[Random.Range(6, 9)].ToString();
        }
        else if (customerNumber == 3)
        {
            valueDisplay.text = orderFailedArray[Random.Range(9, 12)].ToString();
        }
        else if (customerNumber == 4)
        {
            valueDisplay.text =  orderFailedArray[Random.Range(12, 15)].ToString();
        }
        else if (customerNumber == 5)
        {
            valueDisplay.text =  orderFailedArray[Random.Range(15, 18)].ToString();
        }
        else if (customerNumber == 6)
        {
            valueDisplay.text =  orderFailedArray[Random.Range(18, 21)].ToString();
        }
        else if (customerNumber == 7)
        {
            valueDisplay.text = orderFailedArray[Random.Range(21, 24)].ToString();
        }
        else if (customerNumber == 8)
        {
            valueDisplay.text = orderFailedArray[Random.Range(24, 27)].ToString();
        }
        else if (customerNumber == 9)
        {
            valueDisplay.text = orderFailedArray[Random.Range(27, 30)].ToString();
        }
        else if (customerNumber == 10)
        {
            valueDisplay.text = orderFailedArray[Random.Range(30, 33)].ToString();
        }
        //주문 실패했을 때의 후기 출력
    }

    void OrderSuccessArray()
    {
        if(customerNumber == 0)
        {
            valueDisplay.text = orderSuccessArray[Random.Range(0, 3)].ToString();
        }
        else if(customerNumber == 1)
        {
            valueDisplay.text = orderSuccessArray[Random.Range(3, 6)].ToString();
        }
        else if (customerNumber == 2)
        {
            valueDisplay.text = orderSuccessArray[Random.Range(6, 9)].ToString();
        }
        else if (customerNumber == 3)
        {
            valueDisplay.text = orderSuccessArray[Random.Range(9, 12)].ToString();
        }
        else if (customerNumber == 4)
        {
            valueDisplay.text = orderSuccessArray[Random.Range(12, 15)].ToString();
        }
        else if (customerNumber == 5)
        {
            valueDisplay.text = orderSuccessArray[Random.Range(15, 18)].ToString();
        }
        else if (customerNumber == 6)
        {
            valueDisplay.text = orderSuccessArray[Random.Range(18, 21)].ToString();
        }
        else if (customerNumber == 7)
        {
            valueDisplay.text = orderSuccessArray[Random.Range(21, 24)].ToString();
        }
        else if (customerNumber == 8)
        {
            valueDisplay.text = orderSuccessArray[Random.Range(24, 27)].ToString();
        }
        else if (customerNumber == 9)
        {
            valueDisplay.text = orderSuccessArray[Random.Range(27, 30)].ToString();
        }
        else if (customerNumber == 10)
        {
            valueDisplay.text = orderSuccessArray[Random.Range(30, 33)].ToString();
        }
        //주문 실패했을 때의 후기 출력
    }

    public static string[] PizzaName = //피자 이름 배열
        { 
          "Just 피자", //0
          "오징어 먹물 피자",
          "이탈리안 피자"
        };

    public static string[] customerName = //손님 이름 배열
        { 
          "맛있으면 웃는 사람", //0
          "피자 비평가",
          "삶은 달걀이다",
          "햄버거 호소인",
          "Chat GPT",
          "피자에 미친 사람", //5
          "파인애플 피자 혐오 반대 위원회",
          "왕이 넘어지면 킹콩",
          "웃음꽃 피자",
          "분노조절잘함",
          "노란색 토끼", //10
        };

    private string[] orderFailedArray = //주문 실패 시 대사 배열
        { 
          //0번 | 맛있으면 웃는 사람
          ":(",
          ">:[",
          ":/",
          //1번 | 피자 비평가
          "피자에 감동이 없어요.",
          "여기가 이탈리아가 아닌 걸 다행으로 여기세요.",
          "피자에 대한 모욕이라고 받아들이겠습니다.", 
          //2번 | 삶은 달걀이다
          "제가 진짜 피자에 대해서 잘 몰라서 그러는데, 이거 피자 맞죠?",
          "혹시 무슨 생각 하면서 만드신 건가요? 진짜 그냥 궁금해서.",
          "피자가 제가 제시한 값어치만큼의 가치가 없네요.",
          //3번 | 햄버거 호소인
          "그냥 햄버거 먹을걸...",
          "이 돈 주고 피자 먹을 바에 햄버거 먹겠다",
          "덕분에 앞으로는 햄버거만 먹어야겠다는 교훈을 얻었습니다. 감사합니다.",
          //4번 | Chat Gpt
          "제 상상 속 피자보다 한참 못 미치네요. 아마 요리사가 오늘 기분이 안 좋았던 거죠?",
          "이탈리아의 맛이 아니라, 마치 이탈리아 관광 책자를 씹어먹는 기분이나요.",
          "제가 집에서 만든 것보다 더 별로네요. 제가 요리 실력이 형편없는데도 이 정도는 아닌데요!",
          //5번 | 피자에 미친 사람
          "피자 이렇게 만드는 거 아닌데...",
          "이러시면 국제 피자 법률 위반으로 잡혀가실 수도 있습니다. 조심하세요.",
          "식빵에 소스랑 치즈 좀 뿌린다고 다 피자인 건 아닙니다.",
          //6번 | 파인애플 피자 혐오 반대 위원회
          "아무리 파인애플이 만능이라지만 이거는 파인애플을 넣어도 커버를 할 수가 없는데요.",
          "파인애플이 있고 없고를 떠나서 그냥 맛이 없어요.",
          "이거 피자 혐오입니다.",
          //7번 | 왕이 넘어지면 킹콩
          "개가 화를 쉽게 내는 이유는?\n분노 개 이지 폭발.",
          "모내기 기계가 고장나면?\n심기 불편.",
          "형과 동생이 싸울 때 형의 편을 들어주는 사람이 없으면?\n형편없다.",
          //8번 | 웃음꽃 피자
          "기대 이하였습니다.",
          "전체적으로 실망스러웠습니다.",
          "가치가 없는 맛입니다.",
          //9번 | 분노조절잘함
          "이건 피자에 대한 모욕이야!",
          "이딴 걸 내가! 먹으라고?!",
          "사장 나오라 그래!!",
          //번 | 노란색 토끼
          "피자에 깔린 빨간 토마토소스가 붉은 핏자국으로 바뀌는 수가 있습니다;;",
          "피자가 미쳤네요. 아 물론 나쁜 뜻으로요.",
          "준비중",
          
          
    };

    private string[] orderSuccessArray = //주문 성공 시 대사 배열
        { 
          //0번 손님 | 맛있으면 웃는 사람
          ":):):):):):):):):):)",
          ":]:]:]:]:]:]:]:]:]:]",
          ":D:D:D:D:D:D:D:D:D:D",
          //1번 손님 | 피자 비평가
          "피자한테 사랑받고 계시는군요",
          "피자가 네모난 게 신기하네요",
          "피자가 예술이네요. 냉동고에 얼려놓고 평생 보관하겠습니다.",
          //2번 손님 | 삶은 달걀이다
          "다음에 또 올게요!",
          "앞으로 자주 와야겠어요",
          "피자 맛있겠다",
          //3번 손님 | 햄버거 호소인
          "맛있는 피자~ 게살 피자~",
          "햄버거랑 피자랑 고민했는데 피자 고르길 잘한 거 같아요!",
          "햄버거 만큼은 아니지만 피자도 맛있네요!",
          //4번 손님 | Chat Gpt
          "와, 피자가 정말 맛있네요! 도우가 완벽하게 바삭하고 토핑도 신선해요. 이렇게 맛있는 피자는 오랜만에 먹어봐요!",
          "이 피자 정말 최고예요! 친구들이랑 가족에게 꼭 여기 피자가게 추천할게요. 다음에도 꼭 다시 올 거예요.",
          "피자 정말 맛있게 먹었어요. 좋은 피자 만들어주셔서 감사합니다. 덕분에 정말 행복한 식사 시간이었어요!",
          //5번 손님 | 피자에 미친 사람
          "피자조아피자조아피자조아피자조아피자조아피자조아피자조아피자조아",
          "피자ㅏㅏㅏ-파티다ㅏㅏㅏ!!!",
          "오늘 꿈에서도 피자 먹었는데!",
          //6번 손님 | 파인애플 피자 혐오 반대 위원회
          "집 가서 파인애플 통조림이랑 같이 먹어야겠다! 감사합니다~",
          "메뉴에 파인애플 피자가 없어서 아쉬웠는데 이 집은 일반 피자도 맛있네요!",
          "맛있는 피자 덕분에 즐거운 시간을 보냈어요! 파인애플 피자가 추가된다면 더 자주 올 것 같아요.",
          //7번 | 왕이 넘어지면 킹콩
          "학생들이 가장 싫어하는 피자는?\n책피자",
          "가장 인기가 많은 파는?\n파스타",
          "다리미가 좋아하는 음식은?\n피자!",
          //8번| 웃음꽃 피자
          "웃음꽃이 절로 나는 피자구먼~",
          "10점 만점에 10점~",
          "피자 먹고 나니, 기분이 너무 좋은걸!",
          //9번 | 분노조절잘함
          "역시 이게 피자지",
          "이런 맛집을 왜 이제 안거지?",
          "이런 가게는 널리 알려야 해요",
          //번 | 노란색 토끼
          "사장님을 납치해서 피자만 만들게 하고 싶을 정도로 맛있어요!",
          "피자가 미쳤네요. 아 물론 좋은 뜻으로요.",
          "준비중",
          
    };

    void ValueSetting()     //손님이 주문 할 피자의 값어치 세팅
    {
        if (orderLevel == 0)      //만약 orderLevel이 0이면
        {
            orderValue = Random.Range(4, 5);     //100~500사이에서 렌덤으로 주문 할 피자 값어치를 정한다
            GameController2048.moveCount = 2;
            
        }

        else if (orderLevel == 1)
        {
            orderValue = Random.Range(900, 1000);
            GameController2048.moveCount = 110;
        }

        else if (orderLevel == 2)
        {
            orderValue = Random.Range(1300, 1500);
            GameController2048.moveCount = 160;
        }

        else if (orderLevel == 3)
        {
            orderValue = Random.Range(1500, 1700);
            GameController2048.moveCount = 170;
        }

        else if (orderLevel == 4)
        {
            orderValue = Random.Range(1900, 2100);
            GameController2048.moveCount = 190;
        }

        else if (orderLevel == 5)
        {
            orderValue = Random.Range(2200, 2300);
            GameController2048.moveCount = 200;
        }

        else if (orderLevel == 6)
        {
            orderValue = Random.Range(3800, 4000);
            GameController2048.moveCount = 250;
        }

        else if (orderLevel == 7)
        {
            orderValue = Random.Range(5000, 5500);
            GameController2048.moveCount = 260;
        }

        else if (orderLevel == 8)
        {
            orderValue = Random.Range(6200, 6800);
            GameController2048.moveCount = 270;
        }

        else if (orderLevel == 9)
        {
            orderValue = Random.Range(7800, 8000);
            GameController2048.moveCount = 300;
        }

        else if (orderLevel == 10)
        {
            orderValue = Random.Range(9900, 10200);
            GameController2048.moveCount = 350;
        }


    }

    void OrderCount()
    {
        if (star == 0.5 || star == 1)
        {
            dayCount = 1;
        }
        else if (star == 1.5 || star == 2)
        {
            dayCount = 2;
        }
        else if (star == 2.5 || star == 3)
        {
            dayCount = 3;
        }
        else if (star == 3.5 || star == 4)
        {
            dayCount = 4;
        }
        else if (star == 4.5 || star == 5)
        {
            dayCount = 5;
        }
    }

    void LevelSetting()  //일차에 따른 주문 난이도 설정
    {
        if (day == 1)   //1일차 라면
        {
            orderLevel = 0; //주문 난이도를 0으로
            dayCount = 2;
        }
        else if (day == 2 || day == 3)
        {
            orderLevel = 0;
            OrderCount();
            //dayCount = Random.Range(2, 4);
            //다음 일차로 넘어가기 위해 받아야 하는 손님의 수를 2~3 사이에서 랜덤으로 설정

        }
        else if (day == 4 || day == 5)
        {
            orderLevel = 0;
            OrderCount();
        }
        else if (day == 5 || day == 6)
        {
            orderLevel = 0;
            OrderCount();
        }
        else if (day == 7 || day == 8)
        {
            orderLevel = 0;
            OrderCount();
        }
        else if (day == 9)
        {
            orderLevel = 0;
            OrderCount();
        }
        else if (day == 10)
        {
            orderLevel = 0;
            OrderCount();
        }
        else if (day == 11)
        {
            orderLevel = 0;
            OrderCount();
        }
        else if (day == 12)
        {
            orderLevel = 0;
            OrderCount();
        }
        else if (day == 13)
        {
            orderLevel = 0;
            OrderCount();
        }
        else if (day == 14)
        {
            orderLevel = 0;
            OrderCount();
        }
    }

}
