using UnityEngine;

public class MapManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UIManager.Instance.mapInGameInstance == null)
            {
                GameManager.Instance.DisableController();
                UIManager.Instance.ShowMapInGame();
            }
        }
    }
}