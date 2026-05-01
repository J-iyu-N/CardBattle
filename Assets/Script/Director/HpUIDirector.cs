using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HpUIDirector : MonoBehaviour
{
    public PlayerCharacterController playerCharacterController;
    public GameObject hpGuage;
    public TextMeshProUGUI hpText;
    public GameObject shieldGuage;
    public TextMeshProUGUI shiledText;
    public void Start()
    {
        RefreshHP();
    }
    public void RefreshHP()
    {
        hpGuage.GetComponent<Image>().fillAmount = 
        (float)playerCharacterController.State.CurrentHp/playerCharacterController.Data.maxHP;
        hpText.text = playerCharacterController.State.CurrentHp+"/"+playerCharacterController.Data.maxHP;

        if(playerCharacterController.shield == 0)
        {
            shieldGuage.SetActive(false);
        }
        else
        {
            shieldGuage.SetActive(true);
            shieldGuage.GetComponent<Image>().fillAmount = 1f;
            shiledText.text = playerCharacterController.shield.ToString();
        }
    }
}
