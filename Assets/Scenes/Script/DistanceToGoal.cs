using UnityEngine;
using UnityEngine.UI;

public class DistanceToGoal : MonoBehaviour
{
    public Transform player; // �v���C���[��Transform
    public Transform goalYPosition ; // �S�[����Y���W
    public Text distanceText; // ������\������UI�e�L�X�g

    void Update()
    {
        // ���݂�Y���W���擾
        float currentYPosition = player.position.y;

        // �S�[���܂ł̋������v�Z
        float distanceToGoal = goalYPosition.position.y - currentYPosition;

        // UI�ɕ\��
        distanceText.text = "�S�[���܂ł̋���: " + distanceToGoal.ToString("F2") + "m";
    }
}
