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
    public static int star;     //������ ����
    public static int maxStar = 10;     //������ �ִ� ����

    public bool isCustomerHere; //ȭ�鿡 �մ��� �����ϴ���
    public static bool isGameOver;  //�丮 ������ ���� ���� ��������

    public GameObject orderGroup;   //�ֹ��� �� �� �ʿ��� UI�׷�
    public GameObject orderButten;  //�ֹ��� ���� ���� ��ư
    public GameObject nextOrderButten;  //���� �մ��� ���� ���� ��ư
    public GameObject nextDayGroup;     //���� ���� �Ѿ �� UI

    public Text valueDisplay;       //�մ��� ��縦 ����ϴ� �ؽ�Ʈ
    public Text dayText;            //���� ������ ����ϴ� �ؽ�Ʈ
    public Text dayTextTitle;            //���� ������ ����ϴ� �ؽ�Ʈ 
    public Text orderCountText;     //���ݱ��� �ֹ� ���� ����, ��ǥġ ǥ��


    public GameObject customerPrefab;   //�մ��� ������
    public Transform customerGroup;    //�մ��� �������� �Ҵ� �� �׷�

    public void Start()
    {

        nextDayGroup.SetActive(false);
        


        //���� ���� ���� ���¶�� �丮 �� ��������
        if (isGameOver == true)
        {
            Debug.Log("���� ���� ���� ��");
            LevelSetting();

            CookingFinish();

        }
        //���� ���� ���� ���°� �ƴ϶�� ù �մ� ����
        else
        {
            day = 1;    //���� �� ������ 1�� ����
            isCustomerHere = false; // �մ� ���� ���� false
            star = 5; //������ �ʱ� ���� 5�� ����

            LevelSetting();

            Invoke("MakeCustomer", 1.5f);   //1.5�� �ڿ� ù �մ��� �����ϱ�
        }


    }

    private void Update()
    {
        dayText.text = "Day " + day.ToString();
        orderCountText.text = orderCount + " / " + dayCount.ToString();
    }


    void ValueSetting()     //�մ��� �ֹ� �� ������ ����ġ ����
    {
        if (orderLevel == 0)      //���� orderLevel�� 0�̸�
        {
            orderValue = Random.Range(100, 500);     //50~200���̿��� �������� �ֹ� �� ���� ����ġ�� ���Ѵ�
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
            dayCount = Random.Range(1, 2);
            //���� ������ �Ѿ�� ���� �޾ƾ� �ϴ� �մ��� ���� 2~3 ���̿��� �������� ����
        }
        else if (day == 2 || day == 3)
        {
            orderLevel = 1;
            dayCount = Random.Range(2, 4);
        }

    }
    
    public void NextOrder() //���� �ֹ�
    {
        orderCount++;   //���� �ֹ� ī��Ʈ +1
        if (orderCount == dayCount)
        {
            NextDay();      //���� �� ���� �����켭 ���� ���� �� ��������
        }
        else if(orderCount < dayCount)  
        //���� ���ݱ��� ���� �մ��� ���� ���� ���� ���Ǻ��� ���� ���
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

    public void CookingFinish()     //�丮�� ���� �Ŀ� ����
    {
        if(isGameOver == true)  //�丮 ������ ���� ���� �� ���� True���
        {
            if(GameController2048.myScore < orderValue)  
            //���� �÷��̾��� ������ �մ��� �䱸�ϴ� �������� ���ٸ�
            {
                valueDisplay.text = ":(".ToString();    //:( ���
                star -= 2;  //���� 2�� ����
                Debug.Log(star);
            }
            else if(GameController2048.myScore >= orderValue)
            //���� �÷��̾��� ������ �մ��� �䱸�ϴ� �������� ���ų� ���ٸ�
            {
                valueDisplay.text = ":)".ToString();    //:) ���
                star++;     //���� 1�� ����
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