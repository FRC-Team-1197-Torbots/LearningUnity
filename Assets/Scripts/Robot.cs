﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Clone class for "running a robot"
 */


public class Robot : MonoBehaviour
{
    public TorDrive m_drive;
    private bool AutoInitial = true;

    public enum STATE { DISABLED, ENABLED };
    public STATE m_state;

    public enum MODE { AUTO, TELE, IDLE };
    public MODE m_mode;

    private enum AUTO { DRIVEFORWARD, TURN, DONE };
    private enum AUTO_EXAMPLE { MOVE_STRAIGHT, ROTATE, DONE};

    private AUTO auto_state;
    private AUTO_EXAMPLE auto_ex_state;

    #region WPI Clone Functions

    /**
     * Function intially called whne Autonomous starts
     */
    public void autonomousInit()
    {
        m_drive.SetupAuto();
    }

    public void teleopInit()
    {

    }

    //initalize variables for the start of the robot
    public void robotInit()
    {
        if (!m_drive)
            m_drive = GetComponent<TorDrive>();

        m_state = STATE.ENABLED;
        m_mode = MODE.TELE;
    }

    public void autonomousPeriodic()
    {
        if (AutoInitial)
        {
            autonomousInit();
            AutoInitial = false;
            auto_ex_state = AUTO_EXAMPLE.MOVE_STRAIGHT;
            //auto_state = AUTO.DRIVEFORWARD;
        }

        switch(auto_ex_state)
        {
            case AUTO_EXAMPLE.MOVE_STRAIGHT:
                m_drive.DriveFor(2f);
                break;

            case AUTO_EXAMPLE.ROTATE:
                m_drive.TurnFor(60);
                break;
        }    
        //drive forward for 1.5 then rotate 10 degrees
        switch (auto_state)
        {
            case AUTO.DRIVEFORWARD:
                m_drive.DriveFor(1.5f);
                break;

            case AUTO.TURN:
                m_drive.TurnFor(-10);
                break;
        }

        for (int i = 0; i < 6; i++)
        {
            if (m_drive.isCompleted() && auto_ex_state == AUTO_EXAMPLE.MOVE_STRAIGHT && i < 5)
            {
                Debug.Log("Switching to Turn");
                auto_ex_state = AUTO_EXAMPLE.ROTATE;
                m_drive.SetupAuto();
            }
            else if (m_drive.isCompleted() && auto_ex_state == AUTO_EXAMPLE.ROTATE && i < 5)
            {
                Debug.Log("Switching to Straight");
                auto_ex_state = AUTO_EXAMPLE.MOVE_STRAIGHT;
                m_drive.SetupAuto();
            }
            else if (m_drive.isCompleted() && auto_ex_state == AUTO_EXAMPLE.ROTATE && i > 4)
            {
                Debug.Log("Switching to Done");
                auto_ex_state = AUTO_EXAMPLE.DONE;
                m_drive.SetupAuto();
            }
        }
    }

    public void teleopPeriodic()
    {
        m_drive.Run();
    }

    public void autonomousExit()
    {

    }

    public void teleopExit()
    {

    }

    #endregion

    #region UNITY FUNCTIONS
    // Start is called before the first frame update
    void Start()
    {
        robotInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            if (m_state == STATE.ENABLED)
            {
                m_state = STATE.DISABLED;
            }
            else
            {
                m_state = STATE.ENABLED;
            }
        }


        if (m_state == STATE.ENABLED)
        {
            //area for running code while enabled;
            switch (m_mode)
            {
                case MODE.AUTO:
                    autonomousPeriodic();
                    break;

                case MODE.TELE:
                    teleopPeriodic();
                    break;
            }
        }
        else if (m_state == STATE.DISABLED)
        {
            //area for running code while disabled
        }
    }

    #endregion
}
