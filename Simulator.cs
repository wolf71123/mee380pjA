//============================================================================
// Simulator.cs : Defines the base class for creating simulations.
//============================================================================

using System;

	public class Simulator
	{
		protected double g;    // gravitational field strength

		double m = 1.4;		//mass

		double k = 90;		//spring constant

		float l = .9f;	//length

		protected int n;      // number of first order odes
		protected double[] x; // array of states
		protected double[] xi;  // array of intermediate states

		protected double[][] f; // 2d array that holds values of rhs
		private Action<double[], double, double[]> rhsFunc;

		//--------------------------------------------------------------------
		// Constructor
		//--------------------------------------------------------------------
		public Simulator(int nn)
		{
			//Console.WriteLine("Simulator Constructor");
			n = nn;
			x = new double[n];
			xi = new double[n];
			f = new double[4][];
			f[0] = new double[n];
			f[1] = new double[n];
			f[2] = new double[n];
			f[3] = new double[n];

			g = 9.81;
			rhsFunc = nothing;
		}

		//--------------------------------------------------------------------
		// StepEuler: Executes one numerical integration step using Euler's 
		//            method.
		//--------------------------------------------------------------------
		public void StepEuler(double time, double dTime)
		{
			int i;

			rhsFunc(x,time,f[0]);
			for(i=0;i<n;++i)
			{
				x[i] += f[0][i] * dTime;
			}
		}

		//--------------------------------------------------------------------
		// StepRK2: Executes one numerical integration step using the RK2 
		//            method.
		//--------------------------------------------------------------------
		public void StepRK2(double time, double dTime)
		{
			int i;

			rhsFunc(x,time,f[0]);
			for(i=0;i<n;++i)
			{
				xi[i] = x[i] + f[0][i] * dTime;
			}

			rhsFunc(xi,time+dTime,f[1]);
			for(i=0;i<n;++i)
			{
				x[i] += 0.5*(f[0][i] + f[1][i])*dTime;
			}
		}

		//--------------------------------------------------------------------
		// StepRK4 : Executes one numerical integration step using the RK4 
		//            method.
		//--------------------------------------------------------------------
		public void StepRK4(double time, double dTime)
		{
			int i;
			

			rhsFunc(x,time,f[0]);   //calculating the rhs with respect to the state - fa 
			for(i=0;i<n;++i)
			{
				xi[i] = x[i] + f[0][i] * (dTime/2);
			}
			rhsFunc(xi,time,f[1]);  // calculating the rhs with respect to the intermediate state - fb
			for(i=0;i<n;++i)
			{
				xi[i] = x[i] + f[1][i] * (dTime/2);  //??? questioning the xi
			}
			rhsFunc(xi,time,f[2]);  // calculating the rhs with respect to the intermediate state again - fc
			for(i=0;i<n;++i)
			{
				xi[i] = x[i] + f[2][i] * dTime;   //??? questioning the xi
			}

			rhsFunc(xi,time+dTime,f[3]);    // calculating the rhs (last time) with respect to intermediate state - fb
			for(i=0;i<n;++i)
			{
				x[i] += (1.0/6.0)*(f[0][i] + 2*f[1][i] + 2*f[2][i] + f[3][i])*dTime;
			} 
		}
		// StateString: constructs a string in csv format that contains the state.
		public string StateString(double time)
		{
			string s = time.ToString();

			for (int i = 0;i<n;++i)
			{
				s += ',' + x[i].ToString();
			}
			return s;
		}
		// SetRHSFunc: Receives function from child to calculate rhs of ODE.
		protected void SetRHSFunc(Action<double[],double,double[]> rhs)
		{
			rhsFunc = rhs;
		}

		private void nothing(double[] st,double t,double[] ff)
		{

		}       
		// public double KE
        // {
        //     get
        //     {
        //         //double erg = (m(x[1]x[1] + x[3]x[3]) +IG1x[5]x[5] + m2(x[7]x[7] +x[9]x[9])+ IG2x[11]x[11]);
        //         //return(.5erg);
        //     }
        // }

        // public double PE
        // {
        //     get
        //     {
        //         //double erg = m1gx[2] + m2g*x[8];
        //         //return erg;
        //     }
        // }

        // public double Total
        // {
        //     get{
        //         return(KE+PE);
        //     }

        // }
	}
