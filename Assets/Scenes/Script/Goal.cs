using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    public Image imageToShow; // �S�[�����ɕ\������C���[�W
    public Text finalTimeText; // �ŏI���Ԃ�\������e�L�X�g
    public Timer timer; // �^�C�}�[�̎Q��

    void Start()
    {
        imageToShow.gameObject.SetActive(false); // ������ԂŔ�\��
        finalTimeText.gameObject.SetActive(false); // �^�C�}�[����\��
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // �S�[�����ɃC���[�W��\��
            imageToShow.gameObject.SetActive(true);

            // �^�C�}�[���~
            timer.StopTimer();

            // �ŏI�^�C�����擾���ĕ\��
            string finalTime = timer.GetFinalTime();
            finalTimeText.text = "Time: " + finalTime;
            finalTimeText.gameObject.SetActive(true);

            Debug.Log("GameClear");

            // �Q�[���̓������~�߂�
            Time.timeScale = 0f;
        }
    }
}
