using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FriendDead : GameObject
{
  public bool _isVisible = false;
  private Rectangle[] _frames;
  private int _index;
  private Timer _timer;

  public FriendDead(Texture2D image) : base(image)
  {
    int frameWidth = 44;
    int totalFrames = 308 / frameWidth;

    _frames = new Rectangle[totalFrames];

    for (int i = 0; i < totalFrames; i++)
    {
      int frameX = i * frameWidth;
      _frames[i] = new Rectangle(frameX, 0, frameWidth, 51);
    }
  }

  public override void Initialize()
  {
    _bounds.Width = _bounds.Width / _frames.Length;
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

    if (_index > 4)
    {
      _index = 0;
      _isVisible = false;
    }
  }
}