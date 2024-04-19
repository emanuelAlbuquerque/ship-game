using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

public class Sounds
{
  private SoundEffect _soundExplosion, _shot, _rescue, _dead;

  public void LoadContent(ContentManager _content)
  {
    _soundExplosion = _content.Load<SoundEffect>("sounds/explosao");
    _shot = _content.Load<SoundEffect>("sounds/disparo");
    _rescue = _content.Load<SoundEffect>("sounds/resgate");
    _dead = _content.Load<SoundEffect>("sounds/perdido");
  }

  public void ExecuteSoundExplosion()
  {
    _soundExplosion.Play();
  }

  public void ExecuteSoundShot()
  {
    _shot.Play();
  }

  public void ExecuteSoundRescue()
  {
    _rescue.Play();
  }

  public void ExecuteSoundDead()
  {
    _dead.Play();
  }
}