using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Bullet : GameObject
{
  private const float SPEED = 250;
  public bool _isVisible = false;

  public Bullet(Texture2D image) : base(image)
  {
  }

  public override void Initialize()
  {
  }

  public override void Update(float deltaTime)
  {
    if (_isVisible)
    {
      _bounds.X = _bounds.X + (int)(SPEED * deltaTime);

      if (_bounds.X >= Globals.SCREEN_WIDTH)
      {
        _isVisible = false;
      }
    }
  }

  public override void Draw(SpriteBatch spriteBatch)
  {
    if (_isVisible)
    {
      spriteBatch.Draw(_image, _bounds, Color.White);
    }
  }

  public void CheckCollision(FiristEnemy _firistEnemy, SecondEnemy _secondEnemy, Action<GameObject> _callbackBulletColisionExplosion)
  {
    if (_bounds.Intersects(_firistEnemy.Bounds) && _firistEnemy._isVisible && _isVisible)
    {
      _firistEnemy._isVisible = false;
      _callbackBulletColisionExplosion.Invoke(_firistEnemy);
    }

    if (_bounds.Intersects(_secondEnemy.Bounds) && _secondEnemy._isVisible && _isVisible)
    {
      _secondEnemy._isVisible = false;
      _callbackBulletColisionExplosion.Invoke(_secondEnemy);
    }
  }
}