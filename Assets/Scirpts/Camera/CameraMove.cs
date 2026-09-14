using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    private const string CAMERA_POS_NAME = "CameraPos";

    private const float MAX_ROTATION = 89.0f; 
    private const float MIN_ROTATION = -89.0f;
   
    [SerializeField]
    private Transform playerTransform;

    private Transform cameraPos;

    private float mouseSensitivity = 0.1f;

    private float pitch = 0.0f;
    private float yaw = 0.0f;
    public float Yaw => yaw;
    public float Pitch => pitch;

    private void Awake()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag(PLAYER_TAG).transform;
        }

        cameraPos = playerTransform.Find(CAMERA_POS_NAME);
    }

    private void Update()
    {
        CameraRotation();
    }

    private void LateUpdate()
    {
        transform.position = cameraPos.position;
    }
    
    // 카메라를 통한 상하좌우 회전
    private void CameraRotation()
    {
        if (Mouse.current == null) return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        yaw += mouseDelta.x * mouseSensitivity;
        pitch -= mouseDelta.y * mouseSensitivity;

        pitch = Mathf.Clamp(pitch, MIN_ROTATION, MAX_ROTATION);

        transform.localRotation = Quaternion.Euler(pitch, yaw, 0.0f);
    }
}