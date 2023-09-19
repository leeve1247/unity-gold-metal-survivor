using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per)
    {
        this.damage = damage;
        this.per = per;
    }
    
    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if (per > -1)
        {
            // Debug.Log(dir.magnitude);
            _rigidbody2D.velocity = 60 * dir;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy") || per == -1) //본인이 근접 무기거나 적에 부딪힌게 아니었을 때는 함수 실행 안함
            return;

        per--;

        if (per == -1)
        {
            _rigidbody2D.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
