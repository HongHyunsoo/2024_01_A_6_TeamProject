using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController2048 : MonoBehaviour
{
    public static GameController2048 instance;
    public static int ticker;

    [SerializeField] GameObject fillPrefab;     //토핑 프레펩으로 셀 체우기
    [SerializeField] Transform[] allCells;      //모든 셀
    [SerializeField] Text scoreDisplay;

    public static Action<string> slide;
    public int myScore;

    void Start()
    {
        //시작할 때 셀에 2값을 가진 토핑 2개 생성
        StartSpawnFill();
        StartSpawnFill();
    }

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SpawnFill();
        //}
        if (Input.GetKeyDown(KeyCode.W))
        {
            ticker = 0;
            slide("w");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ticker = 0;
            slide("s");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            ticker = 0;
            slide("a");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ticker = 0;
            slide("d");
        }

    }

    public void SpawnFill()
    {

        int whichSpawn = UnityEngine.Random.Range(0, allCells.Length);  //랜덤한 셀에 토핑 생성
        if (allCells[whichSpawn].childCount != 0)   //선택한 셀에 토핑이 있는지 확인
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
            GameObject tempFill = Instantiate(fillPrefab, allCells[whichSpawn]);    //프리펩을 인스턴스화 하여 셀에 배치
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
        if (allCells[whichSpawn].childCount != 0)
        {
            Debug.Log(allCells[whichSpawn].name + "Is Altrady filled");
            SpawnFill();
            return;
        }
        
            GameObject tempFill = Instantiate(fillPrefab, allCells[whichSpawn]);
            Fill tempFillComp = tempFill.GetComponent<Fill>();
            allCells[whichSpawn].GetComponent<Cells>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(2);
      
    }

    public void ScoreUpdate(int ScoreIn)
    {
        myScore += ScoreIn;
        scoreDisplay.text = "$ " + myScore.ToString();
    }
}
