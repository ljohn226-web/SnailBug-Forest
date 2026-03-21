using UnityEngine;

public class Bubble : MonoBehaviour
{

    public RiverBehavior river;
    private void OnDestroy()
    {
        if (river != null)
        {
            river.RemoveBubble(gameObject);
        }
    }
}
