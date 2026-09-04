using TMPro;
using UnityEngine;


using TuanBowFramework.UI;
public class ResultUIView : UIView
{
    public TextMeshProUGUI text;
    private void OnEnable()
    {
        EventBus.OnEndGame += ShowEndGame;
    }

    private void OnDisable()
    {
        EventBus.OnEndGame -= ShowEndGame;
    }

    public void ShowEndGame(bool isWin)
    {
        Show();
        if (isWin)
        {
            text.text = "Win";
        }
        else
        {
            text.text = "Lose";
        }
    }

    public void PlayAgain()
    {
        EventBus.ResetGame?.Invoke();
    }
}
