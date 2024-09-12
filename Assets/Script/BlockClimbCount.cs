using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockClimbCount : MonoBehaviour
{
    [SerializeField] public int climbCount = 0;
    [SerializeField] private int LimitClimbCount = 0;
    [SerializeField] private int goalHeight = 25;

    [SerializeField] private Image[] countImages; // �N���C���񐔂̊e���ɑΉ�����摜
    [SerializeField] private Sprite[] numberSprites; // 0-9�̐����ɑΉ�����X�v���C�g

    private int restClimb = 100;
    private int reduece; 

    void Start()
    {
        if (countImages != null && numberSprites != null && numberSprites.Length == 10)
        {
            LimitClimbCount = goalHeight - climbCount;
            UpdateDisplays();
        }
        else
        {
            Debug.LogWarning("�摜�܂��̓X�v���C�g���������ݒ肳��Ă��܂���B");
        }
        reduece = 100 / (25);
    }

    void Update()
    {
        CalucateRestClimb();
        UpdateDisplays();
    }

    void CalucateRestClimb()
    {
        restClimb = 100 - (reduece * climbCount);
    }

    private void UpdateDisplays()
    {
        LimitClimbCount = goalHeight - climbCount;
        UpdateNumberDisplay(countImages, restClimb);
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