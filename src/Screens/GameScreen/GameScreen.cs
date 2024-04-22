using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class GameScreen : IScreen
{
  private GameObject _background;
  private Player _player;
  private FiristEnemy _firistEnemy;
  private SecondEnemy _secondEnemy;
  private Explosion _explosion;
  private Sounds _sounds;
  private Friend _friend;
  private FriendDead _friendDead;
  private Life _life;
  public int points = 0;
  public int saveds = 0;
  public int losts = 0;
  private SpriteFont _font;

  public void Initialize(int _points, int _saveds, int _losts)
  {
    points = 0;
    saveds = 0;
    losts = 0;
    _secondEnemy.Initialize();
    _firistEnemy.Initialize(_secondEnemy);
    _player.Initialize();
    _explosion.Initialize();
    _friend.Initialize();
    _friendDead.Initialize();
    _life.Initialize();
    _background.Initialize();

    _firistEnemy.ResetLocation();
    _secondEnemy.ResetLocation();

    _player.ClearAllBullets();
  }

  public void LoadContent(ContentManager content)
  {
    _sounds = new Sounds();
    _sounds.LoadContent(content);

    Texture2D _backgroundTexture = content.Load<Texture2D>("background");
    _background = new Background(_backgroundTexture);

    Texture2D _playerTexture = content.Load<Texture2D>("helicopter");
    Texture2D _bulletPlayerTexture = content.Load<Texture2D>("bullet");
    _player = new Player(_playerTexture, _bulletPlayerTexture);

    Texture2D _firistEnemyTexture = content.Load<Texture2D>("inimigo1");
    _firistEnemy = new FiristEnemy(_firistEnemyTexture);

    Texture2D _explosionTexture = content.Load<Texture2D>("explosao");
    _explosion = new Explosion(_explosionTexture);

    Texture2D _secondEnemyTexture = content.Load<Texture2D>("inimigo2");
    _secondEnemy = new SecondEnemy(_secondEnemyTexture);

    Texture2D _friendTexture = content.Load<Texture2D>("amigo");
    _friend = new Friend(_friendTexture);

    Texture2D _friendDeadTexture = content.Load<Texture2D>("amigo_morte");
    _friendDead = new FriendDead(_friendDeadTexture);

    Texture2D[] _lifesTextures = new Texture2D[]
    {
      content.Load<Texture2D>("lifes/energia1"),
      content.Load<Texture2D>("lifes/energia2"),
      content.Load<Texture2D>("lifes/energia3"),
    };
    _life = new Life(_lifesTextures);

    _font = content.Load<SpriteFont>("arial24");
  }

  public void Update(float deltaTime)
  {
    _background.Update(deltaTime);
    _player.Update(deltaTime, _sounds);
    _firistEnemy.Update(deltaTime, _secondEnemy, this);
    _explosion.Update(deltaTime);
    _secondEnemy.Update(deltaTime);
    foreach (var bullet in _player._bullets)
    {
      bullet.CheckCollision(_firistEnemy, _secondEnemy, CallbackBulletColisionExplosion);
    }
    _player.CheckCollision(_firistEnemy, _secondEnemy, _friend, CallbackPlayerCollisonEnemy, CallbackCollisionPlayerWithFriend);
    _friend.Update(deltaTime);
    _friend.CheckCollision(_secondEnemy, CallbackFriendCollisionEnemy);
    _friendDead.Update(deltaTime);
  }

  public void Draw(SpriteBatch _spriteBatch)
  {
    _background.Draw(_spriteBatch);
    _player.Draw(_spriteBatch);
    _firistEnemy.Draw(_spriteBatch);
    _explosion.Draw(_spriteBatch);
    _secondEnemy.Draw(_spriteBatch);
    _friend.Draw(_spriteBatch);
    _friendDead.Draw(_spriteBatch);
    _life.Draw(_spriteBatch);
    _spriteBatch.DrawString(_font, string.Format("Pontos: {0} Perdidos: {1} Salvos: {2}", points, losts, saveds), new Vector2(0, 0), Color.White);
  }

  private void Explosion(GameObject _obj)
  {
    _sounds.ExecuteSoundExplosion();
    _explosion.Position = _obj.Position;
    _explosion._isVisible = true;
  }

  private void CallbackBulletColisionExplosion(GameObject _obj)
  {
    foreach (var bullet in _player._bullets)
    {
      if(bullet.Bounds.Intersects(_obj.Bounds)){
        bullet._isVisible = false;
        bullet.Position = new Point(0, 0);
        Explosion(_obj);
        points += 50;
        break;
      }
    }
  }

  private void CallbackPlayerCollisonEnemy(GameObject _obj)
  {
    Explosion(_obj);
    DecreasePoints(25);
    _life.DecreaseLife();
  }

  private void CallbackCollisionPlayerWithFriend()
  {
    _sounds.ExecuteSoundRescue();
    saveds++;
    points += 50;
    GetLifeRandom();
  }

  private void GetLifeRandom(){
    double randomNumber = new Random().NextDouble();
    if(randomNumber > 0.7){
      _life.IncrementLife();
    }
  }

  private void CallbackFriendCollisionEnemy(Rectangle _posision)
  {
    _sounds.ExecuteSoundDead();
    _friendDead.Position = _friend.Position;
    _friendDead._isVisible = true;
    DecreasePoints(100);
    losts++;
  }

  public void DecreasePoints(int point){
      points -= point;
      if(points < 0) points = 0;
  }

  public (int saveds, int points, int losts) GetParameters()
  {
    return (saveds, points, losts);
  }
}