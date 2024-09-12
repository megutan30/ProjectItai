using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;


public class StartImage : MonoBehaviour
{

    public  GameObject imageToShow;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayBGM(SoundManager.SoundType.MainTheme);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameDirector.IsGameStart) return;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            imageToShow.gameObject.SetActive(false);
            GameDirector.IsGameStart = true;
        }
    }
}
