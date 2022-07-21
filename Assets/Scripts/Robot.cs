using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Clone class for "running a robot"
 */







public class Robot : MonoBehaviour
{
    public enum STATE { DISABLED, ENABLED };
    private STATE m_state;

    public enum MODE { AUTO, TELE };
    private MODE m_mode;

    #region WPI Clone Functions

    /**
     * Function intially called whne Autonomous starts
     */
    public void autonomousInit()
    {

    }

    public void teleopInit()
    {

    }

    public void robotInit()
    {

    }

    public void autonomousPeriodic()
    {

    }

    public void teleopPeriodic()
    {

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
        if (m_state == STATE.ENABLED)
        {
            //area for running code while enabled;
            switch(m_mode)
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
