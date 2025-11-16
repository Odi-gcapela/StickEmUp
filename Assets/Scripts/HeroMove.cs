using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMove : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 InputKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputKey = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));    
    }

    void FixedUpdate()
    {
        rb.MovePosition((Vector3) transform.position + InputKey * 10 * Time.deltaTime);

        float Angle = Mathf.Atan2(InputKey.x, InputKey.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, Angle, 0);
    }
}

