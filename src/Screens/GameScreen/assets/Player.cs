using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Player : GameObject
{
    private const float SPEED_Y = 200;
    public List<Bullet> _bullets;
    private Rectangle[] _frames;
    private int _index;
    private Timer _timer;
    private Texture2D _bulletImage;
    private bool _initialized = false;
    private float _time;

    public Player(Texture2D image, Texture2D bulletImage) : base(image)
    {
        _frames = new Rectangle[2]
        {
            new Rectangle(0, 0, 256, 67), new Rectangle(256, 0, 256, 67)
        };

        _bulletImage = bulletImage;
        _bullets = new List<Bullet>();
    }

    public override void Initialize()
    {
        if (!_initialized)
        {
            _bounds.Width = _bounds.Width / _frames.Length;
            _initialized = true;
        }

        _bounds.X = 0;
        _bounds.Y = (Globals.SCREEN_HEIGHT / 2) - _bounds.Height;
        _index = 0;
        _timer = new Timer();
        _timer.Start(IncraseIndex, 0.2f, true);
    }

    public void IncraseIndex()
    {
        _index++;
        if (_index > 1)

        {
            _index = 0;
        }
    }

    public void Update(float deltaTime, Sounds _sounds)
    {
        _time = _time + deltaTime;
        
        if (Input.GetKey(Keys.W))
        {
            if (_bounds.Y > 0)
            {
                _bounds.Y = _bounds.Y - (int)(SPEED_Y * deltaTime);
            }
        }

        if (Input.GetKey(Keys.S))
        {
            if (_bounds.Y < Globals.SCREEN_HEIGHT - _bounds.Height)
            {
                _bounds.Y = _bounds.Y + (int)(SPEED_Y * deltaTime);
            }
        }

        if (Input.GetKey(Keys.Space) && _time > 0.5f)
        {
            _time = 0.0f;
            Shot(_sounds);
        }

        _timer.Update(deltaTime);
        
        foreach (Bullet bullet in _bullets)
        {
            bullet.Update(deltaTime);
        }
        
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_image, _bounds, _frames[_index], Color.White);
        
        foreach (Bullet bullet in _bullets)
        {
            bullet.Draw(spriteBatch);    
        }
    }

    public void Shot(Sounds sound){
        var bullet = new Bullet(_bulletImage);
        bullet.Position = new Point(_bounds.X / 2 + _bounds.Width - 45, _bounds.Y + (_bounds.Height / 2) + 10);
        bullet._isVisible = true;
        _bullets.Add(bullet);
        sound.ExecuteSoundShot();
    }

    public void CheckCollision(FiristEnemy _firistEnemy, SecondEnemy _secondEnemy, Friend _friend, Action<GameObject> _callbackPlayerCollisonEnemy, Action _callbackCollisionPlayerWithFriend)
    {
        if (_bounds.Intersects(_firistEnemy.Bounds) && _firistEnemy._isVisible)
        {
            if (_firistEnemy._isVisible)
            {
                _firistEnemy._isVisible = false;
                _callbackPlayerCollisonEnemy.Invoke(_firistEnemy);
                _firistEnemy.ResetLocation();
            }
        }

        if (_bounds.Intersects(_secondEnemy.Bounds) && _secondEnemy._isVisible)
        {
            if (_secondEnemy._isVisible)
            {
                _secondEnemy._isVisible = false;
                _callbackPlayerCollisonEnemy.Invoke(_secondEnemy);
                _secondEnemy.ResetLocation();
            }
        }

        if (_bounds.Intersects(_friend.Bounds) && _friend._isVisible)
        {
            if (_friend._isVisible)
            {
                _friend._isVisible = false;
                _callbackCollisionPlayerWithFriend.Invoke();
                _friend.ResetLocation();
                _friend._timerGenerate.Reset();
            }
        }
    }

    public void ClearAllBullets(){
        foreach (var bullet in _bullets)
        {
            bullet._isVisible = false;
            bullet.Position = new Point(0, 0);
        }
    }
}
