using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Player : GameObject
{
    private const float SPEED_Y = 200;
    private SpriteEffects _orientation;
    private Bullet _bullet;

    public Player(Texture2D image, Texture2D _bulletImage) : base(image)
    {
        _bullet = new Bullet(_bulletImage);
    }

    public override void Initialize()
    {
        _bounds.X = 0;
        _bounds.Y = 200;
        _orientation = SpriteEffects.None;
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
                _bullet.Initialize(_bounds.X + _bounds.Width, _bounds.Y + (_bounds.Height / 2));
                _bullet.isVisible = true;
            }
        }

        _bullet.Update(deltaTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_image, _bounds, null, Color.White, 0, Vector2.Zero, _orientation, 0);
        _bullet.Draw(spriteBatch);
    }
}
