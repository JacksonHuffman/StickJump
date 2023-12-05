using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameProject0
{
    public class CoinCube
    {
        private VertexBuffer _vertexBuffer;

        private IndexBuffer _indexBuffer;

        private BasicEffect _basicEffect;

        Game game;

        /// <summary>
        /// Initialize the vertex buffer
        /// </summary>
        public void InitializeVertices()
        {
            var vertexData = new VertexPositionColor[] {
                new VertexPositionColor() { Position = new Vector3(-0.25f,  0.25f, -0.25f), Color = Color.Teal },
                new VertexPositionColor() { Position = new Vector3( 0.25f,  0.25f, -0.25f), Color = Color.Gold },
                new VertexPositionColor() { Position = new Vector3(-0.25f, -0.25f, -0.25f), Color = Color.Teal },
                new VertexPositionColor() { Position = new Vector3( 0.25f, -0.25f, -0.25f), Color = Color.Gold },
                new VertexPositionColor() { Position = new Vector3(-0.25f,  0.25f,  0.25f), Color = Color.Teal },
                new VertexPositionColor() { Position = new Vector3( 0.25f,  0.25f,  0.25f), Color = Color.Gold },
                new VertexPositionColor() { Position = new Vector3(-0.25f, -0.25f,  0.25f), Color = Color.Teal },
                new VertexPositionColor() { Position = new Vector3( 0.25f, -0.25f,  0.25f), Color = Color.Gold }
            };
            _vertexBuffer = new VertexBuffer(
                game.GraphicsDevice,            // The graphics device to load the buffer on 
                typeof(VertexPositionColor),    // The type of the vertex data 
                8,                              // The count of the vertices 
                BufferUsage.None                // How the buffer will be used
            );
            _vertexBuffer.SetData<VertexPositionColor>(vertexData);
        }

        /// <summary>
        /// Initializes the index buffer
        /// </summary>
        public void InitializeIndices()
        {
            var indexData = new short[]
            {
            0, 1, 2, // Side 0
            2, 1, 3,
            4, 0, 6, // Side 1
            6, 0, 2,
            7, 5, 6, // Side 2
            6, 5, 4,
            3, 1, 7, // Side 3 
            7, 1, 5,
            4, 5, 0, // Side 4 
            0, 5, 1,
            3, 7, 2, // Side 5 
            2, 7, 6
            };
            _indexBuffer = new IndexBuffer(
                game.GraphicsDevice,            // The graphics device to use
                IndexElementSize.SixteenBits,   // The size of the index 
                36,                             // The count of the indices
                BufferUsage.None                // How the buffer will be used
            );
            _indexBuffer.SetData<short>(indexData);
        }

        /// <summary>
        /// Initializes the BasicEffect to render our cube
        /// </summary>
        void InitializeEffect()
        {
            _basicEffect = new BasicEffect(game.GraphicsDevice);
            _basicEffect.World = Matrix.CreateTranslation(new Vector3(0, -5, 0));
            _basicEffect.View = Matrix.CreateLookAt(
                new Vector3(0, 0, 4), // The camera position
                new Vector3(0, 0, 0), // The camera target,
                Vector3.Down           // The camera up vector
            );
            _basicEffect.Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,                         // The field-of-view 
                game.GraphicsDevice.Viewport.AspectRatio,   // The aspect ratio
                0.1f, // The near plane distance 
                100.0f // The far plane distance
            );
            _basicEffect.VertexColorEnabled = true;
        }

        /// <summary>
        /// Draws the Cube
        /// </summary>
        public void Draw()
        {
            // apply the effect 
            _basicEffect.CurrentTechnique.Passes[0].Apply();
            // set the vertex buffer
            game.GraphicsDevice.SetVertexBuffer(_vertexBuffer);
            // set the index buffer
            game.GraphicsDevice.Indices = _indexBuffer;
            // Draw the triangles
            game.GraphicsDevice.DrawIndexedPrimitives(
                PrimitiveType.TriangleList, // Tye type to draw
                0,                          // The first vertex to use
                0,                          // The first index to use
                12                          // the number of triangles to draw
            );
        }

        /// <summary>
        /// Updates the Cube
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            float angle = (float)gameTime.TotalGameTime.TotalSeconds;
            // Look at the cube from farther away while spinning around it
            _basicEffect.View = Matrix.CreateRotationY(angle) * Matrix.CreateLookAt(
                new Vector3(0, 5, -10),
                Vector3.Zero,
                Vector3.Down
            );
        }

        /// <summary>
        /// Constructs a cube instance
        /// </summary>
        /// <param name="game">The game that is creating the cube</param>
        public CoinCube(Game game)
        {
            this.game = game;
            InitializeVertices();
            InitializeIndices();
            InitializeEffect();
        }
    }
}
