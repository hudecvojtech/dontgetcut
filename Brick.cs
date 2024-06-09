using Godot;
using System;

public partial class Brick : Area2D
{
	[Signal]
	public delegate void PlayerHitEventHandler();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_body_entered(Node body)
	{
		if (body is Player)
		{
			GD.Print("end");
			CallDeferred(nameof(ProcessPlayerHit));
		}
	}

	private void ProcessPlayerHit() {
		EmitSignal(nameof(PlayerHit));
	}

	
}