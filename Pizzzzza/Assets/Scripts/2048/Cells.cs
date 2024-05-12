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
    private void OnSlide(string whatWasSent)    //어떤 버튼을 눌렀는지 감지하기
    {
        CellCheck();

        
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

        GameController2048.ticker++;
        if(GameController2048.ticker == 4)
        {
            GameController2048.instance.SpawnFill();
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
            if (nextCell.fill != null)   //만약 다음 셀이 비어있지 않다면
            {
                if (currentCell.fill.value == nextCell.fill.value)   //나와 상대의 값이 같다면
                {
                    nextCell.fill.Double(); //값을 합치기
                    nextCell.fill.transform.parent = currentCell.transform;
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else if(currentCell.down.fill != nextCell.fill)
                {
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
        
        if (currentCell.fill != null)
        {
            Cells nextCell = currentCell.up;
            while (nextCell.up != null && nextCell.fill == null)
            {
                nextCell = nextCell.up;
            }
            if (nextCell.fill != null)  
            {
                if (currentCell.fill.value == nextCell.fill.value)  
                {
                    nextCell.fill.Double();
                    nextCell.fill.transform.parent = currentCell.transform;
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else if (currentCell.up.fill != nextCell.fill)
                {
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

        if (currentCell.fill != null)
        {
            Cells nextCell = currentCell.right;
            while (nextCell.right != null && nextCell.fill == null)
            {
                nextCell = nextCell.right;
            }
            if (nextCell.fill != null)   
            {
                if (currentCell.fill.value == nextCell.fill.value)   
                {
                    nextCell.fill.Double();
                    nextCell.fill.transform.parent = currentCell.transform;
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else if (currentCell.right.fill != nextCell.fill)
                {
                    nextCell.fill.transform.parent = currentCell.right.transform;
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
            }
        }

        if (currentCell.right == null)
            return;
        SlideLeft(currentCell.right);

    }





    void SlideRight(Cells currentCell)
    {
        if (currentCell.left == null)
        
            return;
        
        if (currentCell.fill != null)
        {
            Cells nextCell = currentCell.left;
            while (nextCell.left != null && nextCell.fill == null)
            {
                nextCell = nextCell.left;
            }
            if (nextCell.fill != null) 
            {
                if (currentCell.fill.value == nextCell.fill.value)   
                {
                    nextCell.fill.Double();
                    nextCell.fill.transform.parent = currentCell.transform;
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else if(currentCell.left.fill != nextCell.fill)    
                {
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
            }
        }

        if (currentCell.left == null)
            return;
        SlideRight(currentCell.left);

    }

    void CellCheck()
    {
        if (fill == null)
            return;

        if(up != null)
        {
            if (up.fill == null)
                return;
            if (up.fill.value == fill.value)
                return;
        }

        if (down != null)
        {
            if (down.fill == null)
                return;
            if (down.fill.value == fill.value)
                return;
        }

        if (left != null)
        {
            if (left.fill == null)
                return;
            if (left.fill.value == fill.value)
                return;
        }

        if (right != null)
        {
            if (right.fill == null)
                return;
            if (right.fill.value == fill.value)
                return;
        }
        GameController2048.instance.GameOverCheck();
    }
}
