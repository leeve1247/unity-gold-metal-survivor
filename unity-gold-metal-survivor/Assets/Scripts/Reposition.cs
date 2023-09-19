using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    //On TriggerExit2D -> 그만 겹쳐졌을 때, 해제되는 것
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) // 트리거된 존재가 Area가 아니면 Skip
            return;
        Vector3 playerPos = GameManager.instance.player.transform.position; //플레이어 위치
        Vector3 myPos = transform.position; //
        
        float diffx = Mathf.Abs(playerPos.x - myPos.x);
        float diffy = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.inputVec;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffx >= diffy)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffx < diffy)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
            
            case "Enemy":
                
                break;
        }
    }
}
