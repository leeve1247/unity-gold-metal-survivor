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

    private void Awake()
    {
        //Object Pooling
        pools = new List<GameObject>[prefebs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
        
        Debug.Log(pools.Length);
    }

    //index 번째 풀안에 있는 오브젝트를 가져오는 것
    public GameObject Get(int index)
    {
        GameObject select = null;
        // .... access to idle game object
        foreach (var item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = null;
                item.SetActive(true);;
                break;
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
