using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class EnergyShape : MonoBehaviour
{
    public int index = 0;
    public float height = 0;

    private SpriteShapeController controller;
    private Spline spline;

    public List<Vector2> splinePos = new();

    public float speed = 30;
    public float radDis = 180;
    public float currentAngle = 0;
    public float strength = 10;

    private void Awake()
    {
        controller = GetComponent<SpriteShapeController>();
        spline = controller.spline;

        for(int i = 0; i < spline.GetPointCount(); i++)
        {
            splinePos.Add(spline.GetPosition(i));
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < spline.GetPointCount(); i++)
        //{
        //    splinePos[i] = spline.GetPosition(i);
        //}

        currentAngle += speed * Time.deltaTime;
        if (currentAngle > 360)
        {
            currentAngle -= 360;
        }

        float dH1 = Mathf.Sin(currentAngle * Mathf.Deg2Rad) * strength;
        float dH2 = Mathf.Sin(currentAngle * Mathf.Deg2Rad + radDis * Mathf.Deg2Rad) * strength;
        float dH3 = Mathf.Sin(currentAngle * Mathf.Deg2Rad) * strength;
        float dH4 = Mathf.Sin(currentAngle * Mathf.Deg2Rad + radDis * Mathf.Deg2Rad) * strength;

        spline.SetPosition(2, splinePos[2] + new Vector2(0, dH1));
        spline.SetPosition(3, splinePos[3] + new Vector2(0, dH2));
        spline.SetPosition(4, splinePos[4] + new Vector2(0, dH3));
        spline.SetPosition(5, splinePos[5] + new Vector2(0, dH4));

    }
}
