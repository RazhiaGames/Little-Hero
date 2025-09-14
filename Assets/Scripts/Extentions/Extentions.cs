using UnityEngine;

public static class Extentions
{
    public static void Clear(this Transform transform, string ignoreTag = "")
    {
        for (int i = transform.childCount - 1; i >= 0; i--) // Start from the last child
        {
            Transform child = transform.GetChild(i);
            if (ignoreTag != "")
            {
                if (child.gameObject.CompareTag(ignoreTag)) 
                    continue; // Skips this child but doesn't cause an infinite loop
            }


            child.SetParent(null);

#if UNITY_EDITOR
            Object.DestroyImmediate(child.gameObject);
#else
        Object.Destroy(child.gameObject);
#endif
        }
    }

}
