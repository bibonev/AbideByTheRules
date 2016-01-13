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
using System.Diagnostics;

namespace Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainClass : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Matrixes
        Matrix viewMatrix, projectionMatrix;
        Matrix worldMatrix, worldMatrixbb, worldMatrixSl;
        Matrix identity = Matrix.Identity;

        //Fonts
        SpriteFont font;

        // Create lists for models and vectors
        List<Model> models = new List<Model>();
        List<Model> crossroadParts = new List<Model>();

        //Array for .fbx files:
        string[] nameFBXFiles = { "partOne", "partTwo", "partThree", "partFour", "partFive", "partSix", "partSeven", "partEight", "partNine", "partTen", "partEleven", "partTwelfe", "partThirteen", "partFourteen", "partFifteen", "partSixteen", "partSeventeen", "partEighteen", "partNineteen", "partTwenty", "partTwentyone", "partTwentytwo", "partTwentytree", "partTwentyfour"};

        //Declare models:
        Model firstMap;
        Model leftHand;
        Model rightHand;
        Model bicycle;
        Model crossRoad00, crossRoad01, crossRoad02, crossRoad03, crossRoad04, crossRoad05, crossRoad06, crossRoad07, crossRoad08, crossRoad09, crossRoad10, crossRoad11, crossRoad12, crossRoad13, crossRoad14, crossRoad15, crossRoad16, crossRoad17, crossRoad18, crossRoad19, crossRoad20, crossRoad21, crossRoad22, crossRoad23;
        //---

        // Declare vectors:
        Vector3 mapPosition = Vector3.Zero;
        Vector3 leftHandPosition = new Vector3(-70.0f, -20.0f, 722.0f);
        Vector3 rightHandPosition = new Vector3(-70.0f, -20.0f, 722.0f);
        Vector3 bicyclePosition = new Vector3(-70.0f, -20.0f, 722.0f);
        //---

        private static Quaternion bicycleRotation = Quaternion.Identity;

        float bicycleSpeed = 0.0f;
        private static float createRotation = MathHelper.Pi;
        float modelsRotation = 0.0f;
        float aspectRatio;

        //Flag
        bool flagCollision = true;
        bool flagPoints = true;
        bool checkResult = true;
        //Points
        ushort points = 20;
        String name = "";

        List<BoundingBox> crossRoadBox =  new List<BoundingBox>();
        BoundingBox bicycleBox;
        BoundingBox endLevelBox;

        public MainClass()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
            Window.Title = "Първо ниво:";
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
            // Set crossRoads:
            crossroadParts.Add(crossRoad00);
            crossroadParts.Add(crossRoad01);
            crossroadParts.Add(crossRoad02);
            crossroadParts.Add(crossRoad03);
            crossroadParts.Add(crossRoad04);
            crossroadParts.Add(crossRoad05);
            crossroadParts.Add(crossRoad06);
            crossroadParts.Add(crossRoad07);
            crossroadParts.Add(crossRoad08);
            crossroadParts.Add(crossRoad09);
            crossroadParts.Add(crossRoad10);
            crossroadParts.Add(crossRoad11);
            crossroadParts.Add(crossRoad12);
            crossroadParts.Add(crossRoad13);
            crossroadParts.Add(crossRoad14);
            crossroadParts.Add(crossRoad15);
            crossroadParts.Add(crossRoad16);
            crossroadParts.Add(crossRoad17);
            crossroadParts.Add(crossRoad18);
            crossroadParts.Add(crossRoad19);
            crossroadParts.Add(crossRoad20);
            crossroadParts.Add(crossRoad21);
            crossroadParts.Add(crossRoad22);
            crossroadParts.Add(crossRoad23);
       
            //Set models:
            models.Add(firstMap);
            models.Add(leftHand);
            models.Add(rightHand);
            models.Add(bicycle);
            
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            firstMap = Content.Load<Model>("FMap");
            leftHand = Content.Load<Model>("Bicycle//LeftHand");
            rightHand = Content.Load<Model>("Bicycle//RightHand");
            bicycle = Content.Load<Model>("Bicycle//Bicycle");
            font = Content.Load<SpriteFont>("font");

            for (int index = 0; index <= 23; index++)
            {
                crossroadParts[index] = Content.Load<Model>("Parts//" + nameFBXFiles[index]);
            }
            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected BoundingBox UpdateBoundingBox(Model model, Matrix worldTransform)
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            // For each mesh of the model
            foreach (ModelMesh mesh in model.Meshes)
            {
                
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
               
            }
            return new BoundingBox(min, max);
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
            // Types of KeyboardState. 

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

            if (flagCollision)
            {
                for (int index = 0; index <= 23; index++)
                {
                    if (index != 21)
                    {
                        crossRoadBox.Add(UpdateBoundingBox(crossroadParts[index], Matrix.Identity));
                    }
                    else if (index == 21)
                    {
                        endLevelBox = UpdateBoundingBox(crossroadParts[21], Matrix.Identity);
                    }
                }
                flagCollision = false;
            }

            bicycleBox = UpdateBoundingBox(bicycle, worldMatrixbb);

            if (CollisonDetteection(bicycleBox, crossRoadBox))
            {
                if (flagPoints == true)
                {
                    if (points > 0 && points < 21)
                    {
                        points--;
                        bicyclePosition = new Vector3(-70.0f, -20.0f, 722.0f);
                        bicycleRotation = Quaternion.Identity;
                    }
                    flagPoints = false;
                }
            }
            else 
            {
                flagPoints = true;
            }

            if (bicycleBox.Intersects(endLevelBox))
            {
                checkResult = false;
                name = "Congratulations! You go to second level!";
                bicyclePosition = new Vector3(-70.0f, -20.0f, 722.0f);
                bicycleRotation = Quaternion.Identity;
                UpdateCamera();
                String startupPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                Process.Start(startupPath + @"\Map2\WindowsGameN.exe");
                this.Exit();
            }

            if (checkResult)
            {
                ProcessKeyboard(gameTime);
                MoveForward(ref bicyclePosition, bicycleRotation, bicycleSpeed);
                UpdateCamera();
            }

            if (points == 0)
            {
                name = "I am sorry! But you lost the game! Game Over!";
                bicyclePosition = new Vector3(-70.0f, -20.0f, 722.0f);
                bicycleRotation = Quaternion.Identity;
                if (currentKey.IsKeyDown(Keys.Enter))
                {
                    this.Exit();
                }
            }

            base.Update(gameTime);
        }


        private bool CollisonDetteection(BoundingBox c1, List<BoundingBox> c2)
        {

                for (int index2 = 0; index2 < c2.Count; index2++)
                {
                    if (c1.Intersects(c2[index2]))
                    {
                        return true;
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

            DrawModel.DrawBicycle(bicycle, viewMatrix, projectionMatrix, createRotation, bicycleRotation, bicyclePosition, ref worldMatrixbb);
            DrawModel.DrawBicycle(leftHand, viewMatrix, projectionMatrix, createRotation, bicycleRotation, bicyclePosition, ref identity);
            DrawModel.DrawBicycle(rightHand, viewMatrix, projectionMatrix, createRotation, bicycleRotation, bicyclePosition, ref identity);
            DrawModel.DrawRoad(firstMap, viewMatrix, projectionMatrix);

            foreach (Model crossPart in crossroadParts)
            {
                DrawModel.DrawRoad(crossPart, viewMatrix, projectionMatrix);
            }
            DrawPoints(points);
            DrawText(name);
            //DrawBB(bicycleBox);

            //for (int index = 0; index < crossRoadBox.Count; index++)
            //{
            //    DrawBB(crossRoadBox[index]);
            //}
            base.Draw(gameTime);
        }

        private void DrawBB(BoundingBox box)
        {
            Vector3 min = box.Min;
            Vector3 max = box.Max;

            Effect effect;
            VertexPositionColor[] verts = new VertexPositionColor[24];
            VertexDeclaration decl;

            verts[0].Position = new Vector3(min.X, min.Y, min.Z);
            verts[1].Position = new Vector3(max.X, min.Y, min.Z);
            verts[2].Position = new Vector3(min.X, min.Y, max.Z);
            verts[3].Position = new Vector3(max.X, min.Y, max.Z);
            verts[4].Position = new Vector3(min.X, min.Y, min.Z);
            verts[5].Position = new Vector3(min.X, min.Y, max.Z);
            verts[6].Position = new Vector3(max.X, min.Y, min.Z);
            verts[7].Position = new Vector3(max.X, min.Y, max.Z);
            verts[8].Position = new Vector3(min.X, max.Y, min.Z);
            verts[9].Position = new Vector3(max.X, max.Y, min.Z);
            verts[10].Position = new Vector3(min.X, max.Y, max.Z);
            verts[11].Position = new Vector3(max.X, max.Y, max.Z);
            verts[12].Position = new Vector3(min.X, max.Y, min.Z);
            verts[13].Position = new Vector3(min.X, max.Y, max.Z);
            verts[14].Position = new Vector3(max.X, max.Y, min.Z);
            verts[15].Position = new Vector3(max.X, max.Y, max.Z);
            verts[16].Position = new Vector3(min.X, min.Y, min.Z);
            verts[17].Position = new Vector3(min.X, max.Y, min.Z);
            verts[18].Position = new Vector3(max.X, min.Y, min.Z);
            verts[19].Position = new Vector3(max.X, max.Y, min.Z);
            verts[20].Position = new Vector3(min.X, min.Y, max.Z);
            verts[21].Position = new Vector3(min.X, max.Y, max.Z);
            verts[22].Position = new Vector3(max.X, min.Y, max.Z);
            verts[23].Position = new Vector3(max.X, max.Y, max.Z);

            RasterizerState raster = new RasterizerState();
            GraphicsDevice.RasterizerState = raster;
            BasicEffect basicEffect = new BasicEffect(GraphicsDevice);

            basicEffect.View = viewMatrix;
            basicEffect.Projection = projectionMatrix;


            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(
                    PrimitiveType.LineList, verts,
                    0,
                    12
                );
            }
        }

        private void AddElements(Model meshes, ref List<ModelMesh> collection)
        {
            foreach (ModelMesh item in meshes.Meshes)
            {
                collection.Add(item); 
            }
        }

        private void DrawPoints(ushort points)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
            spriteBatch.DrawString(font, "Points: " + points.ToString() , new Vector2(20, 45), Color.Red);
            spriteBatch.End();
        }

        private void DrawText(String name)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
            spriteBatch.DrawString(font, name, new Vector2(460, 45), Color.Red);
            spriteBatch.End();
        }
    }
}