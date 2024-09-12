using UnityEngine;

public class BackGroundMovement : MonoBehaviour
{
    [SerializeField] private BlockClimbCount blockClimbCount;
    [SerializeField] private SpriteRenderer backGroundImage;
    [SerializeField] private float moveTime = 1;
    public AnimationCurve curve;
    [SerializeField] private float imageHeightCorrectionValue;

    private Vector3 initialPosition;
    private Vector3 bgImageOldTF;
    private Vector3 bgImageNowTF;
    private int oldClimbCount;
    private bool isBGMove;
    private float time = 0;

    void Start()
    {
        if (blockClimbCount != null && backGroundImage != null)
        {
            initialPosition = backGroundImage.transform.position;
            bgImageOldTF = initialPosition;
            bgImageNowTF = initialPosition;
        }
        else
        {
            Debug.LogWarning("BlockClimbCount‚Ü‚½‚ÍbackGroundImage‚ª‚ ‚è‚Ü‚¹‚ñB");
        }
    }

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
            backGroundImage.transform.position = Vector3.Lerp(bgImageOldTF, bgImageNowTF, curve.Evaluate(time));
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

    public void ResetPosition()
    {
        backGroundImage.transform.position = initialPosition;
        bgImageOldTF = initialPosition;
        bgImageNowTF = initialPosition;
        time = 0;
        isBGMove = false;
        oldClimbCount = 0;
    }
}