using Godot;
using System;

public partial class Bullet : Area2D
{
	public event EventHandler<Enemy> enemyHitEvent;
	public event EventHandler<Player> playerHitEvent;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += new Vector2(0,-2.0f);
	}

	public void _on_area_entered(Node area)
	{
		if(area is Enemy)
		{
			enemyHitEvent.Invoke(this, (Enemy)area);
		}
		
		QueueFree();
	}
}
