using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHorizontalRotate : MonoBehaviour
{
    [SerializeField]
    private CameraMove cameraMove;

    private void Awake()
    {
        if (cameraMove == null)
        {
            cameraMove = Camera.main.GetComponent<CameraMove>();
        }
    }

    private void Update()
    {
        PlayerHorizontalRotation();
    }

    // 플레이어 좌우 회전
    private void PlayerHorizontalRotation()
    {
        if (cameraMove == null) return;

        transform.localRotation = Quaternion.Euler(0.0f, cameraMove.Yaw, 0.0f);
    }
}