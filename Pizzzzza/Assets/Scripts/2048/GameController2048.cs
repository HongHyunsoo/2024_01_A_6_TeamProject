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

    [SerializeField] GameObject[] fillPrefab;     //토핑 프레펩으로 셀 체우기
    [SerializeField] Cells[] allCells;      //모든 셀
    [SerializeField] Text scoreDisplay;     // 점수 표시
    [SerializeField] Text goalScoreDisplay; //목표 점수 표시
    [SerializeField] Text moveCountDisplay;

    public static Action<string> slide;
    public static int myScore;  //점수 값
    public Text customerNameText;   //손님의 이름 출력
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
        customerNameText.text = OrderSystenManager.customerName[OrderSystenManager.customerNumber] + " 님".ToString();
        moveCountDisplay.text = moveCount.ToString();
        goalScoreDisplay.text = "$ " + OrderSystenManager.orderValue.ToString();

        Invoke("pizzaTopping", 1.0f);
        
        
        OrderSystenManager.isNextDay = false;
        OrderSystenManager.isGameOver = false;

        myScore = 0;
        //시작할 때 셀에 2값을 가진 토핑 2개 생성

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

        int whichSpawn = UnityEngine.Random.Range(0, allCells.Length);  //랜덤한 셀에 토핑 생성
        if (allCells[whichSpawn].transform.childCount != 0)   //선택한 셀에 토핑이 있는지 확인
        {
            SpawnFill(); //있다면 SpawnwFill 함수 다시 호출
            return;
        }
        float chance = UnityEngine.Random.Range(0f, 1f);    //0과 1 사이에 렌덤한 숫자를 생성하여 Chance변수에 할당
        if (chance < .0f)
        {
            return;
        }

        else if (chance < 1f)   //100%의 확률로 2값을 가진 토핑 생성
        {
            GameObject tempFill = Instantiate(fillPrefab[OrderSystenManager.pizzaNumber], allCells[whichSpawn].transform);    //프리펩을 인스턴스화 하여 셀에 배치
            Fill tempFillComp = tempFill.GetComponent<Fill>();        //인스턴스화 된 오브젝트에서 fill 컴포넌트 가져오기
            allCells[whichSpawn].GetComponent<Cells>().fill = tempFillComp; //모든 셀 중 임의로 선택 된 셀 컴포넌트에 fill 속성 설정         
            tempFillComp.FillValueUpdate(2);        //fill 스크립트의 FillValueUpdate 함수를 호출하여 값을 2로 업데이트
        }

        //else if (chance < 0.2f) //%의 확률로 4값을 가진 토핑 생성
        //{
        //    GameObject tempFill = Instantiate(fillPrefab, allCells[whichSpawn]);
        //    Fill tempFillComp = tempFill.GetComponent<Fill>();
        //    allCells[whichSpawn].GetComponent<Cells>().fill = tempFillComp;
        //    tempFillComp.FillValueUpdate(4);
        //}
    }

    public void StartSpawnFill()    
    {

        int whichSpawn = UnityEngine.Random.Range(0, allCells.Length);  //랜덤한 셀에 토핑 생성
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
        if(moveCount <= 1)  //MoveCount가 0이 되면
        {
            gameOverPanel.SetActive(true);
            OrderSystenManager.isGameOver = true;   //게임 오버
        }
    }

    public void ToOrderScene()
    {
        SoundManager.instance.PlaySound("Butten_2");
        SceneManager.LoadScene(1);
        
    }

    
}
