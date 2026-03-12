using UnityEngine;

public class Button : MonoBehaviour
{
    bool clicked = false;
    public bool active = false;
    public int[] position;
    public Game game;

    public void Awake()
    {
        clicked = false;
        active = false;
}

    public void Clicked()
    {
        if (clicked) return;
        Debug.Log("Bingus");

        game.playerSelected(position);

        clicked = true;
    }
}
