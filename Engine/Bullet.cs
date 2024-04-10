using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Bullet : GameObject
{
  private const float SPEED = 250;
  public bool isVisible = false;

  public Bullet(Texture2D image) : base(image)
  {
  }

  public override void Initialize()
  {
  }

  public override void Update(float deltaTime)
  {
    if (isVisible)
    {
      _bounds.X = _bounds.X + (int)(SPEED * deltaTime);

      if (_bounds.X >= Globals.SCREEN_WIDTH)
      {
        isVisible = false;
      }
    }
  }

  public override void Draw(SpriteBatch spriteBatch)
  {
    if (isVisible)
    {
      spriteBatch.Draw(_image, _bounds, Color.White);
    }
  }
  
  public void CheckCollision(GameObject _firistEnemy, Action _callbackExplosionFiristEnemy)
  {
    if (_bounds.Intersects(_firistEnemy.Bounds))
    {
      _callbackExplosionFiristEnemy.Invoke();
    }
  }
}