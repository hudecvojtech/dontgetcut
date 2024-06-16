using Godot;
using System;

public partial class Saw : AnimatedSprite2D
{
	private const int MinSpeed = 20;
	private const int MaxSpeed = 60;

	private PathFollow2D pathFollow;
	private float speed;
	
	private Random random = new Random();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{		
		InitPathFollow();
		speed = CalculateSpeed();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (pathFollow != null) 
		{
			pathFollow.Progress += (float)(speed * delta);
			GlobalPosition = pathFollow.GlobalPosition;
		}
	}
	
	private void InitPathFollow() 
	{
		Node parent = GetParent();
		while (parent != null)
		{
			if (parent is PathFollow2D)
			{
				pathFollow = (PathFollow2D)parent;
				break;
			}
			parent = parent.GetParent();
		}
	}
	
	private float CalculateSpeed() 
	{
		float speed = random.Next(MinSpeed, MaxSpeed + 1);
		int direction = CalculateDirection();
		return speed * direction;
	}
	
	private int CalculateDirection()
	{
		bool isPositive = Convert.ToBoolean(random.Next(0, 2));
		return isPositive ? 1 : -1;
	}
}
