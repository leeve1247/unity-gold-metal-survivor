using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    Rigidbody2D _rigidbody2D;
    SpriteRenderer _spriteRenderer;
    Animator _anim;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     inputVec.x = Input.GetAxis("Horizontal");
    //     inputVec.y = Input.GetAxis("Vertical");
    // }

    // FixedUpdate is called independent to Frame rate.
    private void FixedUpdate()
    {
        // // 1. Force
        // _rigidbody2D.AddForce(inputVec);
        //
        // // 2. Velocity
        // _rigidbody2D.velocity = inputVec;
        
        // 3. Position
        Vector2 nextVec = inputVec * (speed * Time.fixedDeltaTime); //fixedDeltaTime is independant 
        _rigidbody2D.MovePosition(_rigidbody2D.position + nextVec);
    }
    
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    private void LateUpdate()
    {
        _anim.SetFloat("Speed", inputVec.magnitude);
        if (inputVec.x != 0)
        {
            _spriteRenderer.flipX = inputVec.x < 0;
        }
    }
}
