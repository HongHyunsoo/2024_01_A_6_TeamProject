using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OrderSystenManager : MonoBehaviour
{

    public static int day; //����
    public static int dayCount; //������ �Ѿ �� �󸶸�ŭ�� �մ��� �޾ƾ� �Ѿ ����
    public static int orderCount;   //���� �������� ���ݱ��� ���� ���� �մ��� ��  
    public static int orderLevel; //�մ��� �ֹ� ���̵�
    public static int orderValue;   //�մ��� �䱸�ϴ� ������ ����ġ
    public static int customerNumber;      //�մ��� ��ȣ
    public static float star;     //������ ����


    public static bool isCustomerHere; //ȭ�鿡 �մ��� �����ϴ���
    public static bool isGameOver;   //�丮�� ���� ��������
    public static bool isNextDay; //������ �Ѿ �� ���� ���̵��� ���� ������ �ؾ��ϴ� ��������


    [Header("---------------------[ UI ]")]

    public GameObject orderGroup;   //�ֹ��� �� �� �ʿ��� UI�׷�
    public GameObject orderButten;  //�ֹ��� ���� ���� ��ư
    public GameObject customerGroup;    //�մ��� �Ҵ��ϴ� �׷�
    public GameObject nextOrderButten;  //���� �մ��� ���� ���� ��ư
    public GameObject nextDayGroup;     //���� ���� �Ѿ �� UI


    [Header("---------------------[ Text ]")]

    public Text orderValueText;     //�մ��� �䱸�ϴ� ���� ����ϴ� �ؽ�Ʈ
    public Text orderResultText;    //���� ���� ���� ����ϴ� �ؽ�Ʈ
    public Text valueDisplay;       //�մ��� ��縦 ����ϴ� �ؽ�Ʈ
    public Text customerNameText;   //�մ��� �̸� ���
    public Text dayText;            //���� ������ ����ϴ� �ؽ�Ʈ
    public Text dayTextTitle;       //���� ������ �Ѿ �� ������ ����ϴ� �ؽ�Ʈ 
    public Text orderCountText;     //���ݱ��� �ֹ� ���� ����, ��ǥġ ǥ��
    public Text starText;           //���� ������ ǥ��


    //public Transform customerGroup;    //�մ��� �������� �Ҵ� �� �׷�


    



    public void Start()
    {
        
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

            Invoke("MakeCustomer", 1.5f);   //1.5�� �ڿ� ù �մ��� �����ϱ�
        }


    }

    private void Update()
    {
        customerNameText.text = customerName[customerNumber] + " �� �ֹ� �����մϴ�.".ToString();
        orderValueText.text = "�ֹ� ����: $ " + orderValue.ToString();
        starText.text = "�� :" + star.ToString("N1");    //������ �� ������ ǥ��
        dayText.text = "Day " + day.ToString(); //�� �������� ǥ��
        orderCountText.text = orderCount + " / " + dayCount.ToString(); //�� ���� �ֹ��� �޾ƾ� �ϴ��� ǥ��
    }

    void StartOrder() //�ֹ� ����
    {
        if (isCustomerHere == true)     //�մ��� ȭ�鿡 �ִٸ�
        {

            orderResultText.text = "���: $ 0".ToString();
            customerNumber = Random.Range(0, 9);
            valueDisplay.text = "���� ����".ToString();
            //��ǳ���� �մ��� �䱸�ϴ� �� ����ϱ�
        }
    }

    public void NextOrder() //���� �ֹ�
    {
        orderCount++;   //���� �ֹ� ī��Ʈ +1

        if (star <= 0)
        {
            Ending.isBadEnding = true;
            SceneManager.LoadScene(2);
        }

        if (orderCount == dayCount)
        {
            NextDay();      //���� �� ���� �����켭 ���� ���� �� ��������
        }
        else if (orderCount < dayCount)
        //���� ���ݱ��� ���� �մ��� ���� ���� ���� ���Ǻ��� ���� ���
        {
            orderButten.SetActive(false);   //�ֹ� ��ư ��Ȱ��ȭ
            nextOrderButten.SetActive(false);//���� �ֹ� �ޱ� ��ư ��Ȱ��ȭ
            orderGroup.SetActive(false);     //�ֹ��� ���� UI�׷� ��Ȱ��ȭ
            Invoke("MakeCustomer", 1.5f);    //1.5�� �Ŀ� ���� �մ� ����
        }

    }

    public void NextDay()
    {
        if (orderCount == dayCount)
        //���ݱ��� ���� ���� �մ��� ���� ���� ������ �Ѿ�� ���� �մ��� ���� �����Ѵٸ�
        {
            day++;  //������ +1
            dayText.text = "Day " + day.ToString();
            orderCount = 0;

            orderButten.SetActive(false);
            nextOrderButten.SetActive(false);
            orderGroup.SetActive(false);
            nextDayGroup.SetActive(true);
            customerGroup.SetActive(false);

            dayTextTitle.text = "Day " + day.ToString();

            isNextDay = true;
        }
        else
        {
            return;
        }
    }

    public void NextDayButten() //���� ���� ��ư�� ������ ��
    {
        nextDayGroup.SetActive(false);  //���� ����UI�׷� ��Ȱ��ȭ

        LevelSetting();
        Invoke("MakeCustomer", 1.5f);
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
                
                star -= 1f;  //���� 1�� ����
                Debug.Log(star);
            }
            else if (GameController2048.myScore >= orderValue)
            //���� �÷��̾��� ������ �մ��� �䱸�ϴ� �������� ���ų� ���ٸ�
            {
                OrderSuccessArray();
                if (star == 5)
                {
                    return;
                }
                star += 0.5f;     //���� 1�� ����
                Debug.Log(star);
            }

        }
        customerGroup.SetActive(true);
        orderGroup.SetActive(true);
        orderButten.SetActive(false);
        nextOrderButten.SetActive(true);

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

        customerGroup.SetActive(true);
        orderButten.SetActive(true);
        nextOrderButten.SetActive(false);
        isCustomerHere = true;      //�մ��� �����Ǿ��� ������ �մ��� ���� ���θ� ������ ����
        orderGroup.SetActive(true);



        ValueSetting();
        StartOrder();
    }


    public void ToCookginScene()
    {
        SceneManager.LoadScene(1);
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
        //�ֹ� �������� ���� �ı� ���
    }

    private string[] customerName = //�մ� �̸� �迭
        { "�������� ���� ���", //0
          "���� ����",
          "���� �ް��̴�",
          "�ܹ��� ȣ����",
          "Chat GPT",
          "���ڿ� ��ģ ���", //5
          "���ξ��� ���� ���� �ݴ� ����ȸ",
          "���� �Ѿ����� ŷ��",
          "����� �䳢",
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
          //8��| ����� �䳢
          "���ڿ� �� ���� �丶��ҽ��� ���� ���ڱ����� �ٲ�� ���� �ֽ��ϴ�.",
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
          //8�� �մ� | ����� �䳢
          "������� ��ġ�ؼ� ���ڸ� ����� �ϰ� ���� ������ ���־��!",
          "���ڰ� ���Ƴ׿�. �� ���� ���� �����ο�.",
          "�غ���",
          
    };

    void ValueSetting()     //�մ��� �ֹ� �� ������ ����ġ ����
    {
        if (orderLevel == 0)      //���� orderLevel�� 0�̸�
        {
            orderValue = Random.Range(100, 500);     //100~500���̿��� �������� �ֹ� �� ���� ����ġ�� ���Ѵ�
        }

        else if (orderLevel == 1)      
        {
            orderValue = Random.Range(1000, 2000);     
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

  
    void LevelSetting()  //������ ���� �ֹ� ���̵� ����
    {
        if (day == 1)   //1���� �ų� 2���� ���
        {
            orderLevel = 0; //�ֹ� ���̵��� 0����
            dayCount = 1;
        }
        else if (day == 2 || day == 3)
        {
            orderLevel = 1;
            dayCount = Random.Range(2, 4);
            //���� ������ �Ѿ�� ���� �޾ƾ� �ϴ� �մ��� ���� 2~3 ���̿��� �������� ����

        }
        else if (day == 4 || day == 5)
        {
            orderLevel = 2;
            dayCount = Random.Range(2, 4);
        }
    }
    
    
}
