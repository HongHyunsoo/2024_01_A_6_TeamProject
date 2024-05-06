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

    [SerializeField] GameObject fillPrefab;     //���� ���������� �� ü���
    [SerializeField] Transform[] allCells;      //��� ��
    [SerializeField] Text scoreDisplay;

    public static Action<string> slide;
    public int myScore;

    void Start()
    {
        //������ �� ���� 2���� ���� ���� 2�� ����
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

        int whichSpawn = UnityEngine.Random.Range(0, allCells.Length);  //������ ���� ���� ����
        if (allCells[whichSpawn].childCount != 0)   //������ ���� ������ �ִ��� Ȯ��
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
            GameObject tempFill = Instantiate(fillPrefab, allCells[whichSpawn]);    //�������� �ν��Ͻ�ȭ �Ͽ� ���� ��ġ
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
