using Godot;
using System;

public partial class Enemy : Area2D
{
	private float _horizontalSpeed = 50.0f;
	private float _verticalSpeed = 1.0f;
	private static int _direction = 1;
	private static int _numberOfEnemies = 0;
	private int _enemyId;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_numberOfEnemies++;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += new Vector2((_horizontalSpeed + ((float)100.0/_numberOfEnemies)) * _direction, _verticalSpeed + ((float)3.0/_numberOfEnemies)) * (float)delta;
		if(Position.X <= GetViewport().GetVisibleRect().Size.X / 4 || Position.X > GetViewport().GetVisibleRect().Size.X * 3 / 4)
		{
			ChangeDirection();
		}
	}

	public static void ChangeDirection()
	{
		_direction *= -1;
	}

	public void Shoot()
	{
		
	}

	public void _on_area_entered(Area2D area)
	{
		
	}

	public void SetId(int id)
	{
		_enemyId = id;
	}

	public int GetId()
	{
		return _enemyId;
	}

	public static void RemoveEnemy()
	{
		_numberOfEnemies--;
	}

	public static int GetNumberOfEnemies()
	{
		return _numberOfEnemies;
	}
}
