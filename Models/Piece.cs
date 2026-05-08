using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Chess.Models
{
    public enum  PieceType {
        King, Queen, Bishop, Tower, Knight, Pawn
    }

    public enum Color {
        White, Black
    }

    public abstract class Piece
    {
        public Color Color { get; protected set; }
        public PieceType Type { get; protected set; }
        public int Row { get; set; }
        public int Col { get; set; }

        protected Piece(Color color, PieceType type, int row, int col)
        {
            Color = color;
            Type = type;
            Row = row; 
            Col = col;
        }

        protected bool IsOnBoard(int row, int col)
        {
            return row >= 0 && row < 8 && col >= 0 && col < 8;
        }

        protected bool IsFriendly(Piece? otherPiece)
        {
            return otherPiece != null && otherPiece.Color == this.Color;
        }

        public abstract List<(int row, int col)> GetValidMoves();
    }

    // KING

    public class King : Piece
    {
        public King(Color color, PieceType type, int row, int col) : base(color, type, row, col) { }

        public override List<(int, int)> GetValidMoves()
        {
            var moves = new List<(int, int)>();
            int[] dRow = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dCol = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int i = 0; i < 8; i++)
            {
                int r = this.Row + dRow[i];
                int c = this.Col + dCol[i];
                if (IsOnBoard(r, c))
                {
                    Piece? target = Board.getPieceAt(r, c);
                    if (target == null || !IsFriendly(target))
                        moves.Add((r, c));
                }
            }
            return moves;
        }
    }

    public class Queen : Piece
    {
        public Queen(Color color, PieceType type, int row, int col) : base(color, type, row, col) { }

        public override List<(int, int)> GetValidMoves()
        {
            var moves = new List<(int, int)>();
            int[] dRow = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dCol = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int i = 0; i < 8; i++)
            {
                int r = this.Row + dRow[i];
                int c = this.Col + dCol[i];

                while(IsOnBoard(r, c))
                {
                    Piece? target = Board.getPieceAt(r, c);
                    if (target == null)
                    {
                        moves.Add((r, c));
                    }
                    else
                    {
                        if (!IsFriendly(target))
                        {
                            moves.Add((r, c));
                        }
                        break;
                    }
                    r += dRow[i];
                    c += dCol[i];
                }
            }
            return moves;
        }
    }

    public class Bishop : Piece
    {
        public Bishop(Color color, PieceType type, int row, int col) : base(color, type, row, col) { }

        public override List<(int, int)> GetValidMoves()
        {
            var moves = new List<(int, int)>();
            int[] dRow = { -1, -1, 1, 1 };
            int[] dCol = { -1, 1, -1, 1 };

            for (int i = 0; i < 4; i++)
            {
                int r = this.Row + dRow[i];
                int c = this.Col + dCol[i];

                while (IsOnBoard(r, c))
                {
                    Piece? target = Board.getPieceAt(r, c);
                    if (target == null)
                    {
                        moves.Add((r, c));
                    }
                    else
                    {
                        if (!IsFriendly(target))
                        {
                            moves.Add((r, c));
                        }
                        break;
                    }
                    r += dRow[i];
                    c += dCol[i];
                }
            }
            return moves;
        }
    }

    public class Tower : Piece
    {
        public Tower(Color color, PieceType type, int row, int col) : base(color, type, row, col) { }

        public override List<(int, int)> GetValidMoves()
        {
            var moves = new List<(int, int)>();
            int[] dRow = { -1, 0, 0, 1 };
            int[] dCol = { 0, -1, 1, 0 };

            for (int i = 0; i < 4; i++)
            {
                int r = this.Row + dRow[i];
                int c = this.Col + dCol[i];

                while (IsOnBoard(r, c))
                {
                    Piece? target = Board.getPieceAt(r, c);
                    if (target == null)
                    {
                        moves.Add((r, c));
                    }
                    else
                    {
                        if (!IsFriendly(target))
                        {
                            moves.Add((r, c));
                        }
                        break;
                    }
                    r += dRow[i];
                    c += dCol[i];
                }
            }
            return moves;
        }
    }

    public class Knight : Piece
    {
        public Knight(Color color, PieceType type, int row, int col) : base(color, type, row, col) { }

        public override List<(int, int)> GetValidMoves()
        {
            var moves = new List<(int, int)>();
            int[] dRow = { -2, -2, -1, -1, 1, 1, 2, 2 };
            int[] dCol = { -1, 1, -2, 2, -2, 2, -1, 1 };

            for (int i = 0; i < 8; i++)
            {
                int r = this.Row + dRow[i];
                int c = this.Col + dCol[i];
                if (IsOnBoard(r, c))
                {
                    Piece? target = Board.getPieceAt(r, c);
                    if (target == null || !IsFriendly(target))
                        moves.Add((r, c));
                }
            }
            return moves;
        }
    }

    public class Pawn : Piece
    {
        public Pawn(Color color, PieceType type, int row, int col) : base(color, type, row, col) { }

        public override List<(int, int)> GetValidMoves()
        {
            var moves = new List<(int, int)>();

            int direction = Color == Color.White ? 1 : -1;
            int startRow = Color == Color.White ? 1 : 6;

            int oneRow = Row + direction;

            // Move forward one square
            if (IsOnBoard(oneRow, Col) && Board.getPieceAt(oneRow, Col) == null)
            {
                moves.Add((oneRow, Col));

                // Move forward two squares from starting row
                int twoRow = Row + (2 * direction);
                if (Row == startRow &&
                    IsOnBoard(twoRow, Col) &&
                    Board.getPieceAt(twoRow, Col) == null)
                {
                    moves.Add((twoRow, Col));
                }
            }

            // Diagonal captures
            int[] captureCols = { Col - 1, Col + 1 };

            foreach (int captureCol in captureCols)
            {
                if (IsOnBoard(oneRow, captureCol))
                {
                    Piece? target = Board.getPieceAt(oneRow, captureCol);
                    if (target != null && !IsFriendly(target))
                    {
                        moves.Add((oneRow, captureCol));
                    }
                }
            }

            return moves;
        }
    }
}
