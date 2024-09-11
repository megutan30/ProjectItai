using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbController : MonoBehaviour
{
    public Animator animator; // �L�����N�^�[��Animator
    public float climbSpeed = 5f; // �o�鑬�x
    private Rigidbody2D rb2D; // �L�����N�^�[��Rigidbody2D
    private bool isRightArmTurn = true; // �ŏ��͉E�r���グ�铮�삩��n�߂�
    private KeyCode currentKey; // ���݂̃����_���L�[
    private List<KeyCode> possibleKeys = new List<KeyCode>() { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F }; // �\�ȃL�[
    private bool isAnimating = false; // �A�j���[�V���������ǂ����̃t���O

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // Rigidbody2D���擾
        SetRandomKey(); // �ŏ��̃����_���L�[��ݒ�
    }

    void Update()
    {
        // �����_���ɑI�΂ꂽ�L�[�������ꂽ���A���A�j���[�V�������Đ�����Ă��Ȃ����̂ݓ���
        if (Input.GetKeyDown(currentKey) && !isAnimating)
        {
            isAnimating = true; // �A�j���[�V�����J�n
            if (isRightArmTurn)
            {
                // �E�r���グ��A�j���[�V�������Đ�
                animator.SetTrigger("RightArm");
            }
            else
            {
                // ���r���グ��A�j���[�V�������Đ�
                animator.SetTrigger("LeftArm");
            }

            // �L�����N�^�[����Ɉړ�
            Climb();

            // �A�j���[�V�������I���̂�҂R���[�`�����J�n
            StartCoroutine(WaitForAnimation());
        }
    }

    // �����_���ȃL�[��ݒ肷�郁�\�b�h
    void SetRandomKey()
    {
        currentKey = possibleKeys[Random.Range(0, possibleKeys.Count)];
        Debug.Log("���ɉ����L�[: " + currentKey.ToString());
    }

    // �L�����N�^�[����Ɉړ������郁�\�b�h
    void Climb()
    {
        // ������Ɉړ�
        rb2D.velocity = new Vector2(rb2D.velocity.x, climbSpeed);
    }

    // �A�j���[�V�����̏I����҂R���[�`��
    IEnumerator WaitForAnimation()
    {
        // �A�j���[�V�������I������܂őҋ@
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // �A�j���[�V�������I��������A���̃����_���L�[��ҋ@����
        isAnimating = false; // �A�j���[�V�������I�������̂Ńt���O������

        // ���݂ɃA�j���[�V�������؂�ւ��
        isRightArmTurn = !isRightArmTurn;

        // ���̃����_���ȃL�[��ݒ�
        SetRandomKey();
    }
}
