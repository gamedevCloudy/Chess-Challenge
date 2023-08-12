using ChessChallenge.API;

public class MyBot : IChessBot
{
    public Move Think(Board board, Timer timer)
    {
        Move[] moves = board.GetLegalMoves();
        
        // Evaluate each move and choose the one with the highest score
        Move bestMove = moves[0];
        int bestMoveScore = EvaluateMove(board, bestMove);

        foreach (Move move in moves)
        {
            int moveScore = EvaluateMove(board, move);

            if (moveScore > bestMoveScore)
            {
                bestMove = move;
                bestMoveScore = moveScore;
            }
        }

        return bestMove;
        
    }


    int EvaluateMove(Board b, Move m)
    {
        int score = 0;

        if(m.IsCapture)
        {
            switch(m.CapturePieceType)
            {
                case PieceType.Pawn:
                    score += 1;
                    break;
                case PieceType.Knight:
                    score += 3;
                    break;
                case PieceType.Bishop:
                    score += 4;
                    break;
                case PieceType.Rook:
                    score += 5;
                    break;
                case PieceType.Queen:
                    score += 9;
                    break;
                case PieceType.King:
                    score += 15;
                    break;
            }
        }
        if (m.IsPromotion) score += 12;

        if (b.SquareIsAttackedByOpponent(m.TargetSquare)) score -= 5;

        b.MakeMove(m);
        if (b.IsInCheck()) score += 13;
        else if (b.IsInCheckmate()) score += 15;

        b.UndoMove(m);

        return score; 
    }
}
