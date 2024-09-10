using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class GetKey : MonoBehaviour
{
    // Start is called before the first frame update
    // ŽlŽˆ‚Ì“Í‚­‹——£
    public float extremitiesLength;
    CircleCollider2D cc;
    public GameObject extremityObj;


    public List<GameObject> canGetObj;
    public List<KeyCode> canDownKey;
    float t = 0;


    void Start()
    {
        cc = GetComponent<CircleCollider2D>();
        cc.radius = extremitiesLength;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.GetComponent<HasKey>() == null)
            return;
        KeyCode key = obj.GetComponent<HasKey>().GetkeyCode();
        canGetObj.Add(obj);
        canDownKey.Add(key);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.GetComponent<HasKey>() == null)
            return;
        KeyCode key = obj.GetComponent<HasKey>().GetkeyCode();
        foreach (GameObject obj2 in canGetObj)
        {
            if (obj2 == obj)
                return;

        }
        canGetObj.Add(obj);
        canDownKey.Add(key);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.GetComponent<HasKey>() == null)
            return;
        KeyCode key = obj.GetComponent<HasKey>().GetkeyCode();

        canGetObj.Remove(obj);
        canDownKey.Remove(key);
    }
    // Update is called once per frame
    void Update()
    {
    
        t += Time.deltaTime;
        foreach (KeyCode key in canDownKey)
        {
            if(Input.GetKeyDown(key))
            {
                t = 0;
            }
            if (Input.GetKey(key))
            {
                print(key);
                GameObject obj = canGetObj[canDownKey.IndexOf(key)];
                extremityObj.transform.position = Vector3.Lerp(extremityObj.transform.position, obj.transform.position,t);
            }


        }
    }
}
