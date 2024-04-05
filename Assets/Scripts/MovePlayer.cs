using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float Speed = 5f;
    public float Speed2 = 7.5f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        
        if(Input.GetKeyDown(KeyCode.Space)){
            rb.AddForce(movement * Speed2);
        }
        else{
            rb.AddForce(movement * Speed);
        }    
    }
}
