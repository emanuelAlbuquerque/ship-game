using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public interface IScreen
{
    void LoadContent(ContentManager content);
    void Initialize(int _points, int _saveds, int _losts);
    void Update(float deltaTime);
    void Draw(SpriteBatch spriteBatch);

    (int saveds, int points, int losts) GetParameters();
}
