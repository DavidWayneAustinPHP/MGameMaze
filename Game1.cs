// The maze game uses the following resource objects to be able to build the game
// The MONOGAME version 3.6 framework which can be downloaded and installed onto Visual Studion from url: 
// www.monogame.net 
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGameMaze
{
    // The complete class of the maze game
    public class Game1 : Game
    {
        // Declare variables used in the maze game - the random variable controlls the movement of the monsters in the game
        Random random = new Random();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        const int block = 20;

        // Set the game world environment to 2D mode
        Texture2D wizard, monsterAx, monsterIce, background, axmonstercaptured, icemonstercaputred, winningBackground;

        // Declare the vector variables for the sprites
        Vector2 wizardPosition;
        Vector2 monsterAxPosition;
        Vector2 monsterIcePosition;
        Vector2 axmonstercapturedPos;
        Vector2 icemonstercaputredPos;
        Vector2 winningBackgroundPos;

        // Set the starting values of game ending banners
        int axmonstercapturedPosX = 1200;
        int axmonstercapturedPosY = 800;
        int icemonstercaputredPosX = 1200;
        int icemonstercaputredPosY = 800;
        int winningBackgroundPosX = 1200;
        int winningBackgroundPosY = 800;

        // Set the game ending values for banner
        int axmonstercapturedPosXEnd = 190;
        int axmonstercapturedPosYEnd = 120;
        int icemonstercaputredPosXEnd = 190;
        int icemonstercaputredPosYEnd = 120;
        int winningBackgroundPosXEnd = 190;
        int winningBackgroundPosYEnd = 120;

        // Set the starting values of the wizard in the game
        int wizardPositionStartX = 80;
        int wizardPositionStartY = 90;
        int wizardSpeed = 3;

        // Sets the difficult level of the game, this controls the size of the monsters in the game
        int difficultyLevel = 20;

        // Set the starting values for the monsters in the game
        int monsterAxPositionStartX = 780;
        int monsterAxPositionStartXMin;
        int monsterAxPositionStartXMax;
        int monsterAxPositionStartY = 150;
        int monsterAxPositionStartYMin;
        int monsterAxPositionStartYMax;

        int monsterIcePositionStartX = 780;
        int monsterIcePositionStartXMin;
        int monsterIcePositionStartXMax;
        int monsterIcePositionStartY = 300;
        int monsterIcePositionStartYMin;
        int monsterIcePositionStartYMax;

        // This is the location of the treasure in the game
        int foundTreasureMaxX = 1015;
        int foundTreasureMinX = 990;
        int foundTreasureMaxY = 120;
        int foundTreasureMinY = 90;

        // This is the size of the game world that the monsters and and the wizard will play in 
        int worldMinX = 80;
        int worldMaxX = 1015;
        int worldMinY = 90;
        int worldMaxY = 592;

        // Variables that control the move direction and speed of the monsters
        int directionChangeRestart = 50;
        int directionChange = 50;
        int monsterSpeedAx = 2;
        int monsterSpeedIce = 1;
        int monsterDirectionControlAx = 1;
        int monsterDirectionControlIce = 1;

        // Monster direction movement control
        int left = 1;
        int right = 2;
        int up = 3;
        int down = 4;


        // This method load the entire game world for the maze game
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        // This method initialises the maze game world
        protected override void Initialize()
        {
            graphics.PreferredBackBufferHeight = block;
            graphics.PreferredBackBufferWidth = block;

            base.Initialize();
        }

        // This method loads all the game content that is used by the maze game. 
        // The game content files are loaded and controlled by the file Content.mgcb using the open with MonoGame Pipeline (Default) application
        // The game files can then be added and removed. 
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            wizard = Content.Load<Texture2D>("wizard");
            monsterAx = Content.Load<Texture2D>("monsterAx");
            monsterIce = Content.Load<Texture2D>("monsterIce");

            background = Content.Load<Texture2D>("gamebackground");

            axmonstercaptured = Content.Load<Texture2D>("axmonstercaptured");
            icemonstercaputred = Content.Load<Texture2D>("icemonstercaptured");
            winningBackground = Content.Load<Texture2D>("winningBackground");
        }

        // This method is currently not used, but is part of the MONOGAME framework, it can be used to unload game content
        // This method is left blank for future development of the maze game
        protected override void UnloadContent()
        {
           
        }

        // This method controls the movement of the sprites in the game and also will exit the game is the Esc key is pressed
        protected override void Update(GameTime gameTime)
        {
            // Get the keyboard state this is used for the control of the wizard in the maze
            KeyboardState state = Keyboard.GetState();

            // Get the randomly generated direction for the monsters
            int direction1 = random.Next(4) + 1;
            int direction2 = random.Next(4) + 1;

            // Wizard movement controls in the game world - the movement of the wizard is controller by the keyboard arrow keys
            if (state.IsKeyDown(Keys.Left))
            {
                if (wizardPositionStartX > worldMinX)
                {
                    wizardPositionStartX = wizardPositionStartX - wizardSpeed;
                }
            }
            if (state.IsKeyDown(Keys.Right))
            {
                if (wizardPositionStartX < worldMaxX)
                {
                    wizardPositionStartX = wizardPositionStartX + wizardSpeed;
                }
            }
            if (state.IsKeyDown(Keys.Up))
            {
                if (wizardPositionStartY > worldMinY)
                {
                    wizardPositionStartY = wizardPositionStartY - wizardSpeed;
                }
            }
            if (state.IsKeyDown(Keys.Down))
            {
                if (wizardPositionStartY < worldMaxY)
                {
                    wizardPositionStartY = wizardPositionStartY + wizardSpeed;
                }
            }

            // Exit the game if the Esc key is pressed on the keyboard
            if (state.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // Control Monster movement - if the direction count is 0 then reset the direction counter
            if (directionChange == 0)
            {
                monsterDirectionControlAx = direction1;
                monsterDirectionControlIce = direction2;
                directionChange = directionChangeRestart;
            }

            // Move the monsters in the set direction until the directionChange counter is 0, then the monsters will change there direction of movement
            if (directionChange != 0)
            {
                if(monsterDirectionControlAx == left)
                {
                    if (monsterAxPositionStartX > worldMinX)
                    {
                        monsterAxPositionStartX = monsterAxPositionStartX - monsterSpeedAx;
                    }
                }
                if (monsterDirectionControlIce == left)
                {
                    if (monsterIcePositionStartX > worldMinX)
                    {
                        monsterIcePositionStartX = monsterIcePositionStartX - monsterSpeedIce;
                    }
                }


                if (monsterDirectionControlAx == right)
                {
                    if (monsterAxPositionStartX < worldMaxX)
                    {
                        monsterAxPositionStartX = monsterAxPositionStartX + monsterSpeedAx;
                    }
                }
                if (monsterDirectionControlIce == right)
                {
                    if (monsterIcePositionStartX < worldMaxX)
                    {
                        monsterIcePositionStartX = monsterIcePositionStartX + monsterSpeedIce;
                    }
                }


                if (monsterDirectionControlAx == up) 
                {
                    if (monsterAxPositionStartY > worldMinY)
                    {
                        monsterAxPositionStartY = monsterAxPositionStartY - monsterSpeedAx;
                    }
                }
                if (monsterDirectionControlAx == up)
                {
                    if (monsterIcePositionStartY > worldMinY)
                    {
                        monsterIcePositionStartY = monsterIcePositionStartY - monsterSpeedIce;
                    }
                }
                

                if (monsterDirectionControlAx == down)
                {
                    if (monsterAxPositionStartY < worldMaxY)
                    {
                        monsterAxPositionStartY = monsterAxPositionStartY + monsterSpeedAx;
                    }

                }
                if (monsterDirectionControlAx == down)
                {
                    if (monsterIcePositionStartY < worldMaxY)
                    {
                        monsterIcePositionStartY = monsterIcePositionStartY + monsterSpeedIce;
                    }
                }
            }

            // Set the new positions of the sprites on the game world
            wizardPosition = new Vector2(wizardPositionStartX, wizardPositionStartY);
            monsterAxPosition = new Vector2(monsterAxPositionStartX, monsterAxPositionStartY);
            monsterIcePosition = new Vector2(monsterIcePositionStartX, monsterIcePositionStartY);
            winningBackgroundPos = new Vector2(winningBackgroundPosX, winningBackgroundPosY);
            axmonstercapturedPos = new Vector2(axmonstercapturedPosX, axmonstercapturedPosY);
            icemonstercaputredPos = new Vector2(icemonstercaputredPosX, icemonstercaputredPosY);

            // Set the monster size which is controlled by the value of difficultyLevel
            monsterAxPositionStartXMax = monsterAxPositionStartX + difficultyLevel;
            monsterAxPositionStartXMin = monsterAxPositionStartX - difficultyLevel;
            monsterAxPositionStartYMax = monsterAxPositionStartY + difficultyLevel;
            monsterAxPositionStartYMin = monsterAxPositionStartY - difficultyLevel;
            monsterIcePositionStartXMax = monsterIcePositionStartX + difficultyLevel;
            monsterIcePositionStartXMin = monsterIcePositionStartX - difficultyLevel;
            monsterIcePositionStartYMax = monsterIcePositionStartY + difficultyLevel;
            monsterIcePositionStartYMin = monsterIcePositionStartY - difficultyLevel;

            // Condition to win the game if the wizard gets to the treasure
            if (((wizardPositionStartX < foundTreasureMaxX) && (wizardPositionStartX > foundTreasureMinX)) 
                && ((wizardPositionStartY < foundTreasureMaxY) && (wizardPositionStartY > foundTreasureMinY)))
            {
                // Display banner showing that the game has been won
                winningBackgroundPos = new Vector2(winningBackgroundPosXEnd, winningBackgroundPosYEnd);
            }

            // Condition to exit game if captured by monster Ax
            if (((wizardPositionStartX < monsterAxPositionStartXMax) && (wizardPositionStartX > monsterAxPositionStartXMin))
                && ((wizardPositionStartY < monsterAxPositionStartYMax) && (wizardPositionStartY > monsterAxPositionStartYMin))) 
            {
                // Display banner on screen that the game has been lost
                axmonstercapturedPos = new Vector2(axmonstercapturedPosXEnd, axmonstercapturedPosYEnd);
            }

            // Condition to exit the game if captured by monster Ice  
            if ( ((wizardPositionStartX < monsterIcePositionStartXMax) && (wizardPositionStartX > monsterIcePositionStartXMin)) 
                && ((wizardPositionStartY < monsterIcePositionStartYMax) && (wizardPositionStartY > monsterIcePositionStartYMin)))
            {
                // Display banner on screen that the game has been lost
                icemonstercaputredPos = new Vector2(icemonstercaputredPosXEnd, icemonstercaputredPosYEnd);
            }

            // Direction change of monsters movement count down
            directionChange--;

            // Update the poistion of the game sprite characters in the game world
            base.Update(gameTime);
        }

        // Draw the game world for the maze game
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            DrawMaze();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        // Draw the sprites and background to the game world using the vector positioning variable
        void DrawMaze()
        {
            spriteBatch.Draw(background, Vector2.One, Color.White);
            spriteBatch.Draw(wizard, wizardPosition, Color.White);
            spriteBatch.Draw(monsterAx, monsterAxPosition, Color.White);
            spriteBatch.Draw(monsterIce, monsterIcePosition, Color.White);
            spriteBatch.Draw(winningBackground, winningBackgroundPos, Color.White);
            spriteBatch.Draw(axmonstercaptured, axmonstercapturedPos, Color.White);
            spriteBatch.Draw(icemonstercaputred, icemonstercaputredPos, Color.White);
        }
    }
}
