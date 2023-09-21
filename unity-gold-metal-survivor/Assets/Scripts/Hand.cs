using System;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;

    private SpriteRenderer player;

    Vector3 leftPos = new Vector3(0.45f, -0.15f, 0);
    Vector3 leftPosReverse = new Vector3(-0.45f, -0.15f, 0);
    
    Vector3 rightPos = new Vector3(-0.25f, -0.35f, 0);
    Vector3 rightPosReverse = new Vector3(0.25f, -0.35f, 0);
    Quaternion rightRot = Quaternion.Euler(0, 0, -35);
    Quaternion rightRotReverse = Quaternion.Euler(0, 0, 35);
    
    
    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        bool isReverse = player.flipX;
        if (isLeft) // 원거리무기
        {
            transform.localPosition = isReverse ? leftPosReverse : leftPos; // localPosition은 상대적 위치를 나타낸다.
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6; //왼손 오른손 전환 
        }
        else // 근접 무기
        {
            transform.localRotation = isReverse ? rightRotReverse : rightRot;
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4; //
        }
    }
}