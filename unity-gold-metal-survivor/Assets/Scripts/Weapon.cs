using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;
    
    void Start()
    {
        Init();
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * (speed * Time.deltaTime));
                break;
            default:
                break;
        }

        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
        }
    }

    void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Batch();
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = -150; //이건 뭐지
                Batch();
                break;
            default:
                break;
        }
    }

    void Batch() // 생성 무기를 배치하는 함수 호출
    {
        for (int index = 0; index < count; index++)
        {
            Transform bulletTransform;

            if (index < transform.childCount) //이미 있다손 싶으면
            {
                bulletTransform = transform.GetChild(index);
            }
            else
            {
                bulletTransform = GameManager.Instance.poolManager.get(prefabId).transform;
            }
            
            bulletTransform.parent = transform; //부모를 나 자신으로 할당

            bulletTransform.localPosition = Vector3.zero;
            bulletTransform.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * (360 * index) / count;
            bulletTransform.Rotate(rotVec);
            bulletTransform.Translate(bulletTransform.up * 1.5f, Space.World);
            bulletTransform.GetComponent<Bullet>().Init(damage, -1); // -1 은 방어 무시
            
        }
        
    }
}
