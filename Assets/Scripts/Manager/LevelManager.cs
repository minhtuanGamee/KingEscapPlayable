using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private float LevelTime = 15f;
    [SerializeField] private int RockCount = 0;
    public int RockLimit = 5;

    private GameState gameState;

    public float CurrentTime => LevelTime;

    public int CurrentRockCount => RockCount;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        EventBus.OnRockReachedEnd += AddRock;
        EventBus.ResetGame += ResetLevel;
    }

    private void OnDisable()
    {
        EventBus.OnRockReachedEnd -= AddRock;
        EventBus.ResetGame -= ResetLevel;
    

    }
    private void Start()
    {
        gameState = GameState.Play;
        EventBus.OnRockCountChanged?.Invoke(RockCount);
        // Khởi tạo UI thông qua UIManager
        //UIManager.Instance.Init(RockLimit, LevelTime);
    }

    private void Update()
    {
        if (gameState != GameState.Play)
            return;

        LevelTime -= Time.deltaTime;
        EventBus.OnTimeChanged?.Invoke(Mathf.Max(0, LevelTime));

        if (LevelTime <= 0f)
        {
            LevelTime = 0f;
            Lose();
        }
    }

    public void AddRock()
    {
        if (gameState != GameState.Play)
            return;

        RockCount++;
        
        // Cập nhật Slider
        EventBus.OnRockCountChanged?.Invoke(RockCount);
        // Dùng >= để đủ RockLimit là thắng
        if (RockCount >= RockLimit)
        {
            Win();
        }
    }
    public void Lose()
    {
        if (gameState == GameState.End)
            return;

        gameState = GameState.End;
        EventBus.OnEndGame?.Invoke(false);
    }

    public void Win()
    {
        if (gameState == GameState.End)
            return;

        gameState = GameState.End;
        EventBus.OnEndGame?.Invoke(true);
    }
    private void ResetLevel()
    {
        RockCount = 0;
        LevelTime = 15f;
        gameState = GameState.Play;
    }
}

public enum GameState
{
    Play,
    End
}

