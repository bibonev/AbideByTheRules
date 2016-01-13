using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameABTR
{
    class GameABTR : Microsoft.Xna.Framework.Game
    {
        private static GameABTR mSingelton;
		private static volatile object mSingeltonLock;

		public GraphicsDeviceManager graphics;
		private ScreenManager.ScreenManager screenManager;

		static GameABTR()
		{
			GameABTR.mSingelton = null;
			GameABTR.mSingeltonLock = new Object();
		}

        public GameABTR()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

        public static GameABTR Instance
		{
			get
			{
                if (GameABTR.mSingelton == null)
				{
                    lock (GameABTR.mSingeltonLock)
					{
                        if (GameABTR.mSingelton == null)
                            GameABTR.mSingelton = new Pong();
					}
				}
                return GameABTR.mSingelton;
			}
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			//centre.X = graphics.GraphicsDevice.Viewport.Width / 2;
			//centre.Y = graphics.GraphicsDevice.Viewport.Height / 2;

			Graphics.Graphics.InitGraphicsMode(1920, 1080, true);

			screenManager = new ScreenManager.ScreenManager(this);

			Components.Add(screenManager);
			Components.Add(new MessageDisplayComponent(this));
			Components.Add(new GamerServicesComponent(this));

			// Activate the first screens.
			screenManager.AddScreen(new Screens.BackgroundScreen(), null);
			screenManager.AddScreen(new Screens.MainMenuScreen(), null);

			base.Initialize();


		}


		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.Black);
			base.Draw(gameTime);
		}
    }
}
