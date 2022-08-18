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

    private enum AUTO { DRIVEFORWARD, DRIVEBACKWARD, DRIVEFORWARD1, DRIVEFAR, TURNBACK, TURNBACK1, TURN, DONE };
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
            auto_state = AUTO.TURNBACK;
        }

        //drive forward for 1.5 then rotate 10 degrees
        switch (auto_state)
        {
            case AUTO.TURNBACK:
                m_drive.TurnFor(179);
                break;
            
            case AUTO.DRIVEFORWARD:
                m_drive.DriveFor(1.5f);
                break;
            
            case AUTO.TURN:
                m_drive.TurnFor(40);
                break;

            case AUTO.DRIVEFAR:
                m_drive.DriveFor(5f);
                break;

            case AUTO.TURNBACK1:
                m_drive.TurnFor(179);
                break;

            case AUTO.DRIVEFORWARD1:
                m_drive.DriveFor(1.5f);
                break;
        }

        if (m_drive.isCompleted() && auto_state == AUTO.TURNBACK)
        {
            Debug.Log("Switching to Frwd1");
            auto_state = AUTO.DRIVEFORWARD;
            m_drive.SetupAuto();
        }
        else if (m_drive.isCompleted() && auto_state == AUTO.DRIVEFORWARD) {
            Debug.Log("Switching to Turn2");
            auto_state = AUTO.TURN;
            m_drive.SetupAuto();
        }
        else if (m_drive.isCompleted() && auto_state == AUTO.TURN) {
            Debug.Log("Switching to Frwd2");
            auto_state = AUTO.DRIVEFAR;
            m_drive.SetupAuto();
        }
        else if (m_drive.isCompleted() && auto_state == AUTO.DRIVEFAR) {
            Debug.Log("Switching to Turn3");
            auto_state = AUTO.TURNBACK1;
            m_drive.SetupAuto();
        }
        else if (m_drive.isCompleted() && auto_state == AUTO.TURNBACK1) {
            Debug.Log("Switching to Frwd3");
            auto_state = AUTO.DRIVEFORWARD1;
            m_drive.SetupAuto();
        }
        else if (m_drive.isCompleted() && auto_state == AUTO.DRIVEFORWARD1)
        {
            Debug.Log("Switching to Done");
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


/*-------------------------
INTAKE CLASS CONTROLS EVERYTHING INTAKE RELATED. DRIVER PRESSES 1 BUTTON TO TOGGLE INTAKE
    -DROPS AND RAISES INTAKE
    -STARTS/STOPS ROLLERS
**RUNS SEPARATELY FROM THE TURRET CLASS

motors: intake, roller
-------------------------*/

public class Intake {
    
    public enum intake {movein, moveout, off};
    public enum rollers {movein, moveout, off};
    private intake intakeState;
    private rollers rollerState;

    public float rollerin = -0.8f, rollerout = 0.5f;

    public void run() {

        intakeState = intake.movein;
        rollerState = rollers.movein;
        
        switch(intakeState) {
            case intake.movein:
                roller.movein;
                IntakeTurn(rollerin,90);
            break;

            case intake.moveout:
                roller.moveout;
                IntakeTurn(rollerout,-90);
            break;

        }
        {
        if ( Input.GetKey(KeyCode.UpArrow) ) {
            intakeState = intake.movein;
        }
        else if ( Input.GetKey(KeyCode.DownArrow) ) {
            intakeState = intake.moveout;
        }        
    }
}
}