using System;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health}
    public InfoType type;

    private Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
        
    }

    private void LateUpdate()
    {
        switch (type) //보통 협업할 때는 오타나거나 헷갈리지 말라고 열거형을 많이 쓴다.
        {
            case InfoType.Exp:
                var curExp = GameManager.Instance.exp;
                var maxExp = GameManager.Instance.nextExp[GameManager.Instance.Playerlevel];
                mySlider.value = curExp / maxExp;
                break;
            case InfoType.Level:
                break;
            case InfoType.Kill:
                break;
            case InfoType.Time:
                break;
            case InfoType.Health:
                break;
        }
    }
}
