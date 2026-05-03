using UnityEngine;

public class BasicCardGenerator : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform handArea;

    public GameObject CreateOneCardUI()
    {
        GameObject card = Instantiate(cardPrefab, handArea);
        return card;
    }
}