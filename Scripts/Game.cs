using Godot;
using System;

public partial class Game : Node2D
{
	private MainMenu _menu;
	private LevelManager _levelManager;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_menu = new();
		_levelManager = new();
		AddChild(_menu);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("ui_accept") && _menu.IsInsideTree())
		{
			RemoveChild(_menu);
			AddChild(_levelManager);
		}
	}
}
