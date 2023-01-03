using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject hudObj;
    [SerializeField] private Image healthBar;
    [SerializeField] private PlayerClass playerClass;

    private Health playerHealth;
    private bool isPrologueEnded;
    private bool isPlayerInstantiated;

    private void Awake()
    {
        PlayerMovement.OnPlayerInstantiated += PlayerMovement_OnPlayerInstantiated;
        hudObj.SetActive(false);
    }

    private void Start()
    {
        if (StoryOverlay.IsPrologueStarted)
            StoryOverlay.OnPrologueEnded += StoryOverlay_OnPrologueEnded;
        else
            StoryOverlay_OnPrologueEnded();
    }

    private void OnDestroy()
    {
        PlayerMovement.OnPlayerInstantiated -= PlayerMovement_OnPlayerInstantiated;
        StoryOverlay.OnPrologueEnded -= StoryOverlay_OnPrologueEnded;
        if (playerHealth != null)
            playerHealth.OnPlayerHealthChanged -= PlayerHealth_OnPlayerHealthChanged;
    }

    private void Activate ()
    {
        hudObj.SetActive(true);
    }

    private void StoryOverlay_OnPrologueEnded ()
    {
        isPrologueEnded = true;
        if (isPlayerInstantiated)
            Activate();
    }

    private void PlayerMovement_OnPlayerInstantiated (Transform playerTransform)
    {
        if (playerClass == PlayerClass.Jaguar && !playerTransform.TryGetComponent(out Onca jaguar)
            || playerClass == PlayerClass.Tortoise && !playerTransform.TryGetComponent(out Tartaruga tartaruga))
            return;

        isPlayerInstantiated = true;
        if (isPrologueEnded)
            Activate();
        playerHealth = playerTransform.GetComponent<Health>();
        playerHealth.OnPlayerHealthChanged += PlayerHealth_OnPlayerHealthChanged;
        healthBar.fillAmount = playerHealth.CurrentPercent;
    }

    private void PlayerHealth_OnPlayerHealthChanged (float percent)
    {
        healthBar.fillAmount = percent;
    }
}
