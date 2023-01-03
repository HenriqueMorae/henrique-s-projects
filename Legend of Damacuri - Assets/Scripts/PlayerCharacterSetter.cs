using UnityEngine;

public class PlayerCharacterSetter : MonoBehaviour
{
    private static int playerNo;

    [SerializeField] private Onca onca;
    [SerializeField] private Tartaruga tartaruga;

    private void Awake()
    {
        SetPlayerCharacter();
    }

    private void SetPlayerCharacter ()
    {
        if (playerNo == 0)
        {
            onca.transform.SetParent(transform.parent);
            playerNo++;
        }
        else
        {
            tartaruga.transform.SetParent(transform.parent);
            playerNo = 0;
        }
        Destroy(gameObject);
    }
}
