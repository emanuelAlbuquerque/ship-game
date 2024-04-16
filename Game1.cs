using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ship_game;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameObject _background;
    private Player _player;
    private FiristEnemy _firistEnemy;
    private SecondEnemy _secondEnemy;
    private Explosion _explosion;
    private Sounds _sounds;
    private Friend _friend;
    private FriendDead _friendDead;
    private Life _life;
    private int points = 0;
    private int saveds = 0;
    private int losts = 0;

    private SpriteFont _font;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();

        Globals.SCREEN_WIDTH = _graphics.PreferredBackBufferWidth;
        Globals.SCREEN_HEIGHT = _graphics.PreferredBackBufferHeight;

        _secondEnemy.Initialize();
        _firistEnemy.Initialize(_secondEnemy);
        _player.Initialize();
        _explosion.Initialize();
        _friend.Initialize();
        _friendDead.Initialize();
        _life.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _sounds = new Sounds();
        _sounds.LoadContent(Content);

        Texture2D _backgroundTexture = Content.Load<Texture2D>("background");
        _background = new Background(_backgroundTexture);

        Texture2D _playerTexture = Content.Load<Texture2D>("helicopter");
        Texture2D _bulletPlayerTexture = Content.Load<Texture2D>("bullet");
        _player = new Player(_playerTexture, _bulletPlayerTexture);

        Texture2D _firistEnemyTexture = Content.Load<Texture2D>("inimigo1");
        _firistEnemy = new FiristEnemy(_firistEnemyTexture);

        Texture2D _explosionTexture = Content.Load<Texture2D>("explosao");
        _explosion = new Explosion(_explosionTexture);

        Texture2D _secondEnemyTexture = Content.Load<Texture2D>("inimigo2");
        _secondEnemy = new SecondEnemy(_secondEnemyTexture);

        Texture2D _friendTexture = Content.Load<Texture2D>("amigo");
        _friend = new Friend(_friendTexture);

        Texture2D _friendDeadTexture = Content.Load<Texture2D>("amigo_morte");
        _friendDead = new FriendDead(_friendDeadTexture);

        Texture2D[] _lifesTextures = new Texture2D[]
        {
            Content.Load<Texture2D>("lifes/energia1"),
            Content.Load<Texture2D>("lifes/energia2"),
            Content.Load<Texture2D>("lifes/energia3"),
        };
        _life = new Life(_lifesTextures);

        _font = Content.Load<SpriteFont>("arial24");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        _background.Update(deltaTime);
        _player.Update(deltaTime, _sounds);
        _firistEnemy.Update(deltaTime, _secondEnemy);
        _explosion.Update(deltaTime);
        _secondEnemy.Update(deltaTime);
        _player._bullet.CheckCollision(_firistEnemy, _secondEnemy, CallbackBulletColisionExplosion);
        _player.CheckCollision(_firistEnemy, _secondEnemy, _friend, CallbackPlayerCollisonEnemy, CallbackCollisionPlayerWithFriend);
        _friend.Update(deltaTime);
        _friend.CheckCollision(_secondEnemy, CallbackFriendCollisionEnemy);
        _friendDead.Update(deltaTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _background.Draw(_spriteBatch);
        _player.Draw(_spriteBatch);
        _firistEnemy.Draw(_spriteBatch);
        _explosion.Draw(_spriteBatch);
        _secondEnemy.Draw(_spriteBatch);
        _friend.Draw(_spriteBatch);
        _friendDead.Draw(_spriteBatch);
        _life.Draw(_spriteBatch);
        _spriteBatch.DrawString(_font, string.Format("Pontos: {0} Perdidos: {1} Salvos: {2}", points, losts, saveds), new Vector2(0, 0), Color.White);


        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void Explosion(GameObject _obj)
    {
        _sounds.ExecuteSoundExplosion();
        _explosion.Position = _obj.Position;
        _explosion._isVisible = true;
    }

    private void CallbackBulletColisionExplosion(GameObject _obj)
    {
        _player._bullet._isVisible = false;
        _player._bullet.Position = new Point(0, 0);
        Explosion(_obj);
        points += 50;
    }

    private void CallbackPlayerCollisonEnemy(GameObject _obj)
    {
        Explosion(_obj);
        _life.DecreaseLife();
    }

    private void CallbackCollisionPlayerWithFriend()
    {
        _sounds.ExecuteSoundRescue();
        saveds++;
        points += 50;
    }

    private void CallbackFriendCollisionEnemy(Rectangle _posision)
    {
        _sounds.ExecuteSoundDead();
        _friendDead.Position = _friend.Position;
        _friendDead._isVisible = true;
        losts++;
    }
}
