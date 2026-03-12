using UnityEngine;
using UnityEngine.InputSystem;

public class Clicker : MonoBehaviour
{
    //code from https://www.youtube.com/watch?v=mRkFj8J7y_I
    public Camera myCamera;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Clicking");
            RaycastHit2D rayhit = Physics2D.GetRayIntersection(myCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));

            if (!rayhit.collider)
                return;

            rayhit.collider.gameObject.SendMessage("Clicked");
        }
    }
}
