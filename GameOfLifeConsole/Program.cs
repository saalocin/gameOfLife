using System;
using GameOfLife.Domain.Models;

namespace GameOfLifeConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			var rows = 5;
			var columns = 5;

			var board = new Board(rows, columns);
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					board.SetValue(i, j, true);
				}
			}
			WriteUserMessageToConsole();

			WriteToConsole(board);

			while (true)
			{
				Console.ReadLine();

				ClearConsole();
				WriteUserMessageToConsole();

				board.Evolve();
				WriteToConsole(board);
			}
		}

		private static void WriteUserMessageToConsole()
		{
			Console.WriteLine("Press enter to evolve");
			Console.WriteLine("*************************************");
	}

		private static void WriteToConsole(Board gameOfLifeBoard)
		{
			for (int y = 0; y < gameOfLifeBoard.ColumnLength; y++)
			{
				for (int x = 0; x < gameOfLifeBoard.RowLength; x++)
				{
					Console.Write(gameOfLifeBoard[x, y] ? "X" : "-");
				}
				Console.WriteLine();
			}
		}

		private static void ClearConsole()
		{
			Console.Clear();
		}
	}
}
