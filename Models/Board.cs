using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chess.Models
{
    public class Board
    {
        static Piece?[,] board = new Piece[8, 8];
        private static Grid? _mainGrid;

        public static void InitializeBoard(Grid mainGrid)
        {
            _mainGrid = mainGrid;
            // Placing pawns
            for (int i = 0; i < 8; i++)
            {
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

            // Render the board in the UI with emojis
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {

                    Piece? piece = board[r, c];

                    renderPiece(piece, r, c);
                }
            }
        }

        private static void renderPiece(Piece piece, int row, int col)
        {
            if (piece != null)
            {
                Border square = new Border
                {
                    Background = piece.Color == Color.White ? Brushes.Black : Brushes.White,
                    BorderBrush = Brushes.Gray,
                    Width = 50,
                    Height = 50,
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(20),
                    Tag = (row, col)
                };

                square.MouseLeftButtonDown += Square_MouseLeftButtonDown;

                TextBlock pieceText = new TextBlock();
                pieceText.Text = GetPieceEmoji(piece.Type, piece.Color);
                pieceText.FontSize = 36;

                // Center 
                pieceText.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                pieceText.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                // Set color
                pieceText.Foreground = Brushes.Gray;

                square.Child = pieceText;

                Grid.SetRow(square, row);
                Grid.SetColumn(square, col);

                _mainGrid.Children.Add(square);
            }

        }

        private static void renderMoves(Grid mainGrid, List<(int row, int col)> moves)
        {
            moves.ForEach(move =>
            {
                Border square = new Border
                {
                    Background = Brushes.Transparent,
                    BorderBrush = Brushes.Green,
                    BorderThickness = new Thickness(5),
                    Tag = move
                };
                //square.MouseLeftButtonDown += Square_MouseLeftButtonDown; // Add logic for moving piece here
                Grid.SetRow(square, move.row);
                Grid.SetColumn(square, move.col);
                mainGrid.Children.Add(square);
            });
        }

        private static string GetPieceEmoji(PieceType type, Color color)
        {
            string whitePiece = type switch
            {
                PieceType.King => "♔",
                PieceType.Queen => "♕",
                PieceType.Tower => "♖",
                PieceType.Bishop => "♗",
                PieceType.Knight => "♘",
                PieceType.Pawn => "♙",
                _ => "?"
            };

            string blackPiece = type switch
            {
                PieceType.King => "♚",
                PieceType.Queen => "♛",
                PieceType.Tower => "♜",
                PieceType.Bishop => "♝",
                PieceType.Knight => "♞",
                PieceType.Pawn => "♟",
                _ => "?"
            };

            return color == Color.White ? whitePiece : blackPiece;
        }

        public static Piece? getPieceAt(int row, int column)
        {
            if (row >= 0 && row < 8 && column >= 0 && column < 8)
            {
                return board[row, column];
            }
            return null;
        }
        // Place holder for clicking a piece, !!! ADD MISSING LOGIC !!!
        private static void Square_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border clickedSquare = (Border)sender;

            if (clickedSquare.Tag != null)
            {
                (int row, int col) = ((int, int))clickedSquare.Tag;

                List<(int row, int col)>? moves = getPieceAt(row, col).GetValidMoves();
                renderMoves(_mainGrid, moves);
            }
        }
    }
}