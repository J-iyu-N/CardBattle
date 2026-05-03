using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogueDirector : MonoBehaviour
{
    public GameObject tutorialPanel;
    public RectTransform tutorialPanelRect;
    public TextMeshProUGUI tutorialText;

    [TextArea]
    public string[] messages;

    public Vector2[] panelPositions;

    public string nextSceneName = "BattleScene";

    private int currentIndex;

    void Start()
    {
        currentIndex = 0;

        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(true);
        }

        ShowCurrentStep();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            NextStep();
        }
    }

    public void ShowCurrentStep()
    {
        if (tutorialText != null && messages != null && messages.Length > 0)
        {
            tutorialText.text = messages[currentIndex];
        }

        if (tutorialPanelRect != null && panelPositions != null && currentIndex < panelPositions.Length)
        {
            tutorialPanelRect.anchoredPosition = panelPositions[currentIndex];
        }
    }

    public void NextStep()
    {
        currentIndex += 1;

        if (currentIndex >= messages.Length)
        {
            SceneManager.LoadScene(nextSceneName);
            return;
        }

        ShowCurrentStep();
    }
}