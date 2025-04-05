using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyTrain", 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVelocity(float speed)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0);
    }

    private void DestroyTrain()
    {
        Destroy(gameObject);
    }
}
