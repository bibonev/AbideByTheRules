/**
 * Boyan Bonev @2011/2012
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Game.Classes;

namespace Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainClass : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Matrix viewMatrix, projectionMatrix;
        Matrix worldMatrix, worldMatrixbb, worldMatrixSl;
        Matrix idnetity = Matrix.Identity;

        // Create lists for models and vectors
        private List<Vector3> vectors = new List<Vector3>(4);
        private List<Model> models = new List<Model>(4);

        //Declare models:
        Model firstMap;
        Model leftHand;
        Model rightHand;
        Model bicycle;
        Model slideway;
        //---

        // Declare vectors:
        Vector3 mapPosition = Vector3.Zero;
        Vector3 slidewayPosition = Vector3.Zero;
        Vector3 leftHandPosition = new Vector3(-70.0f, -20.0f, 722.0f);
        Vector3 rightHandPosition = new Vector3(-70.0f, -20.0f, 722.0f);
        Vector3 bicyclePosition = new Vector3(-70.0f, -8.0f, 722.0f);
        //---

        private static Quaternion bicycleRotation = Quaternion.Identity;

        float bicycleSpeed = 0.0f;
        private static float createRotation = MathHelper.Pi;
        float modelsRotation = 0.0f;
        float aspectRatio;

        List<BoundingBox> slidewayBox;
        List<BoundingBox> bicycleBox;

        public MainClass()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Първо ниво.";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            firstMap = Content.Load<Model>("FMap");
            leftHand = Content.Load<Model>("Bicycle//LeftHand");
            rightHand = Content.Load<Model>("Bicycle//RightHand");
            bicycle = Content.Load<Model>("Bicycle//Bicycle");
            slideway = Content.Load<Model>("Slideway");

            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;
            // Set vectors:
            vectors.Add(mapPosition);
            vectors.Add(leftHandPosition);
            vectors.Add(rightHandPosition);
            vectors.Add(bicyclePosition);
            vectors.Add(slidewayPosition);
            //Set models:
            models.Add(firstMap);
            models.Add(leftHand);
            models.Add(rightHand);
            models.Add(bicycle);
            models.Add(slideway);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected List<BoundingBox> UpdateBoundingBox(Model model, Matrix worldTransform)
        {
            List<BoundingBox> boxes = new List<BoundingBox>();

            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);
            // For each mesh of the model
            foreach (ModelMesh mesh in model.Meshes)
            {
                Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
                Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    int vertexStride = part.VertexBuffer.VertexDeclaration.VertexStride;
                    int vertexBufferSize = part.NumVertices * vertexStride;

                    // Get vertex data as float
                    float[] vertexData = new float[vertexBufferSize / sizeof(float)];
                    part.VertexBuffer.GetData<float>(vertexData);

                    // Iterate through vertices's (possibly) growing bounding box, all calculations are done in world space
                    for (int i = 0; i < vertexBufferSize / sizeof(float); i += vertexStride / sizeof(float))
                    {
                        Vector3 transformedPosition = Vector3.Transform(new Vector3(vertexData[i], vertexData[i + 1], vertexData[i + 2]), transforms[mesh.ParentBone.Index]);
                        transformedPosition = Vector3.Transform(transformedPosition, worldTransform);

                        min = Vector3.Min(min, transformedPosition);
                        max = Vector3.Max(max, transformedPosition);
                    }
                }
                boxes.Add(new BoundingBox(min, max));
            }

            return boxes;
        }

        private void ProcessKeyboard(GameTime gameTime)
        {
            float leftRightRot = 0;
            float turningSpeed = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
            turningSpeed *= 1.6f;

            KeyboardState keys = Keyboard.GetState();
            if (keys.IsKeyDown(Keys.Right))
            {
                leftRightRot += turningSpeed;
            }
            if (keys.IsKeyDown(Keys.Left))
            {
                leftRightRot -= turningSpeed;
            }

            if (keys.IsKeyDown(Keys.Up))
            {
                if (bicycleSpeed < 1.0f)
                {
                    bicycleSpeed += 4.15f;
                }
            }
            else
            {
                if (bicycleSpeed > 0.0f)
                {
                    bicycleSpeed -= 4.15f;
                }
            }

            Quaternion additionalRot = Quaternion.CreateFromAxisAngle(new Vector3(0, -1, 0), leftRightRot);
            bicycleRotation *= additionalRot;
        }

        private void MoveForward(ref Vector3 position, Quaternion rotationQuat, float speed)
        {
            Vector3 addVector = Vector3.Transform(new Vector3(0, 0, -1), rotationQuat);
            position += addVector * speed;
        }

        private void UpdateCamera()
        {
            Vector3 campos = new Vector3(0, 0.1f, 0.6f);
            campos = Vector3.Transform(campos, Matrix.CreateFromQuaternion(bicycleRotation));
            campos += Vector3.Transform(new Vector3(0, 150, 80), bicycleRotation);
            campos += bicyclePosition;

            Vector3 camup = new Vector3(0, 1, 0);
            camup = Vector3.Transform(camup, Matrix.CreateFromQuaternion(bicycleRotation));

            Vector3 cameraTarget = bicyclePosition + new Vector3(0, 120, 0);

            viewMatrix = Matrix.CreateLookAt(campos, cameraTarget, camup);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, graphics.GraphicsDevice.Viewport.AspectRatio, 0.01f, 10000.0f);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        ///
        
        protected override void Update(GameTime gameTime)
        {   
            // Types of KeyboardState
            KeyboardState currentKey = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || currentKey.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            
            List<ModelMesh> bicycleMesh = new List<ModelMesh>();
            List<ModelMesh> mapMesh = new List<ModelMesh>();
            List<ModelMesh> slidewayMesh = new List<ModelMesh>();

            AddElements(bicycle, ref bicycleMesh);
            AddElements(firstMap, ref mapMesh);
            AddElements(slideway, ref slidewayMesh);

            ProcessKeyboard(gameTime);
            MoveForward(ref bicyclePosition, bicycleRotation, bicycleSpeed);
            UpdateCamera();

            slidewayBox = UpdateBoundingBox(slideway, Matrix.Identity);
            bicycleBox = UpdateBoundingBox(firstMap, Matrix.Identity);
            
            if (CollisonDetteection(bicycleBox, slidewayBox))
            {
                Window.Title = "Seche";
            }
            else 
            {
                Window.Title = "NE Seche";
            }

            base.Update(gameTime);
        }


        private bool CollisonDetteection(List<BoundingBox> c1, List<BoundingBox> c2)
        {
            for (int index1 = 0; index1 < c1.Count; index1++)
            {
                for (int index2 = 0; index2 < c2.Count; index2++)
                {
                 
                    if (c1[index1].Intersects(c2[index2]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
            
        // Set the position of the camera in world space, for our view matrix.
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            DrawModel.DrawBicycle(models, viewMatrix, projectionMatrix, createRotation, bicycleRotation, bicyclePosition, ref worldMatrixbb, 3);
            DrawModel.DrawRoad(models[0], vectors[0], viewMatrix, projectionMatrix);
            DrawModel.DrawRoad(models[4], vectors[4], viewMatrix, projectionMatrix);

            base.Draw(gameTime);
        }

        private void AddElements(Model meshes, ref List<ModelMesh> collection)
        {
            foreach (ModelMesh item in meshes.Meshes)
            {
                collection.Add(item); 
            }
        }
    }
}