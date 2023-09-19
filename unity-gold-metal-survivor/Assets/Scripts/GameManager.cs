using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //스태틱으로 선언된 변수는 인스펙터에서 감지되지 않음
    public Player player;

    void Awake()
    {
        instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
