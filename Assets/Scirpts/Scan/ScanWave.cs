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

    [SerializeField]
    [Range(0.0f, 360.0f)]
    private float scanAngle = 120.0f;

    [SerializeField]
    private float maxRadius = 20.0f;

    [SerializeField]
    private float duration = 1.0f;

    private Coroutine scanCoroutine;

    private readonly Collider[] scanResults = new Collider[30];

    // 현재 스캔에서 이미 발견한 고철
    private readonly HashSet<Scrap> scannedScraps = new HashSet<Scrap>();

    private void Awake()
    {
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

            // 카메라 -> 고철 방향
            Vector3 driectionToScrap = (target.transform.position - Camera.main.transform.position).normalized;

            // 카메라 정면과 고철 방향 사이의 각도
            float angle = Vector3.Angle(Camera.main.transform.forward, driectionToScrap);

            // 시야각 밖이면 제외
            if(angle > scanAngle * 0.5f)
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
}