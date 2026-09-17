using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScanWave : MonoBehaviour
{
    [SerializeField]
    private Transform waveSphere;

    [SerializeField]
    private LayerMask scrapLayer;

    private float horizontalScanAngle = 90.0f;

    private float verticalScanAngle = 60.0f;

    [SerializeField]
    private float maxRadius = 15.0f;

    [SerializeField]
    private float duration = 1.0f;

    private Coroutine scanCoroutine;

    private Transform camTransform;

    private Vector3 scanOriginPosition;
    private Quaternion scanOriginRotation;

    private readonly Collider[] scanResults = new Collider[30];

    // 현재 스캔에서 이미 발견한 고철
    private readonly HashSet<Scrap> scannedScraps = new HashSet<Scrap>();

    private void Awake()
    {
        camTransform = Camera.main.transform;

        if (waveSphere == null)
        {
            waveSphere = GetComponentInChildren<Transform>();
        }

        waveSphere.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Mouse.current.rightButton.wasPressedThisFrame)
        {
            Play();
        }
    }

    public void Play()
    {
        if (scanCoroutine != null)
        {
            StopCoroutine(scanCoroutine);
        }

        scanCoroutine = StartCoroutine(ScanCoroutine());
    }

    private IEnumerator ScanCoroutine()
    {
        scanOriginPosition = camTransform.position;
        scanOriginRotation = camTransform.rotation;

        float elapsedTime = 0.0f;

        scannedScraps.Clear();

        waveSphere.gameObject.SetActive(true);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float ratio = elapsedTime / duration;
            float radius = Mathf.Lerp(0.0f, maxRadius, ratio);

            // 원하는 반지름의 2배를 Scale로 사용
            waveSphere.localScale = Vector3.one * radius * 2.0f;

            DetectScrap(radius);

            yield return null;
        }

        waveSphere.gameObject.SetActive(false);
        scanCoroutine = null;
    }

    private void DetectScrap(float radius)
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, radius, scanResults, scrapLayer, QueryTriggerInteraction.Collide);

        for (int i = 0; i < count; i++)
        {
            Collider target = scanResults[i];

            Scrap scrap = target.GetComponentInParent<Scrap>();

            if (scrap == null)
            {
                continue;
            }

            // 고철이 범위안에 존재하는가
            if(!IsWithinScanAngle(scrap.transform.position))
            {
                continue;
            }

            // 이미 발견했던 Scrap이면 false
            if (!scannedScraps.Add(scrap))
            {
                continue;
            }

            Debug.Log($"스캔 발견 : {scrap.name}");
        }
    }

    // 고철이 범위 안에 존재하는지 체크
    private bool IsWithinScanAngle(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - scanOriginPosition).normalized;

        Vector3 localDirection = Quaternion.Inverse(scanOriginRotation) * direction;

        // 카메라 뒤에 있는 고철은 제외
        if (localDirection.z <= 0.0f)
        {
            return false;
        }

        float horizontalAngle = Mathf.Atan2(Mathf.Abs(localDirection.x), localDirection.z) * Mathf.Rad2Deg;

        float verticalAngle = Mathf.Atan2(Mathf.Abs(localDirection.y), localDirection.z) * Mathf.Rad2Deg;

        if (horizontalAngle > horizontalScanAngle * 0.5f)
        {
            return false;
        }

        if (verticalAngle > verticalScanAngle * 0.5f)
        {
            return false;
        }

        return true;
    }
}