//============================================================================
// PendSim.cs : Defines derived class for simulating a simple pendulum.
//============================================================================
using System;

public class PendSim : Simulator
{
    float L;      // pendulum length
    double m;   //mass

    double k;   //spring constant


    public PendSim() : base(8)
    {
        L = 0.9f;
        m = 1.4;
        k = 90.0;
        
        

        x[0] = 1.0; // default angle
        x[1] = 0.0; //default angular velocity
        x[2] = 0.0; //default x position
        x[3] = 0.0; //default x velocity
        x[4] = 0.0; //default y positon
        x[5] = 0.0; //default y velocity
        x[6] = 0.0; //default z position
        x[7] = 0.0; //default z velocity
        SetRHSFunc(RHSFuncPendulum);
    }

    //----------------------------------------------------
    // RHSFuncPendulum
    //----------------------------------------------------
    private void RHSFuncPendulum(double[] xx,
        double t, double[] ff)
    {
            ff[0] = xx[1];
            ff[1] = -g/L*Math.Sin(xx[0]);
            ff[2]= xx[3];
            ff[3]= -g/L * Math.Sin(xx[2]);
            ff[4]= xx[5];
            ff[5] = -g/L * Math.Sin(xx[4]);
            double springForce = -k * (xx[2]-L);
            ff[2] = xx[3];
            ff[3]= (springForce) / m;
    double xDot = xx[3];
    double zDot = xx[7];
    double yDot = xx[5];
    }




    //--------------------------------------------------------------------
    // Getters
    //--------------------------------------------------------------------
    public double Angle
    {
        get{
            return(x[0]);
        }

        set{
            x[0] = value;
        }
        }
        public double KineticEnergy
    {
        get
        {
            double ke = 0.5*m*(x[1]*x[1]+x[3]*x[3]+x[7]*x[7]);
            return(ke);

        }
    }
    public double PotentialEnergy
    {
        get
        {
            double pe = m*g*x[4];
            return(pe);
        }
    }
    public double TotalMechanicalEnergy
    {
        get
        {
            double TotalME = PotentialEnergy + KineticEnergy;
            return(TotalME);
        }
    }
}