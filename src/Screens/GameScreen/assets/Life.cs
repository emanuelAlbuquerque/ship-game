using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Life : GameObject
{
  private Texture2D[] _lifesTexture;
  private const int TOTAL_LIFE = 3;
  private int _life = TOTAL_LIFE;

  public Life(Texture2D[] images) : base(images[0])
  {
    _lifesTexture = images;
  }

  public override void Initialize()
  {
    _life = TOTAL_LIFE;
    _bounds.X = Globals.SCREEN_WIDTH - _bounds.Width - 5;
    _bounds.Y = 5;
  }

  public override void Update(float deltaTime)
  {
  }

  public override void Draw(SpriteBatch spriteBatch)
  {
    if (_life > 0)
    {
      spriteBatch.Draw(_lifesTexture[_life - 1], _bounds, Color.White);
    }
  }

  public void DecreaseLife()
  {
    _life--;

    if (_life == 0)
    {
      Globals.GameInstance.ChangeScreen(EScreen.GameOver);
    }
  }

  public void IncrementLife(){
    if(_life < 3){
      _life++;
    }
  }

  public int GetLife()
  {
    return _life;
  }

  public void ResetLife()
  {
    _life = TOTAL_LIFE;
  }
}