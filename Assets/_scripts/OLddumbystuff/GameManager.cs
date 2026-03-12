using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;
    public Vector2 screenSize;
    public void Awake()
    {
        Singleton = this;
        screenSize = GetScreenSize();
    }

    public Vector2 GetScreenSize()
    {
        Vector2 screen = new Vector2(
            Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0f))),
            Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f)), Camera.main.ScreenToWorldPoint(new Vector2(0f, Screen.height))));

        return screen;
    }
}
