using UnityEngine;

public class FrogPlayer : MonoBehaviour
{
    public float moveDistance;
    private Rigidbody2D rb;
    private float carriedMovement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        carriedMovement = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            transform.position -= new Vector3(moveDistance, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(moveDistance, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            transform.position += new Vector3(0, moveDistance, 0);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            transform.position -= new Vector3(0, moveDistance, 0);
        }

        transform.position += new Vector3(carriedMovement, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //other.GetComponent<Log>().
    }


}
