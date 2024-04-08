using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Bullet : GameObject
{
  private const float SPEED = 200;
  public bool isVisible = false;

  private SpriteEffects _orientation;

  public Bullet(Texture2D image) : base(image)
  {
  }

  public void Initialize(int _x, int _y)
  {
    _bounds.X = _x;
    _bounds.Y = _y;
    _orientation = SpriteEffects.None;
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
      spriteBatch.Draw(_image, _bounds, null, Color.White, 0, Vector2.Zero, _orientation, 0);
    }
  }
}