using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Models
{
    public static class Board
    {
        
        static Piece[,] board = new Piece[8, 8];
      
        public static void InitializeBoard() {
          // Initialize the board with pieces in their starting positions
          // Placing pawns
            for (int i = 0; i < 8; i++) {
                board[1, i] = new Piece(PieceType.Pawn, Color.White);
                board[6, i] = new Piece(PieceType.Pawn, Color.Black);
            }
          // Placing white pieces
            board[0, 0] = new Piece(PieceType.Tower, Color.White);
            board[0, 1] = new Piece(PieceType.Knight, Color.White);
            board[0, 2] = new Piece(PieceType.Bishop, Color.White);
            board[0, 3] = new Piece(PieceType.King, Color.White);
            board[0, 4] = new Piece(PieceType.Queen, Color.White);
            board[0, 5] = new Piece(PieceType.Bishop, Color.White);
            board[0, 6] = new Piece(PieceType.Knight, Color.White);
            board[0, 7] = new Piece(PieceType.Tower, Color.White);

          // Placing black pieces
            board[7, 0] = new Piece(PieceType.Tower, Color.Black);
            board[7, 1] = new Piece(PieceType.Knight, Color.Black);
            board[7, 2] = new Piece(PieceType.Bishop, Color.Black);
            board[7, 3] = new Piece(PieceType.Queen, Color.Black);
            board[7, 4] = new Piece(PieceType.King, Color.Black);
            board[7, 5] = new Piece(PieceType.Bishop, Color.Black);
            board[7, 6] = new Piece(PieceType.Knight, Color.Black);
            board[7, 7] = new Piece(PieceType.Tower, Color.Black);

        }
        public static Piece getPieceAt(int row, int column) {
            if (board[row, column] != null) {
                return board[row, column];                   
            } else
            {
                return null;
            }
        }



        /*
        static int[,] board = new int[8, 8];

        public static void InitializeBoard()
        {
           
            // Placing white pieces
            board[0, 0] = 1;
            board[0, 1] = 2;
            board[0, 2] = 3;
            board[0, 3] = 4;
            board[0, 4] = 5;
            board[0, 5] = 6;
            board[0, 6] = 7;
            board[0, 7] = 8;
        }

        public static int getPiece(int row, int column)
        {
            return board[0, 7];
        }
        */

    }
}
