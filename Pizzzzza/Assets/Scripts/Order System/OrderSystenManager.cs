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
    public static float star;     //������ ����


    public bool isCustomerHere; //ȭ�鿡 �մ��� �����ϴ���
    public static bool isGameOver;   //�丮�� ���� ��������
    public static bool isNextDay; //������ �Ѿ �� ���� ���̵��� ���� ������ �ؾ��ϴ� ��������


    [Header("---------------------[ UI ]")]

    public GameObject orderGroup;   //�ֹ��� �� �� �ʿ��� UI�׷�
    public GameObject orderButten;  //�ֹ��� ���� ���� ��ư
    public GameObject nextOrderButten;  //���� �մ��� ���� ���� ��ư
    public GameObject nextDayGroup;     //���� ���� �Ѿ �� UI


    [Header("---------------------[ Text ]")]

    public Text valueDisplay;       //�մ��� ��縦 ����ϴ� �ؽ�Ʈ
    public Text dayText;            //���� ������ ����ϴ� �ؽ�Ʈ
    public Text dayTextTitle;       //���� ������ �Ѿ �� ������ ����ϴ� �ؽ�Ʈ 
    public Text orderCountText;     //���ݱ��� �ֹ� ���� ����, ��ǥġ ǥ��
    public Text starText;           //���� ������ ǥ��


    [Header("---------------------[ prefab ]")]

    public GameObject customerPrefab;   //�մ��� ������
    public Transform customerGroup;    //�մ��� �������� �Ҵ� �� �׷�

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
        starText.text = "�� :" + star.ToString("N1");    //������ �� ������ ǥ��
        dayText.text = "Day " + day.ToString(); //�� �������� ǥ��
        orderCountText.text = orderCount + " / " + dayCount.ToString(); //�� ���� �ֹ��� �޾ƾ� �ϴ��� ǥ��
    }


    void ValueSetting()     //�մ��� �ֹ� �� ������ ����ġ ����
    {
        if (orderLevel == 0)      //���� orderLevel�� 0�̸�
        {
            orderValue = Random.Range(100, 500);     //100~500���̿��� �������� �ֹ� �� ���� ����ġ�� ���Ѵ�
        }

        else if (orderLevel == 1)      
        {
            orderValue = Random.Range(100, 2000);     
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

    void StartOrder() //�ֹ� ����
    {
        if (isCustomerHere == true)     //�մ��� ȭ�鿡 �ִٸ�
        {
            valueDisplay.text = "$ " + orderValue.ToString();   
            //��ǳ���� �մ��� �䱸�ϴ� �� ����ϱ�
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
        else if(orderCount < dayCount)  
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
        if(orderCount == dayCount)  
        //���ݱ��� ���� ���� �մ��� ���� ���� ������ �Ѿ�� ���� �մ��� ���� �����Ѵٸ�
        {
            day++;  //������ +1
            dayText.text = "Day " + day.ToString();
            orderCount = 0;
            orderButten.SetActive(false);
            nextOrderButten.SetActive(false);
            orderGroup.SetActive(false);

            nextDayGroup.SetActive(true);
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
        if(isGameOver == true)  //�丮 ������ ���� ���� �� ���� True���
        {
            if(GameController2048.myScore < orderValue)  
            //���� �÷��̾��� ������ �մ��� �䱸�ϴ� �������� ���ٸ�
            {
                valueDisplay.text = ":(".ToString();    //:( ���
                star -= 1f;  //���� 1�� ����
                Debug.Log(star);
            }
            else if(GameController2048.myScore >= orderValue)
            //���� �÷��̾��� ������ �մ��� �䱸�ϴ� �������� ���ų� ���ٸ�
            {
                valueDisplay.text = ":)".ToString();    //:) ���
                if(star == 5)
                {
                    return;
                }
                star += 0.5f;     //���� 1�� ����
                Debug.Log(star);
            }
    
        }
        orderGroup.SetActive(true);
        orderButten.SetActive(false);
        nextOrderButten.SetActive(true);

    }

    public void DeleteCustomer()    //���� �մ��� ����� ���� �մ��� �޴� ����
    {
        //Destroy(customerPrefab);
        isGameOver = false;     //���� ���� ���� �ʱ�ȭ

        //��ư�� ui �׷� ��Ȱ��ȭ
        orderButten.SetActive(false);   
        nextOrderButten.SetActive(false);
        orderGroup.SetActive(false);
    }

    void MakeCustomer() //�մ� ���� ����
    {
        //���� ������Ʈ���� �մ� �������� �ν��Ͻ�ȭ �ؼ� �����ϱ�
        GameObject instantCustomerObj = Instantiate(customerPrefab, customerGroup); 
        //�ν��Ͻ�ȭ �� �մ� ������Ʈ�� customer��ũ��Ʈ�� �Ҵ��ϱ� 
        Customer instantCustomer = instantCustomerObj.GetComponent<Customer>();

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
}
