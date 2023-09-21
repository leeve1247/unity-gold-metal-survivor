using System;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;
    public Gear gear;

    private Image _icon;
    private Text _textLevel;
    private Text _textName;
    private Text _textDescription;
    
    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        _icon = GetComponentsInChildren<Image>()[1];
        _icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>(); //계층구조를 따라간다... 다만, Asset 의 위치가 바뀌는 불상사가 생길수도 있으니, 인덱스보다는 음. Dictionary를 따라가고 싶다.
        _textLevel = texts[0];
        _textName = texts[1];
        _textDescription = texts[2];
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void OnEnable()
    {
        _textLevel.text = "Lv." + (level + 1);

        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
                _textDescription.text = string.Format(data.itemDesc, data.damages[level]* 100, data.counts[level]);
                break;
            case ItemData.ItemType.Range:
                _textDescription.text = string.Format(data.itemDesc, data.damages[level]* 100, data.counts[level]);
                break;
            case ItemData.ItemType.Shoe:
                _textDescription.text = string.Format(data.itemDesc, data.damages[level] * 100);
                break;
            case ItemData.ItemType.Heal:
                _textDescription.text = string.Format(data.itemDesc);
                break;
            default:
                break;
        }
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