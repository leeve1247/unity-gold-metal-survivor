using UnityEngine;

public class Follow : MonoBehaviour
{
    private RectTransform _rect;
    public Camera mainCamera;
    // public Vector3 myVector3;

    private void Start()
    {
        _rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        _rect.position = mainCamera.WorldToScreenPoint(GameManager.Instance.player.transform.position + new Vector3(0, -1.0f, 0));
    }
}
