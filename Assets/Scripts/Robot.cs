using System.Collections;
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

    private enum AUTO { DRIVEFORWARD, TURN2, DRIVEFORWARD3, TURN4, DRIVEFORWARD5, TURN6, DRIVEFORWARD7, DONE }; 
    private AUTO auto_state;

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
            auto_state = AUTO.DRIVEFORWARD;
            Debug.Log("Switching to DRIVEFORWARD");
        }

        //drive forward for 1.5 then rotate 60 degrees and drive 3, 60 degrees and drive 3, 60 degrees and drive 3 
        switch (auto_state)
        {
            case AUTO.DRIVEFORWARD:
                m_drive.DriveFor(1.5f);
                break;

            case AUTO.DRIVEFORWARD3:
                m_drive.DriveFor(3f);
                break;

            case AUTO.DRIVEFORWARD5:
                m_drive.DriveFor(3f);
                break;

            case AUTO.DRIVEFORWARD7:
                m_drive.DriveFor(3f);
                break;

            case AUTO.TURN2:
                m_drive.TurnFor(120);
                break;

            case AUTO.TURN4:
                m_drive.TurnFor(120);
                break;

            case AUTO.TURN6:
                m_drive.TurnFor(120);
                break;                
        }

        if (m_drive.isCompleted() && auto_state == AUTO.DRIVEFORWARD) 
        {
            Debug.Log("Switching to Turn2");
            auto_state = AUTO.TURN2;
            m_drive.SetupAuto();
        }
        else if (m_drive.isCompleted() && auto_state == AUTO.TURN2)
        {
            Debug.Log("Switching to DRIVEFORWARD3");
            auto_state = AUTO.DRIVEFORWARD3;
            m_drive.SetupAuto();
        }
        else if (m_drive.isCompleted() && auto_state == AUTO.DRIVEFORWARD3) //done till here
        {
            Debug.Log("Switching to TURN4");
            auto_state = AUTO.TURN4;
            m_drive.SetupAuto();
        }
        else if (m_drive.isCompleted() && auto_state == AUTO.TURN4)
        {
            Debug.Log("Switching to DRIVEFORWARD5");
            auto_state = AUTO.DRIVEFORWARD5;
            m_drive.SetupAuto();
        }
        else if (m_drive.isCompleted() && auto_state == AUTO.DRIVEFORWARD5)
        {
            Debug.Log("Switching to TURN6");
            auto_state = AUTO.TURN6;
            m_drive.SetupAuto();
        }
        else if (m_drive.isCompleted() && auto_state == AUTO.TURN6)
        {
            Debug.Log("Switching to DRIVEFORWARD7");
            auto_state = AUTO.DRIVEFORWARD7;
            m_drive.SetupAuto();
        }
        else if (m_drive.isCompleted() && auto_state == AUTO.DRIVEFORWARD7)
        {
            Debug.Log("Switching to DONE");
            auto_state = AUTO.DONE;
            m_drive.SetupAuto();
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
