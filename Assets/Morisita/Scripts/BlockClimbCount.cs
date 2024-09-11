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
    private int goalHeight = 30;

    [SerializeField]
    private TextMeshProUGUI countText;
    void Start()
    {
        if (countText != null)
        {
            countText.text = "ClimbCount : " + climbCount.ToString("000");
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
        countText.text= "ClimbCount : " + climbCount.ToString("000");
    }
    public void AddClimbCount()
    {
        climbCount++;
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
