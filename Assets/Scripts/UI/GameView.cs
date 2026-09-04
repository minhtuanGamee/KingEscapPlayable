using TMPro;
using TuanBowFramework.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameView : UIView
{

    [Header("Gameplay UI")]
    [SerializeField] private Slider rockSlider;
    //[SerializeField] private TextMeshProUGUI rockText;
    [SerializeField] private TextMeshProUGUI timeText;

    private void OnEnable()
    {
        EventBus.OnRockCountChanged += UpdateRock;
        EventBus.OnTimeChanged += UpdateTime;
        EventBus.OnEndGame += EndView;
        EventBus.ResetGame += Show;
    }

    private void OnDisable()
    {
        EventBus.OnRockCountChanged -= UpdateRock;
        EventBus.OnTimeChanged -= UpdateTime;
        EventBus.OnEndGame -= EndView;
        EventBus.ResetGame -= Show;
    }
    protected override void Start()
    {
        base.Start();

        Init(LevelManager.Instance.RockLimit);
    }
    public void Init(int rockLimit)
    {
        // Rock Slider
        rockSlider.minValue = 0;
        rockSlider.maxValue = rockLimit;
        rockSlider.value = 0;

        UpdateRockText(0, rockLimit);

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

    public void EndView(bool isWin)
    {
        Hide();
    }
}

