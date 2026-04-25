using UnityEngine;

// 카드 프리팹 생성
public class CardGenerator : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform handArea;
    public GameObject CreatCard()
    {
        GameObject cardUI = Instantiate(cardPrefab,handArea);
        return cardUI;
    }
}
