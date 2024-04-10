using Godot;
using System;

public partial class SimBeginScene : Node3D
{
	// Called when the node enters the scene tree for the first time.
	
		MeshInstance3D anchor;
		MeshInstance3D ball;
		SpringModel spring;
		// Label keLabel;
		// Label pelabel;
		// Label totalLabel;
		PendSim pend;

		//PanelDisplay display;
		
		double xA, yA, zA; // coords of anchor14 
		float length0; // natural length of pendulum15 
		float length; // length of pendulum16 
		double angle; // pendulum angle17 
		double angleInit; // initial pendulum angle18 
		double time;
		double k;
		double mass;


		Vector3 endA; // end point of anchor21 
		Vector3 endB; // end point for pendulum bob

	
	public override void _Ready()
	{
		GD.Print("Hello MEE 381 in Godot!");
		xA = 0.0; yA = 1.2; zA = 0.0;
		anchor = GetNode<MeshInstance3D>("Anchor");
		ball = GetNode<MeshInstance3D>("Ball1");
		spring = GetNode<SpringModel>("SpringModel");
		endA = new Vector3((float)xA, (float)yA, (float)zA);
		anchor.Position = endA; 

		// keLabel = GetNode<Label>("VBoxContainer/keLabel");
		// peLabel = GetNode<Label>("VBoxContainer/peLabel");
		// totalLabel= GetNode<Label>("VBoxContainer/Totlabel");
		pend = new PendSim();

		length0 = length = 0.9f;
		mass = 1.4;
		k = 90.0;

		spring.GenMesh(0.05f, 0.015f, length, 6.0f, 62);
		
		angleInit = Mathf.DegToRad(60.0);
		float angleF = (float)angleInit; 
		pend.Angle = (double)angleInit;

		endB.X = endA.X + length*Mathf.Sin(angleF);
		endB.Y = endA.Y - length*Mathf.Cos(angleF);
		endB.Z = endA.Z;
		PlacePendulum(endB);
		//PlacePendulum((float)angle);
		time = 0.0;

		
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame
	public override void _Process(double delta)
	{
		//float angleF = (float)Math.Sin(3.0 * time);
		// float angleA = (float)(0.4*time);
		 //length = length0 + 0.3f * (float)Math.Cos(5.0f * time);
		//length = length0 +0.3f * (float)Math.Sin(5.0 * time);

		float angleA = 0.0f;
		//pend.StepRK4(time, delta);
		float angleF = (float)pend.Angle;

	
	 float hz = length*Mathf.Sin(angleF);

	 endB.X = endA.X + hz*Mathf.Cos(angleA);
	 endB.Y = endA.Y - length*Mathf.Cos(angleF);
	 endB.Z = endA.Z + hz*Mathf.Sin(angleA);
	 PlacePendulum(endB);
	 time += delta;

	
            
        // GetNode<Label>("KE").value = pend.KE;
		// GetNode<Label>("PE").value = pend.PE;
		// GetNode<Label>("Total").value = pend.TotalME;

         
	}
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		pend.StepRK4(time, delta);
	 } 
	// public override 
	//void _PhysicsProcess(double delta)
	//{ 
	//}
	private void PlacePendulum(Vector3 endBB)
	{
	spring.PlaceEndPoints(endA, endB);
	ball.Position = endBB;
	 }
}
