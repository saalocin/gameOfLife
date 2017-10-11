using System;

namespace GameOfLife.Domain.Models
{
	public class Board
	{
		private BoardState _boardState = null;

		public Board(int numberOfRows, int numberOfColums)
		{
			if (numberOfRows <= 0 || numberOfColums <= 0)
				throw new ArgumentOutOfRangeException(
					"Invalid rows and columns provided, rows and columsn needs to be positive integers");

			_boardState = new BoardState(numberOfRows, numberOfColums);
		}

		public void Evolve(int numberOfGenrations = 1)
		{
			_boardState.EvolveState();
		}

		public void SetValue(int x, int y, bool value)
		{
			_boardState[x, y] = value;
		}

		public int ColumnLength => _boardState.ColumnCount;

		public int RowLength => _boardState.RowCount;


		public bool this[int x, int y]
		{
			get => _boardState[x, y];
		}

		
	}
}
