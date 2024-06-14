using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameController2048 : MonoBehaviour
{
    public static int moveCount;
    public static GameController2048 instance;
    public static int ticker;

    [SerializeField] GameObject[] fillPrefab;     //���� ���������� �� ü���
    [SerializeField] Cells[] allCells;      //��� ��
    [SerializeField] Text scoreDisplay;     // ���� ǥ��
    [SerializeField] Text goalScoreDisplay; //��ǥ ���� ǥ��
    [SerializeField] Text moveCountDisplay;

    public static Action<string> slide;
    public static int myScore;  //���� ��
    public Text customerNameText;   //�մ��� �̸� ���
    public Text pizzaNameText;

    public GameObject finishOrder;
    public GameObject pizzaImage;
    public GameObject toppingImage;

    int gameOver;
    [SerializeField] GameObject gameOverPanel;

    

    void Start()
    {

        pizzaImage.SetActive(true);

        pizzaNameText.text = OrderSystenManager.PizzaName[OrderSystenManager.pizzaNumber];
        customerNameText.text = OrderSystenManager.customerName[OrderSystenManager.customerNumber] + " ��".ToString();
        moveCountDisplay.text = moveCount.ToString();
        goalScoreDisplay.text = "$ " + OrderSystenManager.orderValue.ToString();

        Invoke("pizzaTopping", 1.0f);
        
        
        OrderSystenManager.isNextDay = false;
        OrderSystenManager.isGameOver = false;

        myScore = 0;
        //������ �� ���� 2���� ���� ���� 2�� ����

        StartSpawnFill();
        StartSpawnFill();
    }

    void pizzaTopping()
    {
        toppingImage.SetActive(true);
    }

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void FinishOrder()
    {
        SoundManager.instance.PlaySound("Butten_2");
        OrderSystenManager.isGameOver = true;
        SceneManager.LoadScene(1);
    }

    void Update()
    {

        if(myScore >= OrderSystenManager.orderValue)
        {
            finishOrder.SetActive(true);
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SpawnFill();
        //}
        if (OrderSystenManager.isGameOver == false)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                MoveCountUpdate();
                ticker = 0;
                gameOver = 0;
                slide("w");
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                MoveCountUpdate();
                ticker = 0;
                gameOver = 0;
                slide("s");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                MoveCountUpdate();
                ticker = 0;
                gameOver = 0;
                slide("a");
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                MoveCountUpdate();
                ticker = 0;
                gameOver = 0;
                slide("d");
            }
        }
       



    }

    public void MoveCountUpdate()
    {
        MoveOverCheck();
        moveCount--;
        moveCountDisplay.text = moveCount.ToString();
    }

    public void SpawnFill()
    {
        bool isFull = true;

        for(int i = 0; i < allCells.Length; i++)
        {
            if (allCells[i].fill == null)
            {
                isFull = false;
            }
        }

        if (isFull == true)
        {
            return;
        }

        int whichSpawn = UnityEngine.Random.Range(0, allCells.Length);  //������ ���� ���� ����
        if (allCells[whichSpawn].transform.childCount != 0)   //������ ���� ������ �ִ��� Ȯ��
        {
            SpawnFill(); //�ִٸ� SpawnwFill �Լ� �ٽ� ȣ��
            return;
        }
        float chance = UnityEngine.Random.Range(0f, 1f);    //0�� 1 ���̿� ������ ���ڸ� �����Ͽ� Chance������ �Ҵ�
        if (chance < .0f)
        {
            return;
        }

        else if (chance < 1f)   //100%�� Ȯ���� 2���� ���� ���� ����
        {
            GameObject tempFill = Instantiate(fillPrefab[OrderSystenManager.pizzaNumber], allCells[whichSpawn].transform);    //�������� �ν��Ͻ�ȭ �Ͽ� ���� ��ġ
            Fill tempFillComp = tempFill.GetComponent<Fill>();        //�ν��Ͻ�ȭ �� ������Ʈ���� fill ������Ʈ ��������
            allCells[whichSpawn].GetComponent<Cells>().fill = tempFillComp; //��� �� �� ���Ƿ� ���� �� �� ������Ʈ�� fill �Ӽ� ����         
            tempFillComp.FillValueUpdate(2);        //fill ��ũ��Ʈ�� FillValueUpdate �Լ��� ȣ���Ͽ� ���� 2�� ������Ʈ
        }

        //else if (chance < 0.2f) //%�� Ȯ���� 4���� ���� ���� ����
        //{
        //    GameObject tempFill = Instantiate(fillPrefab, allCells[whichSpawn]);
        //    Fill tempFillComp = tempFill.GetComponent<Fill>();
        //    allCells[whichSpawn].GetComponent<Cells>().fill = tempFillComp;
        //    tempFillComp.FillValueUpdate(4);
        //}
    }

    public void StartSpawnFill()    
    {

        int whichSpawn = UnityEngine.Random.Range(0, allCells.Length);  //������ ���� ���� ����
        if (allCells[whichSpawn].transform.childCount != 0)
        {
            SpawnFill();
            return;
        }
        
            GameObject tempFill = Instantiate(fillPrefab[OrderSystenManager.pizzaNumber], allCells[whichSpawn].transform);
            Fill tempFillComp = tempFill.GetComponent<Fill>();
            allCells[whichSpawn].GetComponent<Cells>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(2);
      
    }

    public void ScoreUpdate(int ScoreIn)
    {
        myScore += ScoreIn;
        scoreDisplay.text = "$ " + myScore.ToString();
    }

    public void GameOverCheck()
    {
        gameOver++;
        if(gameOver >= 16)
        {
            gameOverPanel.SetActive(true);
            OrderSystenManager.isGameOver = true;
        }
    }
    public void GameOverCheck5x5()
    {
        gameOver++;
        if (gameOver >= 25)
        {
            gameOverPanel.SetActive(true);
            OrderSystenManager.isGameOver = true;
        }
    }

    public void MoveOverCheck()
    {
        if(moveCount <= 1)  //MoveCount�� 0�� �Ǹ�
        {
            gameOverPanel.SetActive(true);
            OrderSystenManager.isGameOver = true;   //���� ����
        }
    }

    public void ToOrderScene()
    {
        SoundManager.instance.PlaySound("Butten_2");
        SceneManager.LoadScene(1);
        
    }

    
}
