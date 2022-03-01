﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NOBOIShooter.States;

namespace NOBOIShooter
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        List<GameObject> _gameObjects;
        int _numObject;

        private State _currentState;
        private State _nextState;

        SpriteFont _font;

        //Constructor
        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = Singleton.Instance.ContentRootDir;
            IsMouseVisible = Singleton.Instance.IsMouseVisible;
        }

        //initialize (run once)
        protected override void Initialize()
        {
            //setting screen size
            _graphics.PreferredBackBufferHeight = Singleton.Instance.ScreenHeight;
            _graphics.PreferredBackBufferWidth = Singleton.Instance.ScreenWidth;
            _graphics.ApplyChanges();

            //load game objects
            _gameObjects = new List<GameObject>();

            base.Initialize();
        }

        //load content (such as assets, picture, music)
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            _currentState = new SpriteBatch(GraphicsDevice);
            _currentState.LoadContent();
            _nextState = null;
            //load font
            //_font = Content.Load<SpriteFont>("GameFont");
        }

        //update screen
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            {
                if (_nextState != null)
                {
                    _currentState = _nextState;
                    _currentState.LoadContent();

                    _nextState = null;
                }

                _currentState.Update(gameTime);
                _currentState.PostUpdate(gameTime);
            }

            base.Update(gameTime);
        }

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        //draw
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _currentState.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }
    }
}
