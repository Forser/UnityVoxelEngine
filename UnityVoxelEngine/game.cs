using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace UnityVoxelEngine
{
    public class game : Game
    {
        BasicEffect effect;
        GraphicsDeviceManager graphics;
        VertexBuffer buffer; 
        IndexBuffer indexBuffer;
        VertexPositionColor[] vertexList;
        int[] indexList;

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

            position = new Vector3(-2.0f, -2.0f, 2.0f);
            
            Chunk myChunk = new Chunk();

            MeshData meshData = myChunk.Start();

            vertexList = new VertexPositionColor[meshData.vertices.Count];
            for (int i = 0; i < meshData.vertices.Count; i++ )
            {
                vertexList[i] = meshData.vertices[i];
            }

            indexList = new int[meshData.triangles.Count];
            for (int i = 0; i < meshData.triangles.Count; i++ )
            {
                indexList[i] = meshData.triangles[i];
            }


            buffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), vertexList.Length, BufferUsage.WriteOnly);
            buffer.SetData<VertexPositionColor>(vertexList);

            indexBuffer = new IndexBuffer(graphics.GraphicsDevice, typeof(int), indexList.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indexList);

            effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 0.001f, 1000.0f);
            effect.View = Matrix.CreateLookAt(new Vector3(0, 0, -25), Vector3.Forward, Vector3.Up);
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
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertexList.Length, 0, indexList.Length / 3);
            }

            base.Draw(gameTime);
        }
    }
}