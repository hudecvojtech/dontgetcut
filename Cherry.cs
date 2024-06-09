using Godot;
using System;

public partial class Cherry : Area2D
{
	[Signal]
	public delegate void CherryEatenEventHandler();
	
	public override void _Ready()
	{
		Position = GetRandomPosition();
	}
	
	private Vector2 GetRandomPosition()
	{
		Vector2 newPosition = GenerateRandomPosition();
		while (PositionOverlaps(newPosition)) 
		{
			newPosition = GenerateRandomPosition();
		}
		return newPosition;
	}
	
	private Vector2 GenerateRandomPosition() {
		return new Vector2(
			GD.RandRange(1, Main.CellsHorizontal - 2) * Main.CellSize, 
			GD.RandRange(1, Main.CellsVertical - 2) * Main.CellSize
		);
	}
		
	private bool PositionOverlaps(Vector2 position)
	{
		var player = GetTree().Root.GetNode<Player>("Main/Player");
		return position == player.Position;
	}
	
	private void _on_body_entered(Node body)
	{
		if (body is Player)
		{
			CallDeferred(nameof(ProcessCherryEaten));
		}
	}
	
	private void ProcessCherryEaten() {
		EmitSignal(nameof(CherryEaten));
		Position = GetRandomPosition();
	}
}
