using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemey : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive = true;

    Rigidbody2D _rigidbody;
    Animator _anim;
    SpriteRenderer _sprite;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        speed = 2.5f;
    }

    // When Enemy Enabled and Active
    private void OnEnable()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData spawnData)
    {
        _anim.runtimeAnimatorController = animCon[spawnData.spriteType];
        speed = spawnData.speed;
        maxHealth = spawnData.maxHealth;
        health = spawnData.maxHealth;
    }

    private void FixedUpdate()
    {
        if(!isLive)
            return;
        Vector2 dirVec = target.position - _rigidbody.position;
        Vector2 nextVec = dirVec.normalized * (speed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(_rigidbody.position + nextVec);
        _rigidbody.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if(!isLive)
            return;
        _sprite.flipX = target.position.x < _rigidbody.position.x;
    }
}
