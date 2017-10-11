using System;
using System.Linq;

namespace GameOfLife.Domain.Models
{
	public class BoardState
	{
		private bool[,] _currentState;
		private bool[,] _evolvedState;

		public int RowCount { get; }
		public int ColumnCount { get; }

		public BoardState(int rows, int columns)
		{
			if (rows <= 0 || columns <= 0)
				throw new ArgumentOutOfRangeException(
					"Invalid rows and columns provided, rows and columsn needs to be positive integers");

			RowCount = rows;
			ColumnCount = columns;

			_currentState = new bool[RowCount, ColumnCount];
		}

		public bool this[int x, int y]
		{
			get => _currentState[x, y];
			set => _currentState[x, y] = value;
		}

		public void ForceState(int x, int y, bool state)
		{
			if (x >= RowCount || y >= ColumnCount)
				throw new ArgumentOutOfRangeException("range is outside play area, cant force set value");

			this[x, y] = state;
		}

		public void EvolveState()
		{
			_evolvedState = new bool[RowCount, ColumnCount];

			for (int i = 0; i < RowCount; i++)
			{
				for (int j = 0; j < ColumnCount; j++)
				{
					EvolveTile(i, j);
				}
			}

			_currentState = _evolvedState;
		}

		private void EvolveTile(int x, int y)
		{
			var numberOfActiveNeighborTiles = NumberOfActiveNeighbors(x, y);

			var currentTileState = _currentState[x, y];

			var alive = CheckRulesForTile(currentTileState, numberOfActiveNeighborTiles);

			_evolvedState[x, y] = alive;
		}

		private bool CheckRulesForTile(bool currentStateOfTile, int numberOfNeigbors)
		{
			bool result = false;

			if (currentStateOfTile && (numberOfNeigbors == 2 || numberOfNeigbors == 3))
			{
				return true;
			}
			else if (!currentStateOfTile && numberOfNeigbors == 3)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Check the 8 tiles next to the point and return a integer value that represent the number of active tiles
		/// </summary>
		/// <returns></returns>
		private int NumberOfActiveNeighbors(int x, int y)
		{
			var aliveLeftCentre = IsAlive(x, y, -1, 0);
			var aliveLeftBot = IsAlive(x, y, -1, 1);
			var aliveCentreBot = IsAlive(x, y, 0, 1);
			var aliveRightBot = IsAlive(x, y, 1, 1);
			var aliveRightCentre = IsAlive(x, y, 1, 0);
			var aliveRightTop = IsAlive(x, y, 1, -1);
			var aliveCentreLeft = IsAlive(x, y, 0, -1);
			var aliveLeftTop = IsAlive(x, y, -1, -1);

			var numberOfActive = CountTrue(aliveLeftCentre, aliveLeftBot, aliveCentreBot, aliveRightBot, aliveRightCentre,
				aliveRightTop, aliveCentreLeft, aliveLeftTop);

			return numberOfActive;
		}

		private int CountTrue(params bool[] args)
		{
			return args.Count(t => t);
		}

		private bool IsAlive(int x, int y, int offsetX, int offsetY)
		{
			int newX = x + offsetX;
			int newY = y + offsetY;
			bool outOfBounds = newX < 0 || newX >= RowCount | newY < 0 ||
			                   newY >= ColumnCount;
			if (!outOfBounds)
			{
				return _currentState[newX, newY];
			}

			return false;
		}
	}
}
