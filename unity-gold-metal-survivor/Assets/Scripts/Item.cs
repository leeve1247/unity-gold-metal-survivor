using System;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;
    public Gear gear;

    Image icon;
    private Text textLevel;
    
    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        
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
        textLevel.text = "Lv." + (level + 1);
    }

    public void OnClick()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }
                level++;
                break;
            case ItemData.ItemType.Range:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }
                else
                {   // 레벨 2 부터 할당인데.. 이게 왔다갔다 하는 구먼. 아키텍처가 심히 Complex 하여 관리하기 매우 골룸함
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;
                    
                    nextDamage += data.baseDamage * data.damages[level];
                    nextCount += data.counts[level];

                    weapon.LevelUp(nextDamage, nextCount);
                    // Debug.Log(weapon.damage);
                    
                }
                level++;
                break;
            case ItemData.ItemType.Glove:
                break;
            case ItemData.ItemType.Shoe:
                if (level == 0)
                {
                    GameObject newGaer = new GameObject();
                    gear = newGaer.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }
                break;
            case ItemData.ItemType.Heal:
                GameManager.Instance.health = GameManager.Instance.maxHealth;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        if (level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false; //레벨이 데이터 데미지 목록이 넘어가면(레벨 5에서 6이 되지 않게끔)
        }

    }
}