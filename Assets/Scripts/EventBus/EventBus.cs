using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus 
{
	private static Dictionary<EventType, Action> assignedActions = new();

	public static void Lose(EventType eventType)
	{
		if (assignedActions.TryGetValue(eventType, out Action existingAction))
		{
			existingAction?.Invoke();
		}
	}
	public static void Subscribe(EventType eventType, Action action)
	{
		if (assignedActions.ContainsKey(eventType)) 
		{
			assignedActions[eventType] += action;
		}
		else
		{
			assignedActions[eventType] = action;
		}
	}
	public static void Unsubscribe(EventType eventType, Action action) 
	{
		if (assignedActions.ContainsKey(eventType))
		{
			assignedActions[eventType] -= action;
		}
	}
	public enum EventType
	{
		RockCollected,
		TimeExpired,
		LevelWon,
		LevelLost,
		CharacterFailed
	}
}
