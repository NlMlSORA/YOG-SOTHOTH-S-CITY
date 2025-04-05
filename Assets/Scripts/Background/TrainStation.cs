using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainStation : MonoBehaviour
{
    [SerializeField] private GameObject train;
    [SerializeField] private float speed;

    [SerializeField] GameObject cameraPoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateTrain", 0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        if(cameraPoint != null)
        {
            transform.position = new Vector2(cameraPoint.transform.position.x - 28, transform.position.y);
        }
    }

    private void CreateTrain()
    {
        GameObject newTrain = Instantiate(train, transform.position, transform.rotation);
        newTrain.GetComponent<Train>().SetVelocity(speed);
    }
}
