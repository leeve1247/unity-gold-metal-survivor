using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{

    public Transform[] spawnPoint;
    public SpawnData[] spawnDatas;
    

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    float _timer; //Update 용 변수
    void Update()
    {
        _timer += Time.deltaTime;

        // if (_timer > (_level == 0 ? 0.5f : 0.2f))
        
        if (_timer > spawnDatas[GameManager.Instance.MonsterLevel].spawnTime)
        {
            _timer = 0;
            Spawn();
            
        }
    }

    private void Spawn()
    {
        // var enemy = GameManager.instance.pool.Get(_level);
        var enemy = GameManager.Instance.poolManager.get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemey>().Init(spawnDatas[GameManager.Instance.MonsterLevel]);
    }
}

[System.Serializable] //직렬화가 된다.
public class SpawnData
{
    public float spawnTime; //used in Spawner
    
    public int spriteType; //used in enemy
    public int maxHealth;  //used in enemy
    public float speed; //used in enemy
}