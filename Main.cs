using Godot;
using System;

public partial class Main : Node2D
{
	public const int CellSize = 32;
	public const int CellsHorizontal = 20;
	public const int CellsVertical = 30;
	
	private PackedScene wallScene;
	private PackedScene brickScene;
	private PackedScene sawScene;
	private Player player;
	
	private Random random = new Random();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		wallScene = GD.Load<PackedScene>("res://Wall.tscn");
		brickScene = GD.Load<PackedScene>("res://Brick.tscn");
		sawScene = GD.Load<PackedScene>("res://Saw.tscn");
		CreateTrap();
		CreateWalls();
		
		player = GetNode<Player>("Player");
		player.Position = new Vector2(CellSize, (CellsVertical * CellSize) / 2);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void CreateWalls()
	{
		// Create brick on the top and bottom rows, including corners
		CreateBrick(0, 0, CellsHorizontal); // Top brick
		CreateBrick(0, CellsVertical - 1, CellsHorizontal); // Bottom brick, including corners

		// Create stone walls on the left and right columns, including corners
		CreateWall(0, 0, 1, CellsVertical); // Left wall, including corners
		CreateWall(CellsHorizontal - 1, 0, 1, CellsVertical); // Right wall, including corners
	}

	private void CreateWall(int startX, int startY, int width, int height)
	{
		for (int i = startX; i < startX + width; i++)
		{
			for (int j = startY; j < startY + height; j++)
			{
				StaticBody2D wall = (StaticBody2D)wallScene.Instantiate();
				wall.Position = new Vector2(i * CellSize, j * CellSize);
				AddChild(wall);
			}
		}
	}

	private void CreateBrick(int startX, int startY, int width)
	{
		for (int i = startX; i < startX + width; i++)
		{
			Area2D brick = (Area2D)brickScene.Instantiate();
			brick.Position = new Vector2(i * CellSize, startY * CellSize);
			AddChild(brick);
		}
	}
	
	private void CreateTrap() {
		int sawYCellPosition = random.Next(1, CellsVertical - 1);
		Area2D saw = (Area2D)sawScene.Instantiate();
		saw.Position = new Vector2((CellsHorizontal - 1) * CellSize, sawYCellPosition * CellSize);
		AddChild(saw);
		
		AnimatedSprite2D sawSprite = saw.GetNode<AnimatedSprite2D>("Saw");
		sawSprite.Play();
	}
}
