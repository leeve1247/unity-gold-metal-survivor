using System;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;
    private float timer;
    private Player player;

    private void Awake()
    {
        player = GameManager.Instance.player;
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * (speed * Time.deltaTime));
                break;
            default:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    // Debug.Log("발사!");
                    timer = 0f;
                    Fire();
                }
                break;
        }

        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
        }
    }

    private void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = (targetPos - transform.position).normalized;
        
        Transform bullet =  GameManager.Instance.poolManager.get(prefabId).transform;
        // bullet.parent = transform; //불러온 Bullet을 Weapon 자식으로 할당
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage,count,dir); // 이친구는 Weapon이 아니라, 생성된 Bullet에서 처리하는게 옳다
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Batch();

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform; // 뭔가 너저분하군
        transform.localPosition = Vector3.zero;
        
        // Property Set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        for (int index = 0; index < GameManager.Instance.poolManager.prefebs.Length; index++)
        {
            if (data.projectile == GameManager.Instance.poolManager.prefebs[index])
            {
                prefabId = index;
                break;
            }
        }
        
        switch (id)
        {
            case 0: // 삽이라는 건데... 흠............ 이렇게 한다고?
                speed = -150; // 회전 속도
                Batch();
                break;
            default:
                speed = 0.3f; // 연사 속도
                break;
        }
        
        // Hand Set
        // Debug.Log(data.itemType);

        Hand hand = null; 
        //근접무기일 경우
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
                hand = player.hands.FirstOrDefault(item => item.name == "HandRight");
                break;
            case ItemData.ItemType.Range:
                hand = player.hands.FirstOrDefault(item => item.name == "HandLeft");
                break;
            default:
                break;
        }
        if (hand == null)
            return;
        hand.spriter.sprite = data.hand;
        hand.gameObject.SetActive(true);
        
    }
    
    void Batch() // 생성 무기를 배치하는 함수 호출
    {
        for (int index = 0; index < count; index++)
        {
            Transform bulletTransform;

            if (index < transform.childCount) //총알이 이미 있다손 싶으면
            {
                bulletTransform = transform.GetChild(index);
            }
            else
            {
                bulletTransform = GameManager.Instance.poolManager.get(prefabId).transform;
                bulletTransform.parent = transform; //불러온 Bullet을 Weapon 자식으로 할당
            }

            bulletTransform.localPosition = Vector3.zero;
            bulletTransform.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * (360 * index) / count;
            bulletTransform.Rotate(rotVec);
            bulletTransform.Translate(bulletTransform.up * 1.5f, Space.World);
            bulletTransform.GetComponent<Bullet>().Init(damage, -1); // -1 은 방어 무시
        }
    }
}
