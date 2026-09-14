using UnityEngine;

public class PlayerArmsController : MonoBehaviour
{
    private const string ARMS_TRANSFORM = "spine_003";

    [SerializeField]
    private CameraMove cameraMove;

    [SerializeField]
    private Transform armsTransform;

    private Vector3 originRotation;

    private void Awake()
    {
        if (cameraMove == null)
        {
            cameraMove = Camera.main.GetComponent<CameraMove>();
        }

        if(armsTransform == null)
        {
            armsTransform = transform.Find(ARMS_TRANSFORM);
        }

        originRotation = armsTransform.localRotation.eulerAngles;
    }

    // 팔을 카메라 회전에 맞게 회전해 안보이도록 구현
    private void LateUpdate()
    {
        Vector3 curRotation = originRotation + new Vector3(cameraMove.Pitch, 0.0f, 0.0f);

        armsTransform.localRotation = Quaternion.Euler(curRotation);
    }
}