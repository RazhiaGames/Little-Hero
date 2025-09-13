using Sirenix.OdinInspector;
using UnityEngine;

public class ColliderSizeFixer : MonoBehaviour
{
    public BoxCollider2D thisCollider;
    public BoxCollider2D otherCollider;
    [Button]
    public void UpdateOtherCollider()
    {
        otherCollider.size = new Vector2(thisCollider.size.x, thisCollider.size.y);
        otherCollider.offset = new Vector2(thisCollider.offset.x, thisCollider.offset.y);
		otherCollider.transform.rotation = thisCollider.transform.rotation;
    }
}
