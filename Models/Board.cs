using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess.Models
{
    public class Board
    {
        static Piece[,] board = new Piece[8, 8];
      
        public static void InitializeBoard(Grid mainGrid) {
          // Initialize the board with pieces in their starting positions
          // Placing pawns
            for (int i = 0; i < 8; i++) {
                board[1, i] = new Pawn(Color.White, PieceType.Pawn, 1, i);
                board[6, i] = new Pawn(Color.Black, PieceType.Pawn, 6, i);
            }
          // Placing white pieces
            board[0, 0] = new Tower(Color.White, PieceType.Tower, 0, 0);
            board[0, 1] = new Knight(Color.White, PieceType.Knight, 0, 1);
            board[0, 2] = new Bishop(Color.White, PieceType.Bishop, 0, 2);
            board[0, 3] = new King(Color.White, PieceType.King, 0, 3);
            board[0, 4] = new Queen(Color.White, PieceType.Queen, 0, 4);
            board[0, 5] = new Bishop(Color.White, PieceType.Bishop, 0, 5);
            board[0, 6] = new Knight(Color.White, PieceType.Knight, 0, 6);
            board[0, 7] = new Tower(Color.White, PieceType.Tower, 0, 7);

          // Placing black pieces
            board[7, 0] = new Tower(Color.Black, PieceType.Tower, 7, 0);
            board[7, 1] = new Knight(Color.Black, PieceType.Knight, 7, 1);
            board[7, 2] = new Bishop(Color.Black, PieceType.Bishop, 7, 2);
            board[7, 3] = new Queen(Color.Black, PieceType.Queen, 7, 3);
            board[7, 4] = new King(Color.Black, PieceType.King, 7, 4);
            board[7, 5] = new Bishop(Color.Black, PieceType.Bishop, 7, 5);
            board[7, 6] = new Knight(Color.Black, PieceType.Knight, 7, 6);
            board[7, 7] = new Tower(Color.Black, PieceType.Tower, 7, 7);

          //Render the board in the UI
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Piece piece = board[r, c];

                    if (piece != null)
                    {

                        /* Use this when assets are ready
                        Image img = new Image();

                        img.Source = new BitmapImage(
                            new Uri(piece.*ImagePath*, UriKind.Relative)
                        );
                        */

                        TextBlock img = new TextBlock();
                        img.Text = "x";

                        Grid.SetRow(img, r);
                        Grid.SetColumn(img, c);

                        mainGrid.Children.Add(img);
                    }
                }
            }

        }

        public static Piece getPieceAt(int row, int column) {
            if (board[row, column] != null) {
                return board[row, column];                   
            } else
            {
                return null;
            }
        }

    }
}
