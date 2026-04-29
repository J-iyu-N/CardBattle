using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HpUIController : MonoBehaviour
{
    public PlayerCharacterController playerCharacterController;
    public GameObject hpGuage;
    public TextMeshProUGUI hpText;
    public void Start()
    {
        RefreshHP();
    }
    public void RefreshHP()
    {
        hpGuage.GetComponent<Image>().fillAmount = 
        (float)playerCharacterController.State.CurrentHp/playerCharacterController.Data.maxHP;
        hpText.text = playerCharacterController.State.CurrentHp+"/"+playerCharacterController.Data.maxHP;
    }
}
