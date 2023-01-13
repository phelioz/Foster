namespace Foster.Framework.Components
{
    public class Sprite : IComponent
    {
        public string? TexturePath;

        public Sprite()
        {

        }

        public Sprite(string texturePath)
        {
            this.TexturePath = texturePath;
        }
    }
}
