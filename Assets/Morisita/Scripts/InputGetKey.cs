using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class InputGetKey : MonoBehaviour
{
    [SerializeField]
    List<KeyCode> rnKeyCodes = new List<KeyCode>{ KeyCode.Q, KeyCode.W,KeyCode.E,KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P,
                              KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L,
                              KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M,
                              KeyCode.Alpha0,KeyCode.Alpha1,KeyCode.Alpha2,KeyCode.Alpha3,KeyCode.Alpha4,KeyCode.Alpha5,KeyCode.Alpha6,KeyCode.Alpha7,KeyCode.Alpha8,KeyCode.Alpha9};
    [SerializeField]
    List<KeyCode> rmdKeyCodes;

    const int kaburiNum = 8;

    public List<KeyCode> inputKeys;
    public bool[] isKeyDown = new bool[4];
    public bool isAllKeyDown = true;
    // Start is called before the first frame update
    void Start()
    {
        InitKeySet();
    }

    // Update is called once per frame
    void Update()
    {
        BoolReset();
        isAllKeyDown = true;
        int i = 0;
        foreach (KeyCode c in inputKeys)
        {
            if (Input.GetKey(c))
            {
                isKeyDown[i] = true;
            }
            else
            {
               // print(c + ":no");
            }
            i++;
        }

        foreach (bool ikd in isKeyDown)
        {
            if (!ikd)
                isAllKeyDown = false;
        }

        if (isAllKeyDown)
        {
            KeyChange();
        }

    }

    public void BoolReset()
    {
        for (int i = 0; i < isKeyDown.Length; i++)
        {
            isKeyDown[i] = false;
        }
        isAllKeyDown = false;
    }
    public void InitKeySet()
    {
        for (int i = 0; i < kaburiNum; i++)
        {
            int rnd = Random.Range(0, rnKeyCodes.Count);
            KeyCode key = rnKeyCodes[rnd];
            rnKeyCodes.RemoveAt(rnd);

            if (i < 4)
                inputKeys.Add(key);

            rmdKeyCodes.Add(key);
        }

    }
    public void KeyChange()
    {
        inputKeys.RemoveAt(0);
        int rnd = Random.Range(0, rnKeyCodes.Count);
        KeyCode key = rnKeyCodes[rnd];

        rnKeyCodes.RemoveAt(rnd);
        rnKeyCodes.Add(rmdKeyCodes[0]);

        rmdKeyCodes.RemoveAt(0);
        rmdKeyCodes.Add(key);

        inputKeys.Add(key);
    }
}
