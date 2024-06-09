using Godot;
using System;

public partial class Player : CharacterBody2D
{	
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	
	private AnimatedSprite2D animation;
	private bool direction;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	public override void _Ready() {
		animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		direction = true;
		animation.FlipH = direction;
		Vector2 velocity = Velocity;
		velocity.X = Speed;
		Velocity = velocity;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		
		if (IsOnWall()) {
			velocity.X = -velocity.X;
			direction = !direction;
			animation.FlipH = direction;
		} 
		
		velocity.Y += gravity * (float)delta;
		
		if (Input.IsActionJustPressed("ui_accept") && !IsOnFloor()) {
			velocity.Y += JumpVelocity;
		}
		
		Velocity = velocity;
		MoveAndSlide();
		
		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			var collision = GetSlideCollision(i);
			var collider = (Node)collision.GetCollider();
			GD.Print(collider.Name); // FIXME end game on touch floor or ceiling
		}
	}

}
