using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //스태틱으로 선언된 변수는 인스펙터에서 감지되지 않음
    
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    
    public PoolManager poolManager;
    public Player player;

    void Awake()
    {
        Instance = this;
    }
    
    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
}
