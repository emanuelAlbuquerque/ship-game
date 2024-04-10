using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

public class Sounds
{
  private SoundEffect _soundExplosion, _shot;

  public void LoadContent(ContentManager _content)
  {
    _soundExplosion = _content.Load<SoundEffect>("sounds/explosao");
    _shot = _content.Load<SoundEffect>("sounds/disparo");
  }

  public void ExecuteSoundExplosion()
  {
    _soundExplosion.Play();
  }

  public void ExecuteSoundShot()
  {
    _shot.Play();
  }
}