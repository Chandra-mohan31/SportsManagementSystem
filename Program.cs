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
            RemoveTournament(4,conn);
            conn.Close();
        }
    }
}