using System.Data;
using Microsoft.Data.SqlClient;


namespace SportsManagementSystem
{
    internal class Program
    {
        public static void SportsManagementSystem(SqlConnection conn)
        {
            try
            {
                
                SqlCommand cmd = conn.CreateCommand();


                cmd.CommandText = "select * from SportsTable";

                
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader[i].ToString() + "\t");
                    }
                    Console.WriteLine();
                }
                reader.Close();
                
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
            }
        }


        public static void AddSports(string Name,int no_players, SqlConnection conn)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@sportName", SqlDbType.VarChar).Value = Name;
            cmd.Parameters.Add("@numberOfPlayers", SqlDbType.Int).Value = no_players;

            cmd.CommandText = $"AddSport";
            cmd.ExecuteReader().Close();
        }

        public static void AddScoreCard(int sportId,string team1,string team2,int team1Score,int team2Score,string winner, SqlConnection conn)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@sportId", SqlDbType.Int).Value = sportId;
            cmd.Parameters.Add("@Team1", SqlDbType.VarChar).Value = team1;
            cmd.Parameters.Add("@Team2", SqlDbType.VarChar).Value = team2;
            cmd.Parameters.Add("@team1Score", SqlDbType.Int).Value = team1Score;
            cmd.Parameters.Add("@team2Score", SqlDbType.Int).Value = team2Score;
            cmd.Parameters.Add("@winner", SqlDbType.VarChar).Value = winner;







            cmd.CommandText = $"AddScoreCard";
            cmd.ExecuteReader().Close();
        }
   
        public static void AddTournament(string tournamentName, int sportId, string tournamentDate, SqlConnection conn)
        {
            
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@tournamentName", SqlDbType.VarChar).Value = tournamentName;
            cmd.Parameters.Add("@sportId", SqlDbType.Int).Value = sportId;
            cmd.Parameters.Add("@tournamentDate", SqlDbType.DateTime).Value = DateTime.Parse(tournamentDate);

            cmd.CommandText = "AddTournament";
            cmd.ExecuteReader().Close();
        }

        public static void RemoveSport(int sportId, SqlConnection conn)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@sportId", SqlDbType.Int).Value = sportId;
            cmd.CommandText = $"RemoveSport";

            try
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("Sport and all related records successfully removed.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error removing sport: " + ex.Message);
            }
        }

        public static void EditScoreCard(int scoreCardId, int team1Score, int team2Score, string winner, SqlConnection conn)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "EditScoreCard";

            // Set the parameters
            cmd.Parameters.Add("@scoreCardId", SqlDbType.Int).Value = scoreCardId;
           
            cmd.Parameters.Add("@team1Score", SqlDbType.Int).Value = team1Score;
            cmd.Parameters.Add("@team2Score", SqlDbType.Int).Value = team2Score;
            cmd.Parameters.Add("@winner", SqlDbType.VarChar).Value = winner;

            cmd.ExecuteNonQuery();
        }
        public static void RemovePlayer(int playerId, SqlConnection conn)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@playerId", SqlDbType.Int).Value = playerId;

            cmd.CommandText = "RemovePlayer";
            cmd.ExecuteNonQuery();
        }
        public static void RemoveTournament(int tournamentId, SqlConnection conn)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@tournamentId", SqlDbType.Int).Value = tournamentId;

            cmd.CommandText = $"RemoveTournament";
            cmd.ExecuteReader().Close();
        }

        public static void GetSportsAvailable(SqlConnection conn)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from SportsTable";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //for(int i=0;i<reader.FieldCount;i++) {
                //    Console.Write($"{reader[i]} \t");
                //}
              
                Console.WriteLine(reader.GetInt32(0) + "| " + reader.GetString(1));
            }
            reader.Close();
        }
        public static void GetScoreCard(SqlConnection conn)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from ScoreCard";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write($"{reader[i]} \t");
                }

                Console.WriteLine();
            }
            reader.Close();
        }
        public static void GetTableData(SqlConnection conn,string tableName)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = $"select * from {tableName}";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write($"{reader[i]} \t");
                }

                Console.WriteLine();
            }
            reader.Close();
        }
        //associate tournament id to score card
        static void Main(string[] args)
        {
            string connString = "Data Source=F48DPF2;Initial Catalog=SportsManagementSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";


            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            //SportsManagementSystem(conn);
            //AddSports("Volley Ball", 6, conn);
            //AddScoreCard(3, "t1", "t2", 120, 130, "t3", conn);
            //AddTournament("IPL", 4, "2023-04-10",conn);
            //RemoveSport(3,conn);
            //EditScoreCard(1,5,4,"Team1",conn);
            //RemovePlayer(4,conn);

            Console.WriteLine("Collge Sports Management System:");
            Console.WriteLine("Menu: ");




            

            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Add sport");
                Console.WriteLine("2. Add scorecard");
                Console.WriteLine("3. Add tournament");
                Console.WriteLine("4. Remove sport");
                Console.WriteLine("5. Edit scorecard");
                Console.WriteLine("6. Remove player");
                Console.WriteLine("7. Remove tournament");
                Console.WriteLine("8. Exit");

                int option = int.Parse(Console.ReadLine());

                try
                {
                    switch (option)
                    {
                        case 1:
                            Console.WriteLine("Enter sport name:");
                            string sportName = Console.ReadLine();

                            Console.WriteLine("Enter number of players:");
                            int numberOfPlayers = int.Parse(Console.ReadLine());

                            AddSports(sportName, numberOfPlayers, conn);
                            Console.WriteLine("Sport added successfully");
                            break;

                        case 2:
                            Console.WriteLine("Available Sports data: ");
                            GetSportsAvailable(conn);

                            Console.WriteLine("Enter sport ID:");
                            int sportId = int.Parse(Console.ReadLine());

                            Console.WriteLine("Enter team 1:");
                            string team1 = Console.ReadLine();

                            Console.WriteLine("Enter team 2:");
                            string team2 = Console.ReadLine();

                            Console.WriteLine("Enter team 1 score:");
                            int team1Score = int.Parse(Console.ReadLine());

                            Console.WriteLine("Enter team 2 score:");
                            int team2Score = int.Parse(Console.ReadLine());

                            Console.WriteLine("Enter winner:");
                            string winner = Console.ReadLine();

                            AddScoreCard(sportId, team1, team2, team1Score, team2Score, winner, conn);
                            Console.WriteLine("Scorecard added successfully");
                            break;

                        case 3:
                            Console.WriteLine("Available Sports data: ");

                            GetSportsAvailable(conn);

                            Console.WriteLine("Enter tournament name:");
                            string tournamentName = Console.ReadLine();

                            Console.WriteLine("Enter sport ID:");
                            int tournamentSportId = int.Parse(Console.ReadLine());

                            Console.WriteLine("Enter tournament date (MM/DD/YYYY):");
                            string tournamentDate = Console.ReadLine();

                            AddTournament(tournamentName, tournamentSportId, tournamentDate, conn);
                            Console.WriteLine("Tournament added successfully");
                            break;

                        case 4:
                            Console.WriteLine("Available Sports data: ");

                            GetSportsAvailable(conn);
                            Console.WriteLine("Enter sport ID:");
                            int removeSportId = int.Parse(Console.ReadLine());

                            RemoveSport(removeSportId, conn);
                            Console.WriteLine("Sport removed successfully");
                            break;

                        case 5:
                            Console.WriteLine("Current Score Card:");
                            GetScoreCard(conn);
                            Console.WriteLine("Enter scorecard ID:");
                            int scoreCardId = int.Parse(Console.ReadLine());

                            Console.WriteLine("Enter team 1 score:");
                            int editTeam1Score = int.Parse(Console.ReadLine());

                            Console.WriteLine("Enter team 2 score:");
                            int editTeam2Score = int.Parse(Console.ReadLine());

                            Console.WriteLine("Enter winner:");
                            string editWinner = Console.ReadLine();

                            EditScoreCard(scoreCardId, editTeam1Score, editTeam2Score, editWinner, conn);
                            Console.WriteLine("Scorecard edited successfully");
                            break;

                        case 6:
                            Console.WriteLine("Players Availability:");
                            GetTableData(conn,"Players");

                            Console.WriteLine("Enter player ID:");
                            int removePlayerId = int.Parse(Console.ReadLine());

                            RemovePlayer(removePlayerId, conn);
                            Console.WriteLine("Player removed successfully");
                            break;

                        case 7:
                            Console.WriteLine("Tournaments Around:");
                            GetTableData(conn, "Tournament");
                            Console.WriteLine("Enter tournament ID:");
                            int removeTournamentId = int.Parse(Console.ReadLine());

                            RemoveTournament(removeTournamentId, conn);
                            Console.WriteLine("Tournament removed successfully");
                            break;

                        case 8:
                            return;

                        default:
                            Console.WriteLine("Invalid option");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }



            conn.Close();
        }
    }
}