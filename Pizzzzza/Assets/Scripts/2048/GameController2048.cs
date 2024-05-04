using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController2048 : MonoBehaviour
{
    [SerializeField] GameObject fillPrefab;     //ÅäÇÎ ÇÁ·¹ÆéÀ¸·Î ¼¿ Ã¼¿ì±â
    [SerializeField] Transform[] allCells;      //¸ðµç ¼¿

    public static Action<string> slide;
    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnFill();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            slide("w");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            slide("s");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            slide("a");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            slide("d");
        }
    }

    public void SpawnFill()
    {

        int whichSpawn = UnityEngine.Random.Range(0, allCells.Length);  //·£´ýÇÑ ¼¿¿¡ ÅäÇÎ »ý¼º
        if (allCells[whichSpawn].childCount != 0)
        {
            Debug.Log(allCells[whichSpawn].name + "Is Altrady filled");
            SpawnFill();
            return;
        }
        float chance = UnityEngine.Random.Range(0f, 1f);
        Debug.Log(chance);
        if (chance < .2f)
        {
            return;
        }

        else if (chance < .8f)
        {
            GameObject tempFill = Instantiate(fillPrefab, allCells[whichSpawn]);
            Fill tempFillComp = tempFill.GetComponent<Fill>();
            allCells[whichSpawn].GetComponent<Cells>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(2);
        }
        else
        {
            GameObject tempFill = Instantiate(fillPrefab, allCells[whichSpawn]);
            Fill tempFillComp = tempFill.GetComponent<Fill>();
            allCells[whichSpawn].GetComponent<Cells>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(4);
        }
    }
}
