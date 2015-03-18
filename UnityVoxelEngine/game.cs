using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UnityVoxelEngine
{
    public class game : Game
    {
        BasicEffect effect;
        GraphicsDeviceManager graphics;
        VertexBuffer buffer; 
        IndexBuffer indexBuffer;

        Vector3 position;
        float rotY, rotZ, rotX;

        public game() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
           
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            rotY += deltaTime;
            //rotZ += deltaTime - 0.001f;
            //rotX += deltaTime + 0.001f;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            effect = new BasicEffect(GraphicsDevice);

            position = new Vector3(0.0f, 0.0f, 5.0f);
            
            Chunk myChunk = new Chunk();

            MeshData meshData = myChunk.Start();

            buffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 24, BufferUsage.WriteOnly);
            buffer.SetData<VertexPositionColor>(meshData.vertices);

            indexBuffer = new IndexBuffer(graphics.GraphicsDevice, IndexElementSize.SixteenBits, sizeof(int) * meshData.triangles.Count, BufferUsage.WriteOnly);
            indexBuffer.SetData(meshData.triangles.ToArray());

            effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 0.001f, 1000.0f);
            effect.View = Matrix.CreateLookAt(new Vector3(0, 0, -15), Vector3.Forward, Vector3.Up);
            effect.World = Matrix.Identity * Matrix.CreateRotationY(rotY) * Matrix.CreateTranslation(position);
            effect.VertexColorEnabled = true;

            
            GraphicsDevice.SetVertexBuffer(buffer);
            GraphicsDevice.Indices = indexBuffer;

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;

            foreach(EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 24, 0, 36);
            }

            base.Draw(gameTime);
        }
    }
}