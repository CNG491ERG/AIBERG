using UnityEngine;

public class DestroyOnAnimationComplete : MonoBehaviour
{
    // This method will be called by the Animation Event
    public void DestroySelf()
    {
        Destroy(transform.parent.gameObject);
    }
}