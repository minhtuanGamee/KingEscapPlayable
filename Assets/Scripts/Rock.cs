using UnityEngine;

public class Rock : MonoBehaviour
{
    private void FixedUpdate()
    {
        if(transform.position.y < -5)
        {
            LevelManager.Instance.AddRock();
            gameObject.SetActive(false);
        }
    }
}
