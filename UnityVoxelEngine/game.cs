using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace UnityVoxelEngine
{
    public class game : Game
    {
        BasicEffect effect;
        float rotY, rotZ;
        GraphicsDeviceManager graphics;
        IndexBuffer indexBuffer;
        int[] indexList;
        Texture2D texture;
        Vector3 position;
        VertexBuffer buffer; 
        VertexPositionNormalTexture[] vertexList;

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
            rotZ += deltaTime - 0.001f;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            effect = new BasicEffect(GraphicsDevice);

            position = new Vector3(-0.0f, 0, -0.0f);
            
            Chunk myChunk = new Chunk();

            MeshData meshData = myChunk.Start();

            GenerateBuffers(meshData);

            buffer = new VertexBuffer(GraphicsDevice, VertexPositionNormalTexture.VertexDeclaration, vertexList.Length, BufferUsage.WriteOnly);
            buffer.SetData(vertexList);

            indexBuffer = new IndexBuffer(graphics.GraphicsDevice, typeof(int), indexList.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indexList);

            effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 0.01f, 1000.0f);
            effect.View = Matrix.CreateLookAt(new Vector3(3, 12, 2), new Vector3(1,0,1.5f), new Vector3(0,4,0));
            effect.World = Matrix.Identity * Matrix.CreateRotationY(rotY) * Matrix.CreateRotationX(rotZ) * Matrix.CreateTranslation(position);
            //effect.World = Matrix.Identity;
            effect.VertexColorEnabled = true;
             
            GraphicsDevice.SetVertexBuffer(buffer);
            GraphicsDevice.Indices = indexBuffer;

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            //rasterizerState.FillMode = FillMode.WireFrame;
            GraphicsDevice.RasterizerState = rasterizerState;

            foreach(EffectPass pass in effect.CurrentTechnique.Passes)
            {
                //effect.LightingEnabled = true;
                //effect.DirectionalLight0.DiffuseColor = new Vector3(1f, 0.8f, 0.5f);
                //effect.DirectionalLight0.Direction = new Vector3(1, 0.5f, 0);
                //effect.DirectionalLight0.Enabled = true;

                pass.Apply();
                GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertexList, 0, vertexList.Length, indexList, 0, indexList.Length / 3);
            }

            base.Draw(gameTime);
        }

        private void GenerateBuffers(MeshData meshData)
        {
            vertexList = new VertexPositionNormalTexture[meshData.vertices.Count];
            for (int i = 0; i < meshData.vertices.Count; i++)
            {
                vertexList[i] = meshData.vertices[i];
            }

            indexList = new int[meshData.triangles.Count];

            for (int i = 0; i < meshData.triangles.Count; i++)
            {
                indexList[i] = meshData.triangles[i];
            }

            for (int i = 0; i < vertexList.Length; i++)
            {
                vertexList[i].Normal = new Vector3(0,0,0);
            }

            for (int i = 0; i < indexList.Length / 3; i++)
            {
                int index1 = indexList[i * 3];
                int index2 = indexList[i * 3 + 1];
                int index3 = indexList[i * 3 + 2];

                Vector3 side1 = vertexList[index1].Position - vertexList[index3].Position;
                Vector3 side2 = vertexList[index1].Position - vertexList[index2].Position;
                Vector3 normal = Vector3.Cross(side1, side2);

                vertexList[index1].Normal += normal;
                vertexList[index2].Normal += normal;
                vertexList[index3].Normal += normal;
            }

            for (int i = 0; i < vertexList.Length; i++)
            {
                vertexList[i].Normal.Normalize();
            }
        }
    }
}