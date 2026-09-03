using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private float LevelTime = 15f;
    [SerializeField] private int RockCount = 0;
    public int RockLimit = 10;
    [SerializeField] private CharacterController characterController;

    private GameState gameState;

    public float CurrentTime => LevelTime;
    public int CurrentRockCount => RockCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameState = GameState.Play;

        // Khởi tạo UI thông qua UIManager
        UIManager.Instance.Init(RockLimit, LevelTime);
    }

    private void Update()
    {
        if (gameState != GameState.Play)
            return;

        LevelTime -= Time.deltaTime;

        // Cập nhật timing UI
        UIManager.Instance.UpdateTime(LevelTime);

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
        UIManager.Instance.UpdateRock(RockCount);

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
        UIManager.Instance.Show(false);
        characterController.showWeapon = false;
        characterController.UpdateCharacterSkin();
        characterController.PlayAnimation("lose_prone",0,false);
    }

    public void Win()
    {
        if (gameState == GameState.End)
            return;

        gameState = GameState.End;
        UIManager.Instance.Show(true);
        characterController.showWeapon = false;
        characterController.UpdateCharacterSkin();
        characterController.PlayAnimation("win",0,true);
    }
}

public enum GameState
{
    Play,
    End
}

