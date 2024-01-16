using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Utility
    {
        public string connString;
        public string[] pNames = new string[2];
        public int timeControl = 600; // 10 minutes by default

       

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
            stockfishStreamWriter.WriteLine("go movetime 1000");

            // Wait for Stockfish to respond with the best move or game result
            string response = "Illegal";  // Default to "Illegal" in case of an exception
            try
            {
                while (true)
                {
                    string line = stockfishStreamReader.ReadLine();
                    if (line == null)
                    {
                        // Handle the case where the StreamReader returns null (end of stream)
                        break;
                    }

                    if (line.StartsWith("bestmove"))
                    {
                        response = line;
                        break;
                    }
                    else if (line.StartsWith("info"))
                    {
                        // Process evaluation information if needed
                    }
                    else if (line.StartsWith("mate"))
                    {
                        // Handle checkmate
                        response = "Checkmate";
                        break;
                    }
                    else if (line.StartsWith("score"))
                    {
                        // Process the current evaluation score if needed
                    }
                }
            }
            catch (Exception)
            {
                // Handle other exceptions if needed
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
    }

}


