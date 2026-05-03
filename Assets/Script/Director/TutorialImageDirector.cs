using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialImageDirector : MonoBehaviour
{
    // 이미지 튜토리얼
    public Image tutorialImage;
    public Sprite[] tutorialSprites;

    public int pageIndex = 0;

     void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(pageIndex <= 0) return;

            pageIndex--;
            tutorialImage.sprite = tutorialSprites[pageIndex];
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(pageIndex >= tutorialSprites.Length - 1) return; // 마지막장

            pageIndex++;
            tutorialImage.sprite = tutorialSprites[pageIndex];
        }

        // 마지막 장에서 씬이동
        if(pageIndex == tutorialSprites.Length - 1)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("BattleScene");
            }
        }
    }
}