using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput * speed, rigid.velocity.y);
        rigid.velocity = movement;
    }
}
