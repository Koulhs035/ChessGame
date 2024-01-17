using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
namespace ChessGame
{
    public class Utility
    {
        // Game settings
        public string connString = "Data Source=Database.db;Version=3;";
        public string[] pNames = new string[2];
        public int timeControl = 600; // 10 minutes by default

        // Stockfish settings
        public bool engineToPlay = false;
        public int searchDepth = 10;

        // Database
        public string movesDone = string.Empty;
        public string score;

        public static string FormatTime(int seconds)
        {
            // These are all settings for the game
            int minutes = seconds / 60;
            int remainingSeconds = seconds % 60;
            string formattedTime = $"{minutes}:{remainingSeconds:D2}";
            return formattedTime;
        }

        //---------------------------------------------StockFish---------------------------------------------//
        private Process stockfishProcess;
        private StreamWriter stockfishStreamWriter;
        private StreamReader stockfishStreamReader;
        public void StartStockfish()
        {
            string stockfishPath = Path.Combine("Tools", "stockfish.exe");

            // Start Stockfish process
            stockfishProcess = new Process();
            stockfishProcess.StartInfo.FileName = stockfishPath;
            stockfishProcess.StartInfo.UseShellExecute = false;
            stockfishProcess.StartInfo.RedirectStandardInput = true;
            stockfishProcess.StartInfo.RedirectStandardOutput = true;
            stockfishProcess.StartInfo.CreateNoWindow = true;
            stockfishProcess.Start();

            stockfishStreamWriter = stockfishProcess.StandardInput;
            stockfishStreamReader = stockfishProcess.StandardOutput;

            // Send initial UCI commands
            stockfishStreamWriter.WriteLine("uci");
            stockfishStreamWriter.WriteLine("isready");
            stockfishStreamWriter.WriteLine("ucinewgame");
        }

        public string GetStockfishMove(string fen)
        {
            // Set the position on the chessboard
            stockfishStreamWriter.WriteLine($"position fen {fen}");

            // Instruct Stockfish to calculate the best move
            stockfishStreamWriter.WriteLine($"go depth {searchDepth}");

            // Wait for Stockfish to respond with the best move or game result
            string response = "Illegal";  // Default to "Illegal" in case of an exception

            while (true)
            {
                string line = stockfishStreamReader.ReadLine();
                if (line == null)
                {
                    // Handle the case where the StreamReader returns null (end of stream)
                    break;
                }

                try
                {
                    if (line.StartsWith("bestmove"))
                    {
                        // Extract the best move information
                        string[] parts = line.Split(' ');
                        if (parts.Length >= 2)
                        {
                            // The move is in the second part
                            response = parts[1];
                        }
                        break;
                    }
                    else if (line.StartsWith("mate"))
                    {
                        // Handle checkmate
                        response = "Checkmate";
                        break;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("ERROR!\n" + ex.ToString());
                }
            }

            return response;
        }

        public void StopStockfish()
        {
            // Send quit command to Stockfish
            stockfishStreamWriter.WriteLine("quit");

            // Close the process
            stockfishProcess.WaitForExit();
            stockfishProcess.Close();
        }
        //---------------------------------------------Database---------------------------------------------//
        public void InitializeTable()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connString))
            {
                connection.Open();
                string createTableSQL = "CREATE TABLE IF NOT EXISTS Games(" +
                    "ID INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "player1 TEXT," +
                    "player2 TEXT," +
                    "time INTEGER," +
                    "score TEXT," +
                    "positions TEXT," +
                    "date DATETIME" +
                    ")";

                using (SQLiteCommand command = new SQLiteCommand(createTableSQL, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddToDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connString))
            {
                try
                {
                    connection.Open();
                    string insertSQL = "INSERT INTO Games (player1,player2, time, score, positions, date) " +
                                    "VALUES (@player1, @player2, @time, @score, @positions, @date)";

                    using (SQLiteCommand command = new SQLiteCommand(insertSQL, connection))
                    {
                        command.Parameters.AddWithValue("@player1", pNames[0]);
                        command.Parameters.AddWithValue("@player2", pNames[1]);
                        command.Parameters.AddWithValue("@time", timeControl);
                        command.Parameters.AddWithValue("@score", score);
                        command.Parameters.AddWithValue("@positions", movesDone);
                        command.Parameters.AddWithValue("@date", DateTime.Now);

                        int rowsInserted = command.ExecuteNonQuery();
                        if (rowsInserted > 0)
                            Console.WriteLine("Successfully added game to Database!");
                        else
                            Console.WriteLine("No rows were inserted!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not add to Database!\n" + ex.Message);
                }
            }
        }
    }

}