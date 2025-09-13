using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class DifferenceItem : MonoBehaviour
{
    public BoxCollider2D collider;
    public BoxCollider2D secondCollider;
    public SpriteRenderer rightSprite;
    public SpriteRenderer secondRightSprite;


    [Button]
    public void UpdateCollider()
    {
        secondCollider.size = new Vector2(collider.size.x, collider.size.y);
    }


    public async UniTaskVoid OnFound()
    {
        collider.enabled = false;
        secondCollider.enabled = false;
        rightSprite.transform.position = collider.gameObject.transform.position;
        secondRightSprite.transform.position = secondCollider.gameObject.transform.position;
        rightSprite.gameObject.SetActive(true);
        secondRightSprite.gameObject.SetActive(true);

        var color = FindDifferenceGameHandler.Instance.levelRightSpriteColor;
        rightSprite.color = color;
        secondRightSprite.color = color;

    }

    
}
