using Godot;
using System;

public partial class GameOver : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Button>("RestartButton")
			.Connect("pressed", new Callable(this, nameof(OnRestartButtonPressed)));
	  	GetNode<Button>("ExitButton")
			.Connect("pressed", new Callable(this, nameof(OnExitButtonPressed)));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
		
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventKey && eventKey.Pressed)
		{
			if (eventKey.Keycode == Key.Space)
			{
				RestartGame();
			}
			else if (eventKey.Keycode == Key.E)
			{
				ExitGame();
			}
		}
	}
	
	public void SetScore(int score)
	{
		GetNode<Label>("ScoreLabel").Text = "Score: " + score;
	}
	
	private void OnRestartButtonPressed()
	{
		RestartGame();
	}
	
	private void RestartGame()
	{
		var mainScene = (PackedScene)ResourceLoader.Load("res://Main.tscn");
		GetTree().ChangeSceneToPacked(mainScene);
	}
	
	private void OnExitButtonPressed()
	{
		ExitGame();
	}
	
	private void ExitGame()
	{
		GetTree().Quit();
	}
}
