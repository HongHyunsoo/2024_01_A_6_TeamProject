using UnityEngine;

public class Cells : MonoBehaviour
{
    public Cells left;
    public Cells right;
    public Cells up;
    public Cells down;

    public Fill fill;

    private void OnEnable()
    {
        GameController2048.slide += OnSlide;
    }

    private void OnDisable()
    {
        GameController2048.slide -= OnSlide;
    }
    private void OnSlide(string whatWasSent)    //� ��ư�� �������� �����ϱ�
    {
        Debug.Log(whatWasSent);
        if (whatWasSent == "w")
        {
            if (up != null)
                return;
            Cells currentCell = this;
            SlideUp(currentCell);
        }
        if (whatWasSent == "s")
        {
            if (down != null)
                return;
            Cells currentCell = this;
            SlideDown(currentCell);
        }
        if (whatWasSent == "a")
        {
            if (left != null)
                return;
            Cells currentCell = this;
            SlideLeft(currentCell);
        }
        if (whatWasSent == "d")
        {
            if (right != null)
                return;
            Cells currentCell = this;
            SlideRight(currentCell);
        }
    }

    void SlideUp(Cells currentCell)
    {
        if (currentCell.down == null)
        
            return;
        
       
        if (currentCell.fill != null)
        {
            Cells nextCell = currentCell.down;
            while (nextCell.down != null && nextCell.fill == null)
            {
                nextCell = nextCell.down;
            }
            if (nextCell.fill != null)   //���� ���� ���� ������� �ʴٸ�
            {
                if (currentCell.fill.value == nextCell.fill.value)   //���� ����� ���� ���ٸ�
                {
                    nextCell.fill.Double();
                    nextCell.fill.transform.parent = currentCell.transform;
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else if(currentCell.down.fill != nextCell.fill)
                {
                    Debug.Log("Not Doubled");
                    nextCell.fill.transform.parent = currentCell.down.transform;
                    currentCell.down.fill = nextCell.fill;
                    nextCell.fill = null;
                }
            }
        }
        else
        {
            Cells nextCell = currentCell.down;
            while (nextCell.down != null && nextCell.fill == null)
            {
                nextCell = nextCell.down;
            }
            if (nextCell.fill != null)
            {
                nextCell.fill.transform.parent = currentCell.transform;
                currentCell.fill = nextCell.fill;
                nextCell.fill = null;
                SlideUp(currentCell);
                Debug.Log("Slide to Empty");
            }
        }

        if (currentCell.down == null)
            return;
        SlideUp(currentCell.down);

    }





    void SlideDown(Cells currentCell)
    {
        if (currentCell.up == null)
        
            return;
        
        Debug.Log(currentCell.gameObject);
        if (currentCell.fill != null)
        {
            Cells nextCell = currentCell.up;
            while (nextCell.up != null && nextCell.fill == null)
            {
                nextCell = nextCell.up;
            }
            if (nextCell.fill != null)   //���� ���� ���� ������� �ʴٸ�
            {
                if (currentCell.fill.value == nextCell.fill.value)   //���� ����� ���� ���ٸ�
                {
                    nextCell.fill.Double();
                    nextCell.fill.transform.parent = currentCell.transform;
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else if (currentCell.up.fill != nextCell.fill)
                {
                    Debug.Log("Not Doubled");
                    nextCell.fill.transform.parent = currentCell.up.transform;
                    currentCell.up.fill = nextCell.fill;
                    nextCell.fill = null;
                }
            }
        }
        else
        {
            Cells nextCell = currentCell.up;
            while (nextCell.up != null && nextCell.fill == null)
            {
                nextCell = nextCell.up;
            }
            if (nextCell.fill != null)
            {
                nextCell.fill.transform.parent = currentCell.transform;
                currentCell.fill = nextCell.fill;
                nextCell.fill = null;
                SlideDown(currentCell);
                Debug.Log("Slide to Empty");
            }
        }

        if (currentCell.up == null)
            return;
        SlideDown(currentCell.up);
    }





    void SlideLeft(Cells currentCell)
    {
        if (currentCell.right == null)
        
            return;
        
        Debug.Log(currentCell.gameObject);
        if (currentCell.fill != null)
        {
            Cells nextCell = currentCell.right;
            while (nextCell.down != null && nextCell.fill == null)
            {
                nextCell = nextCell.right;
            }
            if (nextCell.fill != null)   //���� ���� ���� ������� �ʴٸ�
            {
                if (currentCell.fill.value == nextCell.fill.value)   //���� ����� ���� ���ٸ�
                {
                    nextCell.fill.Double();
                    nextCell.fill.transform.parent = currentCell.transform;
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else if(currentCell.right.fill != nextCell.fill)
                {
                    Debug.Log("Not Doubled");
                    nextCell.fill.transform.parent = currentCell.down.transform;
                    currentCell.right.fill = nextCell.fill;
                    nextCell.fill = null;
                }
            }
        }
        else
        {
            Cells nextCell = currentCell.right;
            while (nextCell.right != null && nextCell.fill == null)
            {
                nextCell = nextCell.right;
            }
            if (nextCell.fill != null)
            {
                nextCell.fill.transform.parent = currentCell.transform;
                currentCell.fill = nextCell.fill;
                nextCell.fill = null;
                SlideLeft(currentCell);
                Debug.Log("Slide to Empty");
            }
        }

        if (currentCell.right == null)
            return;
        SlideLeft(currentCell.down);

    }





    void SlideRight(Cells currentCell)
    {
        if (currentCell.left == null)
        
            return;
        
        Debug.Log(currentCell.gameObject);
        if (currentCell.fill != null)
        {
            Cells nextCell = currentCell.left;
            while (nextCell.left != null && nextCell.fill == null)
            {
                nextCell = nextCell.left;
            }
            if (nextCell.fill != null)   //���� ���� ���� ������� �ʴٸ�
            {
                if (currentCell.fill.value == nextCell.fill.value)   //���� ����� ���� ���ٸ�
                {
                    nextCell.fill.Double();
                    nextCell.fill.transform.parent = currentCell.transform;
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else if(currentCell.left.fill != nextCell.fill)
                {
                    Debug.Log("Not Doubled");
                    nextCell.fill.transform.parent = currentCell.left.transform;
                    currentCell.left.fill = nextCell.fill;
                    nextCell.fill = null;
                }
            }
        }
        else
        {
            Cells nextCell = currentCell.left;
            while (nextCell.left != null && nextCell.fill == null)
            {
                nextCell = nextCell.left;
            }
            if (nextCell.fill != null)
            {
                nextCell.fill.transform.parent = currentCell.transform;
                currentCell.fill = nextCell.fill;
                nextCell.fill = null;
                SlideRight(currentCell);
                Debug.Log("Slide to Empty");
            }
        }

        if (currentCell.left == null)
            return;
        SlideRight(currentCell.left);

    }
}