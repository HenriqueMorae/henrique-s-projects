using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryOverlay : MonoBehaviour
{
    public static event Action OnPrologueEnded;

    public static bool IsPrologueStarted;

    [SerializeField] private float timeToTurnOn = 4;
    [SerializeField] private GameObject prologueObj;
    [SerializeField] private GameObject pressKeyTextObj;
    [SerializeField] private GameObject part1;
    [SerializeField] private GameObject part2;
    [SerializeField] private GameObject inimigos;
    [SerializeField] private GameObject onca;

    bool fim;

    private void Awake()
    {
        IsPrologueStarted = true;
    }

    private void Start()
    {
        prologueObj.SetActive(true);
        pressKeyTextObj.SetActive(false);
        part1.SetActive(true);
        part2.SetActive(false);
        Invoke(nameof(TurnOnPressKeyObj), timeToTurnOn);
    }

    public void Update ()
    {
        if (Input.anyKeyDown)
        {
            if (part2.activeSelf)
            {
                prologueObj.SetActive(false);
                inimigos.SetActive(true);
                onca.SetActive(true);
                OnPrologueEnded?.Invoke();
                Destroy(this);
            }
            else
                Next();
            
            if (fim) SceneManager.LoadScene("MenuScene");
        }
    }
    private void TurnOnPressKeyObj ()
    {
        pressKeyTextObj.SetActive(true);
    }

    public void Final() {
        fim = true;
    }

    private void Next ()
    {
        part1.SetActive(false);
        part2.SetActive(true);
    }
}
