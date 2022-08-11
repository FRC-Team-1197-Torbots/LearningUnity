using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorDrive : MonoBehaviour
{
    public enum INPUT_TYPE { GameInput, RobotInput, PID };
    public INPUT_TYPE type;


    [Header("Drive Variables")]
    public float speed;
    public float rotateSpeed;
    private float distance;
    private float rotation;
    private Vector3 StartPosition;
    private Quaternion StartRotation;
    private bool completed;

    #region
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    private void Init()
    {
        //this is where you would initalize the "motors"


    }

    public void Run()
    {
        switch (type)
        {
            case INPUT_TYPE.GameInput:
                DoGameInput();
                break;

            case INPUT_TYPE.RobotInput:
                DoRobotInput();
                break;

            case INPUT_TYPE.PID:

                break;
        }
    }

    public void SetupAuto()
    {
        distance = 0;
        rotation = 0;

        StartPosition = transform.position;
        StartRotation = transform.rotation;

        completed = false;
    }

    public void DriveFor(float distance)
    {
        if (Vector3.Distance(StartPosition, transform.position) < distance)
            transform.position += transform.forward * speed * Time.deltaTime;
        else
            completed = true;
    }

    public void TurnFor(float rotation)
    {
        if (Quaternion.Angle(StartRotation, transform.rotation) < Math.Abs(rotation))
            transform.Rotate(transform.up, (rotation / Mathf.Abs(rotation)) * rotateSpeed);
        else
            completed = true;
    }

    //function for auto to see if current step is completed
    public bool isCompleted()
    {
        return completed;
    }


    private void DoRobotInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position += transform.forward * -speed * Time.deltaTime;
        }         
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(transform.up, -rotateSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(transform.up, rotateSpeed);
        }
    }

    private void DoGameInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position += transform.forward * -speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += transform.right * -speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }
}
