using UnityEngine;
using TMPro;
using UnityEngine.UI;

// 카드 데이터 매핑
// 카드 프리팹에 코드 붙이기
public class CardUIController : MonoBehaviour
{
    public CardData cardData;
    [Header("카드 프리팹 UI 연결")]
    public Image icon;
    public TextMeshProUGUI cardName;
    //public TextMeshPro damage;
    public TextMeshProUGUI desc;

    public void FillCardData(CardData data)
    {
        cardData = data;
        cardName.text = data.cardName;
        desc.text = data.description;

        if (data.cardIcon != null)
        {
            icon = data.cardIcon;
        }
    }
}
