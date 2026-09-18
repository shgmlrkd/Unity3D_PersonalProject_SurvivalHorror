using DG.Tweening;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    private Transform camTransform;

    [SerializeField]
    private float moveDuration = 0.3f;

    [SerializeField]
    private Ease moveEase = Ease.InOutSine;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private Sequence moveSequence;

    private void Awake()
    {
        camTransform = Camera.main.transform;
    }

    public void SaveCurrentTransform()
    {
        originalPosition = camTransform.position;
        originalRotation = camTransform.rotation;
    }

    public void MoveTo(Transform target, TweenCallback onComplete)
    {
        KillCurrentTween();

        moveSequence = DOTween.Sequence();

        moveSequence.Join(camTransform.DOMove(target.position, moveDuration));

        moveSequence.Join(camTransform.DORotateQuaternion(target.rotation, moveDuration));

        moveSequence.SetEase(moveEase).OnComplete(onComplete);
    }

    public void Restore(TweenCallback onComplete)
    {
        KillCurrentTween();

        moveSequence = DOTween.Sequence();

        moveSequence.Join(camTransform.DOMove(originalPosition, moveDuration));

        moveSequence.Join(camTransform.DORotateQuaternion(originalRotation, moveDuration));

        moveSequence.SetEase(moveEase).OnComplete(onComplete);
    }

    private void KillCurrentTween()
    {
        if (moveSequence != null)
        {
            moveSequence.Kill();
            moveSequence = null;
        }
    }

    private void OnDestroy()
    {
        KillCurrentTween();
    }
}