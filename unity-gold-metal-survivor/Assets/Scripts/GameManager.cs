using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //스태틱으로 선언된 변수는 인스펙터에서 감지되지 않음
    
    public float gameTime;
    public int level;
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
        if (gameTime < maxGameTime)
        {
            level = Mathf.FloorToInt(gameTime / 10f); //주어진 시간에 따라 맞춰서 레벨 상승 FloorToInt -> 소숫점 버리기
            gameTime += Time.deltaTime;
        }
        else
        {
            gameTime = maxGameTime;
        }
    }
}
