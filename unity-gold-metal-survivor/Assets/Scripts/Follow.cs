using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform rect;
    
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 worldToScreenPoint = Camera.main.WorldToScreenPoint(GameManager.Instance.player.transform.position);
        rect.position = worldToScreenPoint - new Vector3(0, 130, 0);
    }
}
