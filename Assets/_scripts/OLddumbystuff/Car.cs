using Unity.VisualScripting;
using UnityEngine;

public class Car : MonoBehaviour 
{
    private float speed;
    public float speedRange;
    private float teleportPoint;
    private float arbitraryOffScreenSize;

    private void Start()
    {
        speed = Random.Range(-speedRange, speedRange);
        teleportPoint = teleportPoint = (GameManager.Singleton.screenSize.x * 0.5f) + arbitraryOffScreenSize;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(teleportPoint, transform.position.y, 0), speed);
    }
}
