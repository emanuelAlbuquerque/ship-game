using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Explosion : GameObject
{
  public bool _isVisible = false;
  private Rectangle[] _frames;
  private int _frameWidth = 45;
  private int _index;
  private Timer _timer;

  public Explosion(Texture2D image) : base(image)
  {
    int frameHeight = 87;
    int totalFrames = 5;

    _frames = new Rectangle[totalFrames];
    for (int i = 0; i < totalFrames; i++)
    {
      _frames[i] = new Rectangle(i * _frameWidth, 0, i * _frameWidth, frameHeight);
    }
  }

  public override void Initialize()
  {
    _bounds.Width = _frameWidth;
    _isVisible = false;
    _index = 0;
    _timer = new Timer();
    _timer.Start(IncraseIndex, 0.1f, true);
  }

  public override void Update(float deltaTime)
  {
    if (_isVisible)
    {
      _timer.Update(deltaTime);
    }
  }

  public override void Draw(SpriteBatch spriteBatch)
  {
    if (_isVisible)
    {
      spriteBatch.Draw(_image, _bounds, _frames[_index], Color.White);
    }
  }

  public void IncraseIndex()
  {
    _index++;
    _bounds.Width = _frameWidth * _index;

    if (_index > 4)
    {
      _index = 0;
      _isVisible = false;
    }
  }
}