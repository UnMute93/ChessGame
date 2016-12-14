using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ChessGame.Classes;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ChessGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Chess chess;
        MouseState currentMouseState;
        MouseState previousMouseState;
        Vector2 scale;
        Microsoft.Xna.Framework.Color squareTint;

        Square fromSquare;
        Square toSquare;
        Vector2 previousPiecePos;
        Piece draggingPiece;
        Vector2 imageOffset;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            scale = new Vector2(1f, 1f);
            chess = new Chess(scale);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            currentMouseState = Mouse.GetState();
            previousMouseState = Mouse.GetState();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            chess.Board.LoadContent(Content);
            foreach (Square square in chess.Board.Squares)
            {
                square.LoadContent(Content);
            }
            foreach (Piece piece in chess.Board.Pieces)
            {
                piece.LoadContent(Content);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds; //placeholder for timer
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            Vector2 mousePos = new Vector2(currentMouseState.X, currentMouseState.Y);
            
            // Clicked on piece
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton != ButtonState.Pressed)
            {
                foreach (Square square in chess.Board.Squares)
                {
                    // If within bounds of texture
                    if (square.Piece != null && square.Piece.IsSelectable
                        && mousePos.X >= square.ImagePos.X && mousePos.X <= square.ImagePos.X + (square.Image.Width * scale.X)
                        && mousePos.Y >= square.ImagePos.Y && mousePos.Y <= square.ImagePos.Y + (square.Image.Height * scale.Y))
                    {
                        fromSquare = square;
                        previousPiecePos = square.Piece.ImagePos;
                        draggingPiece = square.Piece;
                        imageOffset = fromSquare.Piece.ImagePos - mousePos;
                    }
                }
            }

            if (draggingPiece != null)
            {
                draggingPiece.ImagePos = mousePos + imageOffset;
            }

            if (draggingPiece != null && currentMouseState.LeftButton == ButtonState.Released &&  previousMouseState.LeftButton != ButtonState.Released)
            {
                bool moveMade = false;
                foreach (Square square in chess.Board.Squares)
                {
                    // If within bounds of texture
                    if (mousePos.X >= square.ImagePos.X && mousePos.X <= square.ImagePos.X + (square.Image.Width * scale.X)
                        && mousePos.Y >= square.ImagePos.Y && mousePos.Y <= square.ImagePos.Y + (square.Image.Height * scale.Y))
                    {
                        toSquare = square;
                        if (chess.MakeMove(fromSquare, toSquare))
                            moveMade = true;
                    }
                }
                if (!moveMade)
                    draggingPiece.ImagePos = previousPiecePos;

                draggingPiece = null;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.BackToFront, null);
            spriteBatch.Draw(chess.Board.Image, chess.Board.ImagePos, null, null, null, 0, scale, Microsoft.Xna.Framework.Color.White, SpriteEffects.None, 1);
            foreach (Square square in chess.Board.Squares)
            {
                spriteBatch.Draw(square.Image, square.ImagePos, null, null, null, 0, scale, Microsoft.Xna.Framework.Color.White, SpriteEffects.None, 0.9f);
            }
            if (draggingPiece != null)
            {
                foreach (Square square in draggingPiece.PseudoLegalMoves)
                {
                    spriteBatch.Draw(square.SelectionImage, square.ImagePos, null, null, null, 0, scale, Microsoft.Xna.Framework.Color.White, SpriteEffects.None, 0.8f);
                }
            }
            foreach (Piece piece in chess.Board.Pieces)
            {
                if (draggingPiece != null && piece == draggingPiece)
                    spriteBatch.Draw(piece.Image, piece.ImagePos, null, null, null, 0, scale, Microsoft.Xna.Framework.Color.White, SpriteEffects.None, 0.6f);
                else
                    spriteBatch.Draw(piece.Image, piece.ImagePos, null, null, null, 0, scale, Microsoft.Xna.Framework.Color.White, SpriteEffects.None, 0.7f);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
