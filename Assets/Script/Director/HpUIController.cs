using UnityEngine;
using UnityEngine.UI;
public class HpUIController : MonoBehaviour
{
    public PlayerCharacterController playerCharacterController;
    public GameObject hpGuage;
    public void RefreshHP()
    {
        hpGuage.GetComponent<Image>().fillAmount = 
        (float)playerCharacterController.State.CurrentHp/playerCharacterController.Data.maxHP;
    }
}
