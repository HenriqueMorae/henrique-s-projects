using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Obsolete("This was not working properly. The player is spawned when a gamepad button is pressed, instead.")]
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private PlayerInputManager inputManager;
    [SerializeField] private GameObject playerRootPrefab;

    private int playerNo;

    private void Awake()
    {
        inputManager.playerPrefab = playerRootPrefab;
        inputManager.JoinPlayer(playerNo, playerNo);
        playerNo++;
        inputManager.JoinPlayer(playerNo, playerNo);
    }
}
