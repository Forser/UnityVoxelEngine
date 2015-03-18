using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UnityVoxelEngine.Chunks
{
    public class game : Game
    {
        public static int chunkSize = 16;
        public bool update = true;
        VertexBuffer buffer;
        BasicEffect effect;
        GraphicsDeviceManager graphics;
        VertexPositionColor[] verts;
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
            effect = new BasicEffect(GraphicsDevice);
            
            verts = new VertexPositionColor[8];
            //front left bottom corner
            verts[0] = new VertexPositionColor(new Vector3(0, 0, 0), Color.Red);
            //front left upper corner
            verts[1] = new VertexPositionColor(new Vector3(0, 1f, 0), Color.Blue);
            //front right upper corner
            verts[2] = new VertexPositionColor(new Vector3(1f, 1f, 0), Color.Black);
            //front lower right corner
            verts[3] = new VertexPositionColor(new Vector3(1f, 0, 0), Color.Purple);
            //back left lower corner
            verts[4] = new VertexPositionColor(new Vector3(0, 0, -1f), Color.White);
            //back left upper corner
            verts[5] = new VertexPositionColor(new Vector3(0, 1f, -1f), Color.Yellow);
            //back right upper corner
            verts[6] = new VertexPositionColor(new Vector3(1f, 1f, -1f), Color.Green);
            //back right lower corner
            verts[7] = new VertexPositionColor(new Vector3(1f, 0, -1f), Color.Pink);

            buffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 12, BufferUsage.WriteOnly);
            buffer.SetData<VertexPositionColor>(verts);

            position = new Vector3(0.0f, 0.0f, 5.0f);

            short[] indices = new short[36];
            indices[0] = 0;
            indices[1] = 3;
            indices[2] = 2;
            //top left triangle
            indices[3] = 2;
            indices[4] = 1;
            indices[5] = 0;
            //back face
            //bottom right triangle
            indices[6] = 4;
            indices[7] = 7;
            indices[8] = 6;
            //top left triangle
            indices[9] = 6;
            indices[10] = 5;
            indices[11] = 4;
            //Top face
            //bottom right triangle
            indices[12] = 1;
            indices[13] = 2;
            indices[14] = 6;
            //top left triangle
            indices[15] = 6;
            indices[16] = 5;
            indices[17] = 1;
            //bottom face
            //bottom right triangle
            indices[18] = 4;
            indices[19] = 7;
            indices[20] = 3;
            //top left triangle
            indices[21] = 3;
            indices[22] = 0;
            indices[23] = 4;
            //left face
            //bottom right triangle
            indices[24] = 4;
            indices[25] = 0;
            indices[26] = 1;
            //top left triangle
            indices[27] = 1;
            indices[28] = 5;
            indices[29] = 4;
            //right face
            //bottom right triangle
            indices[30] = 3;
            indices[31] = 7;
            indices[32] = 6;
            //top left triangle
            indices[33] = 6;
            indices[34] = 2;
            indices[35] = 3;

            indexBuffer = new IndexBuffer(graphics.GraphicsDevice, IndexElementSize.SixteenBits, sizeof(short) * indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            rotY += deltaTime;
            rotZ += deltaTime - 0.001f;
            rotX += deltaTime + 0.001f;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Start();

            effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 0.001f, 1000.0f);
            effect.View = Matrix.CreateLookAt(new Vector3(0, 0, -5), Vector3.Forward, Vector3.Up);
            effect.World = Matrix.Identity * Matrix.CreateRotationY(rotY) * Matrix.CreateRotationZ(rotZ) * Matrix.CreateRotationX(rotX) * Matrix.CreateTranslation(position);
            effect.VertexColorEnabled = true;

            GraphicsDevice.Indices = indexBuffer;
            GraphicsDevice.SetVertexBuffer(buffer);

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;

            foreach(EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 12, 0, 20);
            }

            base.Draw(gameTime);
        }
    }
}