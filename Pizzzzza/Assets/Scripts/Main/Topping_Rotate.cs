using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Topping_Rotate : MonoBehaviour
{
    float distance = 5;

    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * 90));
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DistroyTopping")
        {
            Destroy(gameObject);
        }
    }
}
