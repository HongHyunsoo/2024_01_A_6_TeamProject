using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class OrderSystenManager : MonoBehaviour
{

    public static int day; //����
    public static int dayCount; //������ �Ѿ �� �󸶸�ŭ�� �մ��� �޾ƾ� �Ѿ ����

    public static int orderCount;          //���� �������� ���ݱ��� ���� ���� �մ��� ��  
    public static int orderLevel;          //�մ��� �ֹ� ���̵�
    public static int orderValue;          //�մ��� �䱸�ϴ� ������ ����ġ
    public static int customerNumber;      //�մ��� ��ȣ
    public static int pizzaNumber;         //���� �޴� ��ȣ

    public float CustomerAppearTime;        // �մ� ���� �ð� ����

    public static int changePizzaSize = 3;  //5x5�� ����Ǵ� ����
    
    public static float star;     //������ ����


    public static bool isCustomerHere; //ȭ�鿡 �մ��� �����ϴ���
    public static bool isGameOver;   //�丮�� ���� ��������
    public static bool isNextDay; //������ �Ѿ �� ���� ���̵��� ���� ������ �ؾ��ϴ� ��������
    public static bool isReciptPrint;   //�������� ����ؾ��ϴ� ��Ȳ����


    [Header("---------------------[ UI ]")]

    public GameObject orderGroup;   //�ֹ��� �� �� �ʿ��� UI�׷�
    public GameObject orderButten;  //�ֹ��� ���� ���� ��ư
    public GameObject customerGroup;    //�մ��� �Ҵ��ϴ� �׷�
    public GameObject nextOrderButten;  //���� �մ��� ���� ���� ��ư

    public GameObject nextDayGroup;     //���� ���� �Ѿ �� UI



    [Header("---------------------[ Text ]")]

    public Text orderValueText;     //�մ��� �䱸�ϴ� ���� ����ϴ� �ؽ�Ʈ
    public Text orderResultText;    //���� ���� ���� ����ϴ� �ؽ�Ʈ
    public Text customerNameText;   //�մ��� �̸� ���
    public Text valueDisplay;       //�մ��� ��縦 ����ϴ� �ؽ�Ʈ
    public Text pizzaNameText;      //������ �̸��� ����ϴ� �ؽ�Ʈ

    public Text dayText;            //���� ������ ����ϴ� �ؽ�Ʈ
    public Text dayTextTitle;       //���� ������ �Ѿ �� ������ ����ϴ� �ؽ�Ʈ 
    
    public Text orderCountText;     //���ݱ��� �ֹ� ���� ����, ��ǥġ ǥ��
    public Text starText;           //���� ������ ǥ��

    public Text orderValueReciptText;     //�մ��� �䱸�ϴ� ���� ����ϴ� �ؽ�Ʈ
    public Text orderResultReciptText;    //���� ���� ���� ����ϴ� �ؽ�Ʈ
    public Text customerNameReciptText;   //�մ��� �̸� ���

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

    //����
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



    //public Transform customerGroup;    //�մ��� �������� �Ҵ� �� �׷�

    public void Start()
    {
        CustomerAppearTime = Random.Range(0.8f, 2.0f);
        nextDayGroup.SetActive(false);

        //���� �丮�� ���� ���¶�� �丮 �� ��������
        if (isGameOver == true)
        {
            Debug.Log("���� ���� ���� ��");
            CookingFinish();

        }
        //���� �丮�� ���� ���°� �ƴ϶��(ù�� ° �������) ù �մ� ����
        else
        {
            day = 1;    //���� �� ������ 1�� ����
            isCustomerHere = false; // �մ� ���� ���� false
            star = 2.5f; //������ �ʱ� ���� 2.5�� ����

            LevelSetting();

            Invoke("MakeCustomer", CustomerAppearTime);   //1.5�� �ڿ� ù �մ��� �����ϱ�
        }


    }

    private void Update()
    {
        customerNameText.text = customerName[customerNumber] + " �� �ֹ� �����մϴ�.".ToString();
        orderValueText.text = "�ֹ� ����: $ " + orderValue.ToString();
        pizzaNameText.text = PizzaName[pizzaNumber];

        customerNameReciptText.text = customerName[customerNumber] + " ��".ToString();
        orderValueReciptText.text = "$" + orderValue.ToString();

        starText.text = "�� :" + star.ToString("N1");    //������ �� ������ ǥ��
        dayText.text = "Day " + day.ToString(); //�� �������� ǥ��
        orderCountText.text = orderCount + " / " + dayCount.ToString(); //�� ���� �ֹ��� �޾ƾ� �ϴ��� ǥ��
    }

    void StartOrder() //�ֹ� ����
    {
        if (isCustomerHere == true)     //�մ��� ȭ�鿡 �ִٸ�
        {
            orderResultText.text = "���: $ 0".ToString();
            orderResultReciptText.text = "$0".ToString();
            customerNumber = Random.Range(0, 10);
            valueDisplay.text = "���� ����".ToString();
            
            
            //��ǳ���� �մ��� �䱸�ϴ� �� ����ϱ�
        }
    }

    public void NextOrder() //���� �ֹ�
    {
        orderCount++;   //���� �ֹ� ī��Ʈ +1

        SoundManager.instance.PlaySound("Butten_3"); //��ư �Ҹ� ���

        if (orderCount == dayCount && star >= 0.5f)
        {
            NextDay();      //���� �� ���� �����켭 ���� ���� �� ��������
        }
        else if (orderCount < dayCount && star >= 0.5f)
        //���� ���ݱ��� ���� �մ��� ���� ���� ���� ���Ǻ��� ���� ���
        {
            orderButten.SetActive(false);   //�ֹ� ��ư ��Ȱ��ȭ
            nextOrderButten.SetActive(false);//���� �ֹ� �ޱ� ��ư ��Ȱ��ȭ
            orderGroup.SetActive(false);     //�ֹ��� ���� UI�׷� ��Ȱ��ȭ
            Invoke("MakeCustomer", CustomerAppearTime);    //1.5�� �Ŀ� ���� �մ� ����
        }
        else if (star <= 0)
        {
            Ending.isBadEnding = true;  //���� ���� ���� True
            SceneManager.LoadScene("Scene_Ending");  //���� ������ �̵�
        }
        
    }

    public void NextDay()   //���� ��
    {
        if (orderCount == dayCount)
        //���ݱ��� ���� ���� �մ��� ���� ���� ������ �Ѿ�� ���� �մ��� ���� �����Ѵٸ�
        {
            day++;  //������ +1
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

    public void NextDayButten() //���� ���� ��ư�� ������ ��
    {

        if (day == 14)
        {
            Ending.isBadEnding = false;  //���� ���� ���� false
            SceneManager.LoadScene("Scene_Ending");  //���� ������ �̵�
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

            nextDayGroup.SetActive(false);  //���� ����UI�׷� ��Ȱ��ȭ
        }
        else if (day == 7)
        {
            SoundManager.instance.PlaySound("NewEvent");
            NewEventNumber = 1;
            NewEventGroup[1].SetActive(true);

            nextDayGroup.SetActive(false);  //���� ����UI�׷� ��Ȱ��ȭ
        }
        else if (day == 11)
        {
            SoundManager.instance.PlaySound("NewEvent");
            NewEventNumber = 2;
            NewEventGroup[2].SetActive(true);

            nextDayGroup.SetActive(false);  //���� ����UI�׷� ��Ȱ��ȭ
        }
        else
        {

            nextDayGroup.SetActive(false);  //���� ����UI�׷� ��Ȱ��ȭ
            Invoke("MakeCustomer", CustomerAppearTime);
        }
        
    }

    public void NewEventButten()
    {
        NewEventGroup[NewEventNumber].SetActive(false);
        SoundManager.instance.PlaySound("Butten_3");
        Invoke("MakeCustomer", CustomerAppearTime);

    }

    public void CookingFinish()     //�丮�� ���� �Ŀ� ����
    {
        if (isGameOver == true)  //�丮 ������ ���� ���� �� ���� True���
        {

            orderResultText.text = "���: $ " + GameController2048.myScore.ToString();

            if (GameController2048.myScore < orderValue)
            //���� �÷��̾��� ������ �մ��� �䱸�ϴ� �������� ���ٸ�
            {
                OrderFailedArray();
                OrderFailedCalculate();
                star -= 1f;  //���� 1�� ����
                Debug.Log(star);
            }
            else if (GameController2048.myScore >= orderValue)
            //���� �÷��̾��� ������ �մ��� �䱸�ϴ� �������� ���ų� ���ٸ�
            {
                OrderSuccessArray();
                OrderSeccessCalculate();
                if (star == 5)
                {
                    star += 0.0f;     //���� 1�� ����
                    Debug.Log(star);
                }
                else if (star < 5)
                {
                    star += 0.5f;     //���� 1�� ����
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

        sumDayOrderValue = orderValue_1 + orderValue_2 + orderValue_3 + orderValue_4 + orderValue_5;    //���� ���� ���
        sumEntireOrderValue = sumDayOrderValue + sumEntireOrderValue;   //�u�� ������ ���� ���Ͱ� ���� �� ������ ���ؼ� ����� �� ����

        sumDay_Text.text = sumDayOrderValue.ToString();
        sumEntire_Text.text = sumEntireOrderValue.ToString();

    }
    public void OrderSeccessCalculate()         //�ֹ� ���� �� ����
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
    public void OrderFailedCalculate()      //�ֹ� ���� �� ����
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

    public void DeleteCustomer()    //���� �մ��� ����� ���� �մ��� �޴� ����
    {
        //Destroy(customerPrefab);
        isGameOver = false;     //���� ���� ���� �ʱ�ȭ

        //��ư�� ui �׷� ��Ȱ��ȭ
        customerGroup.SetActive(false);
        orderButten.SetActive(false);
        nextOrderButten.SetActive(false);
        orderGroup.SetActive(false);
    }

    void MakeCustomer() //�մ� ���� ����
    {
        //���� ������Ʈ���� �մ� �������� �ν��Ͻ�ȭ �ؼ� �����ϱ�
        //GameObject instantCustomerObj = Instantiate(customerPrefab, customerGroup);
        //�ν��Ͻ�ȭ �� �մ� ������Ʈ�� customer��ũ��Ʈ�� �Ҵ��ϱ� 
        //Customer instantCustomer = instantCustomerObj.GetComponent<Customer>();
        SoundManager.instance.PlaySound("Money");
        PizzaRecipe();      //���� �޴� ����

        customerGroup.SetActive(true);
        orderButten.SetActive(true);
        nextOrderButten.SetActive(false);
        isCustomerHere = true;      //�մ��� �����Ǿ��� ������ �մ��� ���� ���θ� ������ ����
        orderGroup.SetActive(true);

        ValueSetting();
        StartOrder();
    }

    void PizzaRecipe()      //���� �޴� ����
    {
        if (day <= 2)
        {
            pizzaNumber = 0;
        }
        else if (day >= 3 && day <= 6)  //������ 3��Ÿ ũ�ų� ���� 6���� �۰ų� ����
        {
            pizzaNumber = Random.Range(0, 2); //0,1�� ���� �� ���� ����
        }
        else if (day >= 7 && day <= 14)  //������ 6��Ÿ ũ�ų� ���� 9���� �۰ų� ����
        {
            pizzaNumber = Random.Range(0, 3); //0~2�� ���� �� ���� ����
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
        //�ֹ� �������� ���� �ı� ���
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
        //�ֹ� �������� ���� �ı� ���
    }

    public static string[] PizzaName = //���� �̸� �迭
        { 
          "Just ����", //0
          "��¡�� �Թ� ����",
          "��Ż���� ����"
        };

    public static string[] customerName = //�մ� �̸� �迭
        { 
          "�������� ���� ���", //0
          "���� ����",
          "���� �ް��̴�",
          "�ܹ��� ȣ����",
          "Chat GPT",
          "���ڿ� ��ģ ���", //5
          "���ξ��� ���� ���� �ݴ� ����ȸ",
          "���� �Ѿ����� ŷ��",
          "������ ����",
          "�г���������",
          "����� �䳢", //10
        };

    private string[] orderFailedArray = //�ֹ� ���� �� ��� �迭
        { 
          //0�� | �������� ���� ���
          ":(",
          ">:[",
          ":/",
          //1�� | ���� ����
          "���ڿ� ������ �����.",
          "���Ⱑ ��Ż���ư� �ƴ� �� �������� ���⼼��.",
          "���ڿ� ���� ����̶�� �޾Ƶ��̰ڽ��ϴ�.", 
          //2�� | ���� �ް��̴�
          "���� ��¥ ���ڿ� ���ؼ� �� ���� �׷��µ�, �̰� ���� ����?",
          "Ȥ�� ���� ���� �ϸ鼭 ����� �ǰ���? ��¥ �׳� �ñ��ؼ�.",
          "���ڰ� ���� ������ ����ġ��ŭ�� ��ġ�� ���׿�.",
          //3�� | �ܹ��� ȣ����
          "�׳� �ܹ��� ������...",
          "�� �� �ְ� ���� ���� �ٿ� �ܹ��� �԰ڴ�",
          "���п� �����δ� �ܹ��Ÿ� �Ծ�߰ڴٴ� ������ ������ϴ�. �����մϴ�.",
          //4�� | Chat Gpt
          "�� ��� �� ���ں��� ���� �� ��ġ�׿�. �Ƹ� �丮�簡 ���� ����� �� ���Ҵ� ����?",
          "��Ż������ ���� �ƴ϶�, ��ġ ��Ż���� ���� å�ڸ� �þ�Դ� ����̳���.",
          "���� ������ ���� �ͺ��� �� ���γ׿�. ���� �丮 �Ƿ��� ������µ��� �� ������ �ƴѵ���!",
          //5�� | ���ڿ� ��ģ ���
          "���� �̷��� ����� �� �ƴѵ�...",
          "�̷��ø� ���� ���� ���� �������� �������� ���� �ֽ��ϴ�. �����ϼ���.",
          "�Ļ��� �ҽ��� ġ�� �� �Ѹ��ٰ� �� ������ �� �ƴմϴ�.",
          //6�� | ���ξ��� ���� ���� �ݴ� ����ȸ
          "�ƹ��� ���ξ����� �����̶����� �̰Ŵ� ���ξ����� �־ Ŀ���� �� ���� ���µ���.",
          "���ξ����� �ְ� ���� ������ �׳� ���� �����.",
          "�̰� ���� �����Դϴ�.",
          //7�� | ���� �Ѿ����� ŷ��
          "���� ȭ�� ���� ���� ������?\n�г� �� ���� ����.",
          "�𳻱� ��谡 ���峪��?\n�ɱ� ����.",
          "���� ������ �ο� �� ���� ���� ����ִ� ����� ������?\n�������.",
          //8�� | ������ ����
          "��� ���Ͽ����ϴ�.",
          "��ü������ �Ǹ����������ϴ�.",
          "��ġ�� ���� ���Դϴ�.",
          //9�� | �г���������
          "�̰� ���ڿ� ���� ����̾�!",
          "�̵� �� ����! �������?!",
          "���� ������ �׷�!!",
          //�� | ����� �䳢
          "���ڿ� �� ���� �丶��ҽ��� ���� ���ڱ����� �ٲ�� ���� �ֽ��ϴ�;;",
          "���ڰ� ���Ƴ׿�. �� ���� ���� �����ο�.",
          "�غ���",
          
          
    };

    private string[] orderSuccessArray = //�ֹ� ���� �� ��� �迭
        { 
          //0�� �մ� | �������� ���� ���
          ":):):):):):):):):):)",
          ":]:]:]:]:]:]:]:]:]:]",
          ":D:D:D:D:D:D:D:D:D:D",
          //1�� �մ� | ���� ����
          "�������� ����ް� ��ô±���",
          "���ڰ� �׸� �� �ű��ϳ׿�",
          "���ڰ� �����̳׿�. �õ��� ������� ��� �����ϰڽ��ϴ�.",
          //2�� �մ� | ���� �ް��̴�
          "������ �� �ðԿ�!",
          "������ ���� �;߰ھ��",
          "���� ���ְڴ�",
          //3�� �մ� | �ܹ��� ȣ����
          "���ִ� ����~ �Ի� ����~",
          "�ܹ��Ŷ� ���ڶ� ����ߴµ� ���� ���� ���� �� ���ƿ�!",
          "�ܹ��� ��ŭ�� �ƴ����� ���ڵ� ���ֳ׿�!",
          //4�� �մ� | Chat Gpt
          "��, ���ڰ� ���� ���ֳ׿�! ���찡 �Ϻ��ϰ� �ٻ��ϰ� ���ε� �ż��ؿ�. �̷��� ���ִ� ���ڴ� �������� �Ծ����!",
          "�� ���� ���� �ְ���! ģ�����̶� �������� �� ���� ���ڰ��� ��õ�ҰԿ�. �������� �� �ٽ� �� �ſ���.",
          "���� ���� ���ְ� �Ծ����. ���� ���� ������ּż� �����մϴ�. ���п� ���� �ູ�� �Ļ� �ð��̾����!",
          //5�� �մ� | ���ڿ� ��ģ ���
          "����������������������������������������������������������������",
          "���ڤ�����-��Ƽ�٤�����!!!",
          "���� �޿����� ���� �Ծ��µ�!",
          //6�� �մ� | ���ξ��� ���� ���� �ݴ� ����ȸ
          "�� ���� ���ξ��� �������̶� ���� �Ծ�߰ڴ�! �����մϴ�~",
          "�޴��� ���ξ��� ���ڰ� ��� �ƽ����µ� �� ���� �Ϲ� ���ڵ� ���ֳ׿�!",
          "���ִ� ���� ���п� ��ſ� �ð��� ���¾��! ���ξ��� ���ڰ� �߰��ȴٸ� �� ���� �� �� ���ƿ�.",
          //7�� | ���� �Ѿ����� ŷ��
          "�л����� ���� �Ⱦ��ϴ� ���ڴ�?\nå����",
          "���� �αⰡ ���� �Ĵ�?\n�Ľ�Ÿ",
          "�ٸ��̰� �����ϴ� ������?\n����!",
          //8��| ������ ����
          "�������� ���� ���� ���ڱ���~",
          "10�� ������ 10��~",
          "���� �԰� ����, ����� �ʹ� ������!",
          //9�� | �г���������
          "���� �̰� ������",
          "�̷� ������ �� ���� �Ȱ���?",
          "�̷� ���Դ� �θ� �˷��� �ؿ�",
          //�� | ����� �䳢
          "������� ��ġ�ؼ� ���ڸ� ����� �ϰ� ���� ������ ���־��!",
          "���ڰ� ���Ƴ׿�. �� ���� ���� �����ο�.",
          "�غ���",
          
    };

    void ValueSetting()     //�մ��� �ֹ� �� ������ ����ġ ����
    {
        if (orderLevel == 0)      //���� orderLevel�� 0�̸�
        {
            orderValue = Random.Range(4, 5);     //100~500���̿��� �������� �ֹ� �� ���� ����ġ�� ���Ѵ�
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

    void LevelSetting()  //������ ���� �ֹ� ���̵� ����
    {
        if (day == 1)   //1���� ���
        {
            orderLevel = 0; //�ֹ� ���̵��� 0����
            dayCount = 2;
        }
        else if (day == 2 || day == 3)
        {
            orderLevel = 0;
            OrderCount();
            //dayCount = Random.Range(2, 4);
            //���� ������ �Ѿ�� ���� �޾ƾ� �ϴ� �մ��� ���� 2~3 ���̿��� �������� ����

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
