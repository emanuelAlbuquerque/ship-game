// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;

// public class Explosion : GameObject
// {
//   public bool isVisible = false;

//   public Explosion(Texture2D image) : base(image)
//   {
//   }

//   public override void Initialize()
//   {
//   }

//   public override void Update(float deltaTime)
//   {
//     if (isVisible)
//     {
//       _bounds.X = _bounds.X + (int)(SPEED * deltaTime);

//       if (_bounds.X >= Globals.SCREEN_WIDTH)
//       {
//         isVisible = false;
//       }
//     }
//   }

//   public override void Draw(SpriteBatch spriteBatch)
//   {
//     if (isVisible)
//     {
//       spriteBatch.Draw(_image, _bounds, null, Color.White, 0, Vector2.Zero, _orientation, 0);
//     }
//   }
// }