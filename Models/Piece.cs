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

            if (this.Color == Color.White)
            {
                int[] dRow = { 1 , 0};
                int[] dCol = { 0 , 0};
                int[] dEatRow = { 1, 1 };
                int[] dEatCol = { -1, 1 };
                int[] dFirstRow = { 1, 2 };
                int[] dFirstCol = { 0, 0 };

                for (int i = 0; i < 2; i++)
                {
                    int r = this.Row + dRow[i];
                    int c = this.Col + dCol[i];
                    int rEat = this.Row + dEatRow[i];
                    int cEat = this.Col + dEatCol[i];
                    int rFirst = this.Row + dFirstRow[i];
                    int cFirst = this.Col + dFirstCol[i];

                    if (i == 0)
                    {
                        if (IsOnBoard(rFirst, cFirst) == true)
                        {
                            Piece? target = Board.getPieceAt(rFirst, cFirst);
                            if (target == null || !IsFriendly(target))
                            {
                                moves.Add((rFirst, cFirst));
                            }
                            else if (IsOnBoard(rEat, cEat) == true)
                            {
                                target = Board.getPieceAt(rEat, cEat);
                                if (target != null && !IsFriendly(target))
                                {
                                    moves.Add((rEat, cEat));
                                }
                            }
                        }

                    }
                    else if (IsOnBoard(r, c) == true)
                    {
                        Piece? target = Board.getPieceAt(r, c);
                        if (target == null || !IsFriendly(target))
                        {
                            moves.Add((r, c));
                        }
                        else if (IsOnBoard(rEat, cEat) == true)
                        {
                            target = Board.getPieceAt(rEat, cEat);
                            if (target != null && !IsFriendly(target))
                            {
                                moves.Add((rEat, cEat));
                            }
                        }
                    }

                }
            }
            else 
            {
                int[] dRow = { -1, 0 };
                int[] dCol = { 0 , 0 };
                int[] dEatRow = { -1, -1};
                int[] dEatCol = { -1, 1};
                int[] dFirstRow = { -1, -2 };
                int[] dFirstCol = { 0, 0};

                for (int i = 0; i < 2; i++)
                {
                    int r = this.Row + dRow[i];
                    int c = this.Col + dCol[i];
                    int rEat = this.Row + dEatRow[i];
                    int cEat = this.Col + dEatCol[i];
                    int rFirst = this.Row + dFirstRow[i];
                    int cFirst = this.Col + dFirstCol[i];

                    if (i == 0)
                    {
                        if (IsOnBoard(rFirst, cFirst) == true)
                        {
                            Piece? target = Board.getPieceAt(rFirst, cFirst);
                            if (target == null || !IsFriendly(target))
                            {
                                moves.Add((rFirst, cFirst));
                            }
                            else if (IsOnBoard(rEat, cEat) == true)
                            {
                                target = Board.getPieceAt(rEat, cEat);
                                if (target != null && !IsFriendly(target))
                                {
                                    moves.Add((rEat, cEat));
                                }
                            }
                        }

                    }
                    else if (IsOnBoard(r, c) == true)
                    {
                        Piece? target = Board.getPieceAt(r, c);
                        if (target == null || !IsFriendly(target))
                        {
                            moves.Add((r, c));
                        }
                        else if (IsOnBoard(rEat, cEat) == true)
                        {
                            target = Board.getPieceAt(rEat, cEat);
                            if (target != null && !IsFriendly(target))
                            {
                                moves.Add((rEat, cEat));
                            }
                        }
                    }
                }
            }   
            return moves;
        }
    }
}
