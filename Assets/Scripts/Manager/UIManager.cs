using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Gameplay UI")]
    [SerializeField] private Slider rockSlider;
    //[SerializeField] private TextMeshProUGUI rockText;
    [SerializeField] private TextMeshProUGUI timeText;

    [Header("Result UI")]
    public TotalPanel totalPanel;

    private void Awake()
    {
        Instance = this;
    }

    public void Init(int rockLimit, float levelTime)
    {
        // Rock Slider
        rockSlider.minValue = 0;
        rockSlider.maxValue = rockLimit;
        rockSlider.value = 0;

        UpdateRockText(0, rockLimit);

        // Timer
        UpdateTime(levelTime);
    }

    public void UpdateRock(int currentRock)
    {
        rockSlider.value = currentRock;
        UpdateRockText(currentRock, (int)rockSlider.maxValue);
    }

    private void UpdateRockText(int current, int max)
    {
        //rockText.text = current + " / " + max;
    }

    public void UpdateTime(float time)
    {
        // Không cho số âm
        time = Mathf.Max(0f, time);

        // Ví dụ: 14, 13, 12...
        timeText.text = Mathf.CeilToInt(time).ToString();

        // Nếu muốn format 00:15 thì dùng:
        // int seconds = Mathf.CeilToInt(time);
        // timeText.text = "00:" + seconds.ToString("00");
    }

    public void Show(bool isWin)
    {
        totalPanel.gameObject.SetActive(true);

        if (isWin)
        {
            totalPanel.text.text = "Win";
        }
        else
        {
            totalPanel.text.text = "Lose";
        }
    }

    public void LoadingScene()
    {
        Debug.Log("Click");
        SceneManager.LoadScene("GamePlay");
    }
}

