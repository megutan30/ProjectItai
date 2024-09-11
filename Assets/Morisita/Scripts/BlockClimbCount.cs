using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockClimbCount : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int climbCount = 0;
    [SerializeField]
    private int LimitClimbCount = 0;

    [SerializeField]
    private int goalHeight = 30;

    [SerializeField]
    private TextMeshProUGUI countText;
    [SerializeField]
    private TextMeshProUGUI limitCountText;
    void Start()
    {
        if (countText != null)
        {
            LimitClimbCount = goalHeight - climbCount;
            countText.text = "ClimbCount : " + climbCount.ToString("000");
            limitCountText.text = "limitCount : " + LimitClimbCount.ToString("000");
        }
        else
        {
            Debug.LogWarning("カウント用のテキストがありません。");
        }
    }

    // Update is called once per frame
    void Update()
    {
        DisplayCount();
    }

    public void DisplayCount()
    {
        LimitClimbCount = goalHeight - climbCount;
        countText.text = "ClimbCount : " + climbCount.ToString("000");
        limitCountText.text = "limitCount : " + LimitClimbCount.ToString("000");
    }
    public void AddClimbCount()
    {
        climbCount++;
    }

    public bool IsClearCheck()
    {
        return climbCount >= goalHeight ? true : false;
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
