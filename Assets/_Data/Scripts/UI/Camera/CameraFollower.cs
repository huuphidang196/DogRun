using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : StandMonoBehaviour
{
    [Header("Follow Setting")]
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);

    [Tooltip("Thời gian camera đạt tới mục tiêu")]
    [SerializeField] private float smoothTime = 0.2f;

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (PlayerCtrl.Instance == null) return;

        Vector3 targetPosition = PlayerCtrl.Instance.transform.position + offset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );
    }
}
