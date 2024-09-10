using UnityEngine;
using UnityEngine.UI;

public class DistanceToGoal : MonoBehaviour
{
    public Transform player; // プレイヤーのTransform
    public Transform goalYPosition ; // ゴールのY座標
    public Text distanceText; // 距離を表示するUIテキスト

    void Update()
    {
        // 現在のY座標を取得
        float currentYPosition = player.position.y;

        // ゴールまでの距離を計算
        float distanceToGoal = goalYPosition.position.y - currentYPosition;

        // UIに表示
        distanceText.text = "ゴールまでの距離: " + distanceToGoal.ToString("F2") + "m";
    }
}
