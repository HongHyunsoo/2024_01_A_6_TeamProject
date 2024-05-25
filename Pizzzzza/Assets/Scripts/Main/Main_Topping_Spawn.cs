using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Main_Topping_Spawn : MonoBehaviour
{
    public GameObject [] topping;
    public float checkTime;     //시간 체크
    public float LocationX;
    public float RandomRangeMin;
    public float RandomRangeMax;
    int firstSpawn = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkTime += Time.deltaTime;
        if (checkTime >= Random.Range(RandomRangeMin, RandomRangeMax) && firstSpawn == 2)
        {
            checkTime = 0.0f;                           //시간 초기화
            GameObject temp = Instantiate(topping[Random.Range(0, 5)]);      //아이템 박스 프리팹을 instantiate 로 생성한다.
            temp.transform.position = new Vector3(LocationX, 6.0f, -0.5f);      //생성할 때 스크립트가 있는 오브젝트 위치로 생성 
            float RandomNumberX = Random.Range(0, 2.0f);                         //0에 8까지의 랜덤 값을 받아서
            temp.transform.position += new Vector3(RandomNumberX, 0.0f, 0.0f);   //x, y 값 위치에 더해준다.

            Destroy(temp, 10.0f);
        }
        else if (checkTime >= Random.Range(0.6f, 0.9f) && firstSpawn == 1)
        {
            checkTime = 0.0f;                           //시간 초기화
            GameObject temp = Instantiate(topping[Random.Range(0, 5)]);      //아이템 박스 프리팹을 instantiate 로 생성한다.
            temp.transform.position = new Vector3(LocationX, 6.0f, -0.5f);      //생성할 때 스크립트가 있는 오브젝트 위치로 생성 
            float RandomNumberX = Random.Range(0, 2.0f);                         //0에 8까지의 랜덤 값을 받아서
            temp.transform.position += new Vector3(RandomNumberX, 0.0f, 0.0f);   //x, y 값 위치에 더해준다.
            firstSpawn++;

            Destroy(temp, 10.0f);
        }
    }
}
