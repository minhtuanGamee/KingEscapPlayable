using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus
{
    public static Action OnRockReachedEnd;
    public static Action<int> OnRockCountChanged;
    public static Action<float> OnTimeChanged;
    public static Action<bool> OnEndGame;
    public static Action ResetGame;
}
