using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMovement : MonoBehaviour
{
    [SerializeField]
    private BlockClimbCount blockClimbCount;

    [SerializeField]
    private SpriteRenderer backGroundImage;

    [SerializeField]
    private float imageHeightCorrectionValue;

    [SerializeField]
    private float bgImageNowHeight;

    private float bgImageOriginalHeight;

    private int oldClimbCount;
    // Start is called before the first frame update
    void Start()
    {
        if (blockClimbCount != null)
        {
            float height = backGroundImage.bounds.size.y-imageHeightCorrectionValue;

            bgImageOriginalHeight=backGroundImage.transform.position.y;
            bgImageNowHeight = height * blockClimbCount.GetClimbCount() / blockClimbCount.GetGoalHeight();
        }
        else
        {
            Debug.LogWarning("BlockClimbCountÇ™Ç†ÇËÇ‹ÇπÇÒÅB");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (oldClimbCount != blockClimbCount.GetClimbCount())
            BGDisplay();

        oldClimbCount = blockClimbCount.GetClimbCount();

    }

    public float GetBGHeight()
    {
        float height = backGroundImage.bounds.size.y-imageHeightCorrectionValue;
        return bgImageNowHeight = height * blockClimbCount.GetClimbCount() / blockClimbCount.GetGoalHeight();
    }
    public void BGDisplay()
    {
        bgImageNowHeight=bgImageOriginalHeight- GetBGHeight();
        backGroundImage.transform.position = new(backGroundImage.transform.position.x, bgImageNowHeight, backGroundImage.transform.position.z);
    }
}
