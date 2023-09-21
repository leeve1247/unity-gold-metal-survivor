using System;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;

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
                break;
            case ItemData.ItemType.Range:
                break;
            case ItemData.ItemType.Glove:
                break;
            case ItemData.ItemType.Shoe:
                break;
            case ItemData.ItemType.Heal:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        level++;

        if (level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false; //레벨이 데이터 데미지 목록이 넘어가면(레벨 5에서 6이 되지 않게끔)
        }

    }
}