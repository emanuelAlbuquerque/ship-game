using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Player : GameObject
{
    private const float SPEED_Y = 200;
    private Bullet _bullet;
    private Rectangle[] _frames;
    private int _index;
    private Timer _timer;

    public Player(Texture2D image, Texture2D _bulletImage) : base(image)
    {
        _frames = new Rectangle[2]
        {
            new Rectangle(0, 0, 256, 67), new Rectangle(256, 0, 256, 67)
        };
        _bullet = new Bullet(_bulletImage);
    }

    public override void Initialize()
    {
        _bounds.Width = _bounds.Width / _frames.Length;
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

    public override void Update(float deltaTime)
    {

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

        if (Input.GetKey(Keys.Space))
        {
            if (!_bullet.isVisible)
            {
                _bullet.Position = new Point(_bounds.X / 2 + _bounds.Width - 45, _bounds.Y + (_bounds.Height / 2) + 10);
                _bullet.isVisible = true;
            }
        }

        _timer.Update(deltaTime);
        _bullet.Update(deltaTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_image, _bounds, _frames[_index], Color.White);
        _bullet.Draw(spriteBatch);
    }
}
