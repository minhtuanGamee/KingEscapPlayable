using Spine;
using Spine.Unity;
using System;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation skeleton;
    [SerializeField] private string[] skinId;
    [SerializeField] private string[] weaponId;
    private bool isMoving;
    public int indexSkin = 0;
    public int indexWeapon = 0;
    public bool showWeapon = false;
    [SerializeField] private AudioSource audioSource;
    public AudioClip hitClip;
    private void Start()
    {
        PlayAnimation("push", 0,true);
        UpdateCharacterSkin();
    }
    private void OnEnable()
    {
        EventBus.OnEndGame += CharacterEndGame;
    }

    private void OnDisable()
    {
        EventBus.OnEndGame -= CharacterEndGame;
    }
    public void PlayAnimation(string name, int indexTrack, bool loop, Action<Spine.Event> onEvent = null, Action onComplete = null)
    {
        var anim = skeleton.AnimationState.SetAnimation(0, name, loop);

        if (onEvent != null)
        {
            anim.Event += (trackEntry, e) =>
            {
                onEvent(e);
            };
        }
        if (onComplete != null)
        {
            anim.Complete += _ => onComplete();
        }
    }
    public void UpdateCharacterSkin()
    {
        var skeletonData = skeleton.Skeleton.Data;

        // Tạo skin kết hợp
        Skin combinedSkin = new Skin("CombinedSkin");

        // Skin nhân vật hiện tại
        Skin characterSkin = skeletonData.FindSkin(skinId[indexSkin]);
        if (characterSkin != null)
        {
            combinedSkin.AddSkin(characterSkin);
        }

        if (showWeapon)
        {
            // Weapon hiện tại
            Skin weaponSkin = skeletonData.FindSkin(weaponId[indexWeapon]);
            if (weaponSkin != null)
            {
                combinedSkin.AddSkin(weaponSkin);
            }
        }
        // Áp dụng skin
        skeleton.Skeleton.SetSkin(combinedSkin);
        skeleton.Skeleton.SetSlotsToSetupPose();

        // Cập nhật animation ngay
        skeleton.LateUpdate();
    }
    private void CharacterEndGame(bool isWin)
    {
        showWeapon = false;
        UpdateCharacterSkin();
        if (isWin)
        {
            PlayAnimation("win", 0, true);
        }
        else
        {
            PlayAnimation("lose", 0, false);
        }
    }
}
