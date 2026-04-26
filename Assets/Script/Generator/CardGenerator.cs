using UnityEngine;

// 카드 프리팹 생성
public class CardGenerator : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform handArea;
    public HandController handController;
    float cardSpacing = 220f;

    public CardData testCardData;
    public void Start()
    {
        CreatCard(testCardData);
    }
    public void CreatCard(CardData data)
    {
        int count = handController.Hand.Count;
        float totalWidth = (count-1)*cardSpacing; // 전체 가로 길이
        // 맨 왼쪽 위치 = 전체의 절반만큼 왼쪽으로 이동
        float startX = -totalWidth/2f;

        for(int i =0; i<count; i++)
        {
            GameObject card = Instantiate(cardPrefab,handArea);
            RectTransform transform = card.GetComponent<RectTransform>();
            transform.anchoredPosition = new Vector2(startX+(cardSpacing*i),-360);

            // 카드 data 매핑
            CardUIController cardUIController = card.GetComponent<CardUIController>();
            cardUIController.FillCardData(data);
        }
    }
}
