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
using LaborationVFX.Components.Input;
using LaborationVFX.Components;

namespace LaborationVFX {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        FlyingCamera fcamera;
        Camera camera;

        SimplePlane simplePlane;

        private BasicEffect effect;
        private Matrix world;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.IsFullScreen = false;
            graphics.PreferMultiSampling = true;
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 800;
            graphics.SynchronizeWithVerticalRetrace = true;


            camera = new Camera(GraphicsDevice);
            fcamera = new FlyingCamera();

            RasterizerState rs = new RasterizerState() { CullMode = CullMode.None };
            graphics.GraphicsDevice.RasterizerState = rs;

            InputHandler ip = new InputHandler(this);
            Components.Add(ip);
            Services.AddService(typeof(IInputHandler), ip);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            effect = new BasicEffect(GraphicsDevice);

            simplePlane = new SimplePlane(GraphicsDevice, new Vector3(0, 0, 0), Quaternion.Identity, 1f);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            // Allows the game to exit
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Read input
            IInputHandler inputHandler = (IInputHandler)Services.GetService(typeof(IInputHandler));
            inputAction(inputHandler.getUnhandledActions(), gameTime.ElapsedGameTime.Milliseconds);

            //To make the camera mov   
            camera.Update(fcamera.Position, fcamera.Rotation);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        private void inputAction(List<ActionType> actions, float elapsedTime)
        {
            foreach (var action in actions)
            {
                if (action == ActionType.Quit)
                    this.Exit();
                else
                fcamera.PerformAction(action, elapsedTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            world = Matrix.Identity;
            effect.Projection = camera.ViewProjectionMatrix;
            effect.View = camera.ViewMatrix;
            effect.World = world;
            //effect.TextureEnabled = true;

            Matrix parent = Matrix.Identity;
            simplePlane.Draw(ref effect, ref parent);

            base.Draw(gameTime);
        }
    }
}
