using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        if (hor != 0)
        {
            transform.position += Vector3.right * hor * speed * Time.deltaTime;
        }
        if (ver != 0)
        {
            transform.position += Vector3.up * ver * speed * Time.deltaTime;
        }
    }
}
