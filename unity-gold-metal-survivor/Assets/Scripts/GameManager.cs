using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance; //스태틱으로 선언된 변수는 인스펙터에서 감지되지 않음
    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    public int MonsterLevel;
    public bool isLive;
    [Header("# Player Info")]
    public int Playerlevel;
    public int kill;
    public int exp;
    public int[] nextExp = { 1, 2, 4, 5, 6, 7 };
    
    public int health = 10;
    public int maxHealth = 20;
    
    [Header("# Game Object")]
    public PoolManager poolManager;
    public Player player;
    public LevelUp uiLevelUp;

    void Awake()
    {
        Instance = this;
    }

    public void GameStart()
    {
        health = maxHealth;
        
        // 임시 스크립트 (첫 번째 캐릭터 기본 무기 지급)
        uiLevelUp.Select(0);
        isLive = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!isLive)
            return;
        gameTime += Time.deltaTime;
        
        if (gameTime < maxGameTime)
        {
            MonsterLevel = Mathf.FloorToInt(gameTime / 10f); //주어진 시간에 따라 맞춰서 레벨 상승 FloorToInt -> 소숫점 버리기
            gameTime += Time.deltaTime;
        }
        else
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;
        if (exp == nextExp[Playerlevel])
        {
            Playerlevel += 1;
            Playerlevel = Mathf.Min(Playerlevel , 5);
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0; //유니티 시간 속도 (배율)
    }
    
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
