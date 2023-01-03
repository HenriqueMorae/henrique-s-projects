using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSetter : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        PlayerMovement.OnPlayerInstantiated += HandlePlayerInstantiated;
    }

    private void OnDestroy()
    {
        PlayerMovement.OnPlayerInstantiated -= HandlePlayerInstantiated;
    }

    private void HandlePlayerInstantiated (Transform playerTransform)
    {
        virtualCamera.Follow = playerTransform;
        virtualCamera.LookAt = playerTransform;
    }
}
