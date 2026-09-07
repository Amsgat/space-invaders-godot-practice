using Godot;
using System;

public partial class Bullet : Area2D
{
	public event EventHandler<Enemy> enemyHitEvent;
	public event EventHandler<Player> playerHitEvent;
	private float _speed = 200.0f;
	private float _direction = 1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += new Vector2(0,_direction * _speed * (float)delta);
	}

	//check which Area2D overlaps with the bullet
	//Invokes EventHandlers when enemy or player is hit
	public void _on_area_entered(Node area)
	{
		if(area is Enemy enemy)
		{
			enemyHitEvent.Invoke(this, enemy);
		}
		
		QueueFree();
	}

	public void _on_body_entered(Node2D body)
	{
		if(body is Player player)
		{
			playerHitEvent.Invoke(this, player);
		}

		QueueFree();
	}

	public void SetDirection(int direction)
	{
		_direction = direction;
	}
}
