using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // .. 프리펩 보관할 변수
    public GameObject[] prefebs;
    
    // .. 풀 담당을 하는 리스트
    List<GameObject>[] pools;

    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        //Object Pooling
        pools = new List<GameObject>[prefebs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
        
        // Debug.Log(pools.Length);
    }

    //index 번째 풀안에 있는 오브젝트를 가져오는 함수
    public GameObject get(int index)
    {
        GameObject select = null;
        // .... access to idle game object
        foreach (var item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                item.SetActive(true);
                return select;
            }
        }

        if (!select)
        {
            select = Instantiate(prefebs[index], transform);
            pools[index].Add(select);
        }
        return select;
    }
}
