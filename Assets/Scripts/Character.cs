using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //[SerializeField] private GameObject bot;
    
    [SerializeField] private GameObject bot;
    private UpdatingValue updatingLazy;
    private float angle;
    private float speed;
    private float step;
    private float deltaAngle;
    private Vector3 target;
    private Vector3 mousePos;
    private Vector3 botPos;
    private bool FirstRunning = false;
    private float distanceToTarget;
    private float countSteps;
    RunMethodJ<Vector3> rm;
    RunMethod cb, run;

    private void Awake()
    {
        speed = 1.0f;
        step = speed * Time.deltaTime;        
    }
    void Start()
    {
        GameObject bot = GetComponent<GameObject>();
        updatingLazy = new UpdatingValue();
        // rm = new RunMethodJ<Vector3>(Move);
        //rm(Vector3.left);
        cb = Move_2;
        run = hh;
    }

    void hh()
    {
        distanceToTarget = Vector3.Distance(mousePos, botPos);
        countSteps = distanceToTarget / step;
        deltaAngle = angle / countSteps;
        if (!FirstRunning)
        {
            Move_2();
            FirstRunning = true;
            Debug.Log("hh");
        }
    }
    private void Move_2()
    {        
        botPos = bot.transform.position;
        Vector3 targetDir = mousePos - botPos;
        Vector3 botForward = bot.transform.forward;

        
        bot.transform.position = Vector3.MoveTowards(botPos, mousePos, step);
        angle = Vector3.SignedAngle(targetDir, botForward, Vector3.up);

        bot.transform.Rotate(0, -deltaAngle * 4.0f, 0);
    }

    private void FixedUpdate()
    {
        Debug.DrawLine(botPos, mousePos, Color.red);
    }
    void Update()
    {
        mousePos = MouseHitting.mouseHit;

        updatingLazy.CheckingValue(botPos.x, 0.001f, cb);
        updatingLazy.CheckingValue(mousePos.x, 0.001f, run);
    }
}
