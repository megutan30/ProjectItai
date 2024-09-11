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
    private float moveTime = 1;
    [SerializeField]
    private float time = 0;

    [SerializeField]
    private float imageHeightCorrectionValue;


    private Vector3 bgImageOldTF;
    private Vector3 bgImageNowTF;
    private int oldClimbCount;
    private bool isBGMove;
    // Start is called before the first frame update
    void Start()
    {
        if (blockClimbCount != null)
        {
            bgImageOldTF = backGroundImage.transform.position;
            bgImageNowTF = backGroundImage.transform.position;
        }
        else
        {
            Debug.LogWarning("BlockClimbCount‚ª‚ ‚è‚Ü‚¹‚ñB");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (oldClimbCount != blockClimbCount.GetClimbCount())
        {

            SetBGMovePos();
            isBGMove = true;
        }
        if (isBGMove)
        {
            time += Time.deltaTime / moveTime;
            backGroundImage.transform.position = Vector3.Lerp(bgImageOldTF, bgImageNowTF, time);
            if (time > 1)
            {
                time = 0;
                isBGMove = false;

            }
        }

        oldClimbCount = blockClimbCount.GetClimbCount();

    }

    public float GetBGMoveHeight()
    {
        float height = backGroundImage.bounds.size.y - imageHeightCorrectionValue;
        return height * 1f / blockClimbCount.GetGoalHeight();
    }

    public void SetBGMovePos()
    {
        bgImageOldTF = bgImageNowTF;
        float y = bgImageOldTF.y;
        bgImageNowTF = new Vector3(bgImageOldTF.x, y - GetBGMoveHeight(), bgImageOldTF.z);
    }
}
