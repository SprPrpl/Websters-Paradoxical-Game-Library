using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
public class PipeManager : MonoBehaviour
{
    public GameObject firstPipe;
    private List<Transform> pipes;
    public float speed;
    private float distanceBetween;
    public int pipeCount;
    public Vector3 spawnPoint;
    private float teleportPoint;
    private float ground;
    public float heightRange;

    public void OnEnable()
    {
        teleportPoint = (GameManager.Singleton.screenSize.x * -0.5f) - 0.5f;
        distanceBetween = Mathf.Abs(teleportPoint - firstPipe.transform.position.x);
        loadPipes();
    }

    public void Update()
    {
       moveForward();
    }

    public void moveForward()
    {
        foreach (Transform t in pipes) 
        {
            t.position = Vector3.MoveTowards(t.position, new Vector3(teleportPoint,t.position.y,0), speed*Time.deltaTime);
            if(t.position.x <= teleportPoint)
            {
                t.position = spawnPoint;
                t.position = new Vector3(t.position.x, Random.Range(-heightRange, heightRange),0);
            }
        }
    }

    public void loadPipes()
    {
        pipes = new List<Transform>();
        pipes.Add(firstPipe.transform);
        Vector3 currentPos = firstPipe.transform.position;
        for (int i = 1; i < pipeCount; i++)
        {
            pipes.Add(Instantiate(firstPipe.transform, transform));
            pipes[i].position = currentPos + new Vector3(distanceBetween,0,0);
            currentPos = pipes[i].position;
            pipes[i].position += new Vector3(0, Random.Range(-heightRange, heightRange), 0);
        }

        spawnPoint = pipes[pipeCount - 1].position;
        
    }


}
