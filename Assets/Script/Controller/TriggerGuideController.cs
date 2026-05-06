using UnityEngine;
using TMPro;

public class TriggerGuideController : MonoBehaviour
{
    public string message = "문구";

    public GameObject guidePanel;
    public TextMeshProUGUI guideText;

    void Start()
    {
        if (guidePanel != null)
        {
            guidePanel.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowGuide();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideGuide();
        }
    }

    public void ShowGuide()
    {
        if (guidePanel != null)
        {
            guidePanel.SetActive(true);
        }

        if (guideText != null)
        {
            guideText.text = message;
        }
    }

    public void HideGuide()
    {
        if (guidePanel != null)
        {
            guidePanel.SetActive(false);
        }
    }
}