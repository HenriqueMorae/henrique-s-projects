using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    [SerializeField] private float showTime;
    [SerializeField] private GameObject tutorialTextObj;

    private void Start()
    {
        if (StoryOverlay.IsPrologueStarted)
            StoryOverlay.OnPrologueEnded += BeginTutorialShow;
        else
            BeginTutorialShow();
    }

    private void OnDestroy()
    {
        StoryOverlay.OnPrologueEnded -= BeginTutorialShow;
    }

    private void BeginTutorialShow ()
    {
        tutorialTextObj.SetActive(true);
        Invoke(nameof(EndTutorialShow), showTime);
    }

    private void EndTutorialShow ()
    {
        tutorialTextObj.SetActive(false);
    }
}
