using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class FindPathLevel : MonoBehaviour
{
    public SplineContainer m_Spline;
    public List<SpriteRenderer> flowers;
    
    public IReadOnlyList<Spline> splines => LoftSplines;

    public IReadOnlyList<Spline> LoftSplines
    {
        get
        {
            if (m_Spline == null)
                m_Spline = GetComponent<SplineContainer>();

            if (m_Spline == null)
            {
                Debug.LogError("Cannot loft road mesh because Spline reference is null");
                return null;
            }

            return m_Spline.Splines;
        }
    }
}
