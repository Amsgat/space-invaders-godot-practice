using Godot;
using System;
using System.Collections.Generic;

public partial class LevelManager : Node
{
	private PackedScene _enemySpawner = GD.Load<PackedScene>("res://Scenes/enemy.tscn");
	private List<Enemy> _enemyList;
	private PackedScene _playerSpawner = GD.Load<PackedScene>("res://Scenes/player.tscn");
	private Player _player;
	private PackedScene _bulletSpawner = GD.Load<PackedScene>("res://Scenes/bullet.tscn");

	private int _score;

	int i = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_enemyList = new();
		PopulateEnemies();

		_player = _playerSpawner.Instantiate<Player>();
		AddChild(_player);
		_player.Position = new Vector2(500,500);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("ui_accept"))
		{
			Shoot();
		}

		if(i % 100 == 0)
		{
			GD.Print(_enemyList.Count);
		}

		i++;
	}

	public void PopulateEnemies()
	{
		for(int i = 0; i < 50; i++)
		{
			Enemy e = _enemySpawner.Instantiate<Enemy>();
			AddChild(e);
			e.Position = new Vector2(20 + (50 * (_enemyList.Count % 10)), 20 + (50 * (_enemyList.Count / 10)));
			_enemyList.Add(e);
		}
	}

	public void Shoot()
	{
		Bullet bullet = _bulletSpawner.Instantiate<Bullet>();
		AddChild(bullet);
		bullet.Position = new Vector2(_player.Position.X, _player.Position.Y-10);

		bullet.enemyHitEvent += OnEnemyHit;
	}

    private void OnEnemyHit(object sender, Enemy enemy)
    {
		_score++;
		_enemyList.Remove(enemy);
		RemoveChild(enemy);
    }
}
