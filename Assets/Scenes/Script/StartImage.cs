using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartImage : MonoBehaviour
{

    public Image imageToShow;
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;

        button.onClick.AddListener(() =>
        {
            imageToShow.gameObject.SetActive(false);
            Time.timeScale = 1f;
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
