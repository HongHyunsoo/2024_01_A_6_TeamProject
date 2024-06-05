using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
	public Text moneyText; // UI�� ���� ǥ���� �ؽ�Ʈ �ʵ�

    private int[] dailyMoney = new int[5]; // �� ��¥�� ��
    private int totalMoney = 0; // ������ ��

    void Start()
    {
        // ���� ������ �� ������ ���� ǥ��
        UpdateMoneyText();
    }

    // ���� �߰��ϰ� �ؽ�Ʈ ������Ʈ
    public void AddMoney(int day, int amount)
    {
        dailyMoney[day - 1] += amount; // �迭 �ε����� 0���� �����ϹǷ� day���� 1�� ����
        totalMoney += amount;
        UpdateMoneyText();
    }

    // �ؽ�Ʈ ������Ʈ
    void UpdateMoneyText()
    {
        string moneyInfo = "Total Money: $" + totalMoney.ToString() + "\n";
        for (int i = 0; i < dailyMoney.Length; i++)
        {
            moneyInfo += "Day " + (i + 1) + " Money: $" + dailyMoney[i] + "\n";
        }
        moneyText.text = moneyInfo;
    }
}

