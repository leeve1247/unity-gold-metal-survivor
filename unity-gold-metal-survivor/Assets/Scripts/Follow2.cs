using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow2 : MonoBehaviour
{
    private RectTransform image;
    public Player player;
    // public Vector3 myVector3;

    private void Start()
    {
        image = GetComponent<RectTransform>();
        
    }

    void Update()
    {
        Vector3 adfa = Camera.main.WorldToScreenPoint(player.transform.position + new Vector3(0, -1.0f, 0));
        image.position = adfa;
    }
}
