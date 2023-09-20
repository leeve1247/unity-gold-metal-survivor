using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform _rectTransform;
    public Camera mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 updatedPosition = GameManager.Instance.player.transform.position;
        _rectTransform.position = mainCamera.WorldToScreenPoint(updatedPosition);
    }
}
