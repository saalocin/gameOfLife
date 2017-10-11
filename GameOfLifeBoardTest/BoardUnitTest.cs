using System;
using GameOfLife.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeBoardTest
{
	[TestClass]
	public class BoardTest
	{
		[ExpectedException(typeof(ArgumentOutOfRangeException))] //THEN
		[TestMethod]
		public void GIVEN0Rows_WHENCreated_THENExceptionShouldBeThrown()
		{

			//GIVEN and WHEN
			var board = new Board(0, 1);

		}

		[ExpectedException(typeof(ArgumentOutOfRangeException))] //THEN
		[TestMethod]
		public void GIVEN0Columns_WHENCreated_THENExceptionShouldBeThrown()
		{
			//GIVEN and WHEN
			var board = new Board(1, 0);
		}
	
		[TestMethod]
		public void GIVENCorrectValuesWith1AliveTile_WHENEvolveIsDone_THENAllTilesShouldBeDead()
		{
			//GIVEN 
			var rows = 5;
			var columns = 5;
			var board = new Board(rows, columns);
			board.SetValue(1, 1, true);

			//WHEN
			board.Evolve();

			//THEN
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					Assert.IsFalse(board[i, j]);
				}
			}
		}
	}
}
