using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
	public Text moneyText; // UI에 돈을 표시할 텍스트 필드

    private int[] dailyMoney = new int[5]; // 각 날짜별 돈
    private int totalMoney = 0; // 누적된 돈

    void Start()
    {
        // 게임 시작할 때 누적된 돈을 표시
        UpdateMoneyText();
    }

    // 돈을 추가하고 텍스트 업데이트
    public void AddMoney(int day, int amount)
    {
        dailyMoney[day - 1] += amount; // 배열 인덱스는 0부터 시작하므로 day에서 1을 빼줌
        totalMoney += amount;
        UpdateMoneyText();
    }

    // 텍스트 업데이트
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

