using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OrderSystenManager : MonoBehaviour
{
    public static int day; //����
    public int orderLevel; //�մ��� �ֹ� ���̵�
    public static int orderValue;   //������ ��
    public bool isCustomerHere; //ȭ�鿡 �մ��� �����ϴ���
    public static bool isGameOver;

    public GameObject orderGroup;   //�ֹ��� �� �� �ʿ��� UI�׷�
    public GameObject orderButten;
    public GameObject nextOrderButten;
    public Text valueDisplay;       //�մ��� ��縦 ����ϴ� �ؽ�Ʈ


    public GameObject customerPrefab;   //�մ��� ������
    public Transform customerGroup;    //�մ��� �������� �Ҵ� �� �׷�

    void Start()
    {
        isCustomerHere = false; // �մ� ���� ���� false
        day = 1;    //���� �� ������ 1�� ����

        //���� ���� ���� ���¶�� �丮 �� ��������
        if (isGameOver == true)
        {
            Debug.Log("���� ���� ���� ��");
            CookingFinish();
        }
        //���� ���� ���� ���°� �ƴ϶�� ù �մ� ����
        else
        {
            Invoke("MakeCustomer", 1.5f);   //1.5�� �ڿ� ù �մ��� �����ϱ�
        }

    }


    void ValueSetting()     //�մ��� �ֹ� �� ������ ����ġ ����
    {
        if (orderLevel == 0)      //���� orderLevel�� 0�̸�
        {
            orderValue = Random.Range(10000, 20000);     //50~200���̿��� �������� �ֹ� �� ���� ����ġ�� ���Ѵ�
        }

        if (orderLevel == 1)      //���� orderLevel�� 0�̸�
        {
            orderValue = Random.Range(200, 400);     //200~400���̿��� �������� �ֹ� �� ���� ����ġ�� ���Ѵ�
        }

    }

    void StartOrder()
    {
        if (isCustomerHere == true)     //�մ��� ȭ�鿡 �ִٸ�
        {
            valueDisplay.text = "$ " + orderValue.ToString();   //��ǳ���� �� ����ϱ�
        }
    }
    void LevelSetting()
    {
        if (day == 1)
        {
            orderLevel = 0;
        }
    }
    public void NextCustomer() //���� �մ� ����
    {
        
         Invoke("MakeCustomer", 1.5f);    //1.5�� �ڿ� ù �մ��� �����ϱ�
        
        
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
        SceneManager.LoadScene(0);
    }
}
