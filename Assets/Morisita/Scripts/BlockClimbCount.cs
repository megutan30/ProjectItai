using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockClimbCount : MonoBehaviour
{
    [SerializeField] private int climbCount = 0;
    [SerializeField] private int LimitClimbCount = 0;
    [SerializeField] private int goalHeight = 30;

    [SerializeField] private Image[] countImages; // クライム回数の各桁に対応する画像
    [SerializeField] private Sprite[] numberSprites; // 0-9の数字に対応するスプライト

    void Start()
    {
        if (countImages != null && numberSprites != null && numberSprites.Length == 10)
        {
            LimitClimbCount = goalHeight - climbCount;
            UpdateDisplays();
        }
        else
        {
            Debug.LogWarning("画像またはスプライトが正しく設定されていません。");
        }
    }

    void Update()
    {
        UpdateDisplays();
    }

    private void UpdateDisplays()
    {
        LimitClimbCount = goalHeight - climbCount;
        UpdateNumberDisplay(countImages, climbCount);
    }

    private void UpdateNumberDisplay(Image[] images, int number)
    {
        string numberString = number.ToString("000");
        for (int i = 0; i < 3; i++)
        {
            if (i < images.Length)
            {
                int digit = int.Parse(numberString[i].ToString());
                images[i].sprite = numberSprites[digit];
            }
        }
    }

    public void AddClimbCount()
    {
        climbCount++;
    }

    public bool IsClearCheck()
    {
        return climbCount >= goalHeight;
    }

    public int GetClimbCount()
    {
        return climbCount;
    }

    public int GetGoalHeight()
    {
        return goalHeight;
    }
}