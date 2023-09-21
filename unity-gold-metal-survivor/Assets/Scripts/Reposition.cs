using UnityEngine;

public class Reposition : MonoBehaviour
{
    private Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    //On TriggerExit2D -> 그만 겹쳐졌을 때, 해제되는 것
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) // 트리거된 존재가 Area가 아니면 Skip
            return;
        Vector3 playerPos = GameManager.Instance.player.transform.position; //플레이어 위치
        Vector3 myPos = transform.position; //

        switch (transform.tag)
        {
            case "Ground":
                float diffx = playerPos.x - myPos.x;
                float diffy = playerPos.y - myPos.y;
                float dirX = diffx < 0 ? -1 : 1;
                float dirY = diffy < 0 ? -1 : 1;
                diffx = Mathf.Abs(diffx);
                diffy = Mathf.Abs(diffy);
                
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
                if (coll.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
                    transform.Translate(
                        ran + dist * 2
                    );
                }
                break;
        }
    }
}
