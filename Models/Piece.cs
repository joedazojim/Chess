using System;
using System.Collections.Generic;
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

    public class Piece {
        public Color Color { get; set; }
        public PieceType Type { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public Piece(PieceType type, Color color)
        {
            Type = type;
            Color = color;
        }
    }
}
