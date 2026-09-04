using UnityEngine;

public class Rock : MonoBehaviour
{
    private void FixedUpdate()
    {
        if(transform.position.y < -5)
        {
            EventBus.OnRockReachedEnd?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
