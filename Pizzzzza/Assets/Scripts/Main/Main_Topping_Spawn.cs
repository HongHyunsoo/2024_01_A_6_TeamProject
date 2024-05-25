using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Main_Topping_Spawn : MonoBehaviour
{
    public GameObject [] topping;
    public float checkTime;     //�ð� üũ
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
            checkTime = 0.0f;                           //�ð� �ʱ�ȭ
            GameObject temp = Instantiate(topping[Random.Range(0, 5)]);      //������ �ڽ� �������� instantiate �� �����Ѵ�.
            temp.transform.position = new Vector3(LocationX, 6.0f, -0.5f);      //������ �� ��ũ��Ʈ�� �ִ� ������Ʈ ��ġ�� ���� 
            float RandomNumberX = Random.Range(0, 2.0f);                         //0�� 8������ ���� ���� �޾Ƽ�
            temp.transform.position += new Vector3(RandomNumberX, 0.0f, 0.0f);   //x, y �� ��ġ�� �����ش�.

            Destroy(temp, 10.0f);
        }
        else if (checkTime >= Random.Range(0.6f, 0.9f) && firstSpawn == 1)
        {
            checkTime = 0.0f;                           //�ð� �ʱ�ȭ
            GameObject temp = Instantiate(topping[Random.Range(0, 5)]);      //������ �ڽ� �������� instantiate �� �����Ѵ�.
            temp.transform.position = new Vector3(LocationX, 6.0f, -0.5f);      //������ �� ��ũ��Ʈ�� �ִ� ������Ʈ ��ġ�� ���� 
            float RandomNumberX = Random.Range(0, 2.0f);                         //0�� 8������ ���� ���� �޾Ƽ�
            temp.transform.position += new Vector3(RandomNumberX, 0.0f, 0.0f);   //x, y �� ��ġ�� �����ش�.
            firstSpawn++;

            Destroy(temp, 10.0f);
        }
    }
}
