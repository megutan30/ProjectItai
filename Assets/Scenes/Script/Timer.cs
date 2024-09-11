using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private int minute;
    private float seconds;
    //�@�O��Update�̎��̕b��
    private float oldSeconds;
    //�@�^�C�}�[�\���p�e�L�X�g
    [SerializeField]private TextMeshProUGUI timerText;
    // �^�C�}�[�����삵�Ă��邩�ǂ���
    private bool isTimerRunning = true;

    void Start()
    {
        minute = 0;
        seconds = 0f;
        oldSeconds = 0f;
    }

    void Update()
    {
        if (isTimerRunning||GameDirector.IsGameStart) // �^�C�}�[�����쒆�Ȃ�
        {
            seconds += Time.deltaTime;
            if (seconds >= 60f)
            {
                minute++;
                seconds = seconds - 60;
            }
            //�@�l���ς�����������e�L�X�gUI���X�V
            if ((int)seconds != (int)oldSeconds)
            {
                timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
            }
            oldSeconds = seconds;
        }
    }

    // �^�C�}�[���~���郁�\�b�h
    public void StopTimer()
    {
        isTimerRunning = false;
    }

    // �ŏI�I�Ȏ��Ԃ��擾���郁�\�b�h
    public string GetFinalTime()
    {
        return minute.ToString("00") + ":" + ((int)seconds).ToString("00");
    }
}
