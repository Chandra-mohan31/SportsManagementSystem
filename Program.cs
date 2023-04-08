using System.Data;
using System.Linq.Expressions;
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
            try
            {
                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@sportName", SqlDbType.VarChar).Value = Name;
                cmd.Parameters.Add("@numberOfPlayers", SqlDbType.Int).Value = no_players;

                cmd.CommandText = $"AddSport";
                cmd.ExecuteReader().Close();
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
            }
        }

        public static void AddScoreCard(int sportId,string team1,string team2,int team1Score,int team2Score,string winner, SqlConnection conn)
        {
            try
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
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
            }
        }
   
        public static void AddTournament(string tournamentName, int sportId, string tournamentDate, SqlConnection conn)
        {
            try
            {
                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@tournamentName", SqlDbType.VarChar).Value = tournamentName;
                cmd.Parameters.Add("@sportId", SqlDbType.Int).Value = sportId;
                cmd.Parameters.Add("@tournamentDate", SqlDbType.DateTime).Value = DateTime.Parse(tournamentDate);

                cmd.CommandText = "AddTournament";
                cmd.ExecuteReader().Close();
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
            }
        }

        public static void RemoveSport(int sportId, SqlConnection conn)
        {
            try
            {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@sportId", SqlDbType.Int).Value = sportId;
            cmd.CommandText = $"RemoveSport";

            
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
            try
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
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
            }

        }
        public static void RemovePlayer(int playerId, SqlConnection conn)
        {
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@playerId", SqlDbType.Int).Value = playerId;

                cmd.CommandText = "RemovePlayer";
                cmd.ExecuteNonQuery();
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
            }
        }
        public static void RemoveTournament(int tournamentId, SqlConnection conn)
        {
            try
            {
                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@tournamentId", SqlDbType.Int).Value = tournamentId;

                cmd.CommandText = $"RemoveTournament";
                cmd.ExecuteReader().Close();
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
            }
        }

        public static void GetSportsAvailable(SqlConnection conn)
        {
            try
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
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
            }
        }
 
        public static void GetTableData(SqlConnection conn,string tableName)
        {
            try
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
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
            }
        }
        //associate tournament id to score card
        public static void RegisterIndividual(SqlConnection conn,string ind)
        {
            SqlCommand cmd = conn.CreateCommand();
            Console.WriteLine("Sports and Tournament Available");
            
            cmd.CommandText = "select t.tournamentId,s.sportId,s.sportName,t.tournamentName,t.tournamentDate from SportsTable s join Tournament t on t.sportId = s.sportId where s.numberOfPlayers = 1;";
          
            
            var reader = cmd.ExecuteReader();
            if(reader.HasRows)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader[i] + "\t");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No records found..");
            }
            reader.Close();
            
            Console.WriteLine("Enter the tournament Id: ");
            int t_id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Player name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the Sport ID:");
            int s_id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the age:");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the DOB:");
            string dob = Console.ReadLine();
            Console.WriteLine("College Name: ");
            string collegeName = Console.ReadLine();
            string paidS = "not paid";
            try
            {
                //cmd.CommandText = $"INSERT INTO Players ({s_id}, '{name}', {age}, '{DateTime.Parse(dob)}', '{collegeName}', {t_id})";

                //cmd.ExecuteNonQuery();
                //cmd.CommandText = $"INSERT INTO PLAYERREGISTRATION VALUES ({t_id}, '{name}', {s_id}, {age}, '{DateTime.Parse(dob)}', '{collegeName}','{ind}' )";
                //cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO Players (sportId, playerName, age, dob, TeamName, tournamanet_ID) VALUES (@sportId, @playerName, @age, @dateOfBirth, @collegeName, @tournamentId)";
                cmd.Parameters.AddWithValue("@sportId", s_id);
                cmd.Parameters.AddWithValue("@playerName", name);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@dateOfBirth", DateTime.Parse(dob));
                cmd.Parameters.AddWithValue("@collegeName", collegeName);
                cmd.Parameters.AddWithValue("@tournamentId", t_id);
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO PLAYERREGISTRATION (tournament_id, player_name, sportsId, age, dob, collegeName, groupOrIndividual,paid) VALUES (@tournamentId, @playerName, @sportId, @age, @dob, @collegeName, @groupOrIndividual, @paid)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@tournamentId", t_id);
                cmd.Parameters.AddWithValue("@playerName", name);
                cmd.Parameters.AddWithValue("@sportId", s_id);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@dob", DateTime.Parse(dob));
                cmd.Parameters.AddWithValue("@collegeName", collegeName);
                cmd.Parameters.AddWithValue("@groupOrIndividual", ind);
                cmd.Parameters.AddWithValue("@paid", paidS);
                cmd.ExecuteNonQuery();

            }
            catch (SqlException ex){
                Console.WriteLine(ex.Message);
                return;
            }
            finally
            {
                Console.WriteLine("Registration Completed");
            }
            
            
            

        }

        public static void RegisterGroup(SqlConnection conn)
        {
            Console.WriteLine("group sports registration");
            SqlCommand cmd = conn.CreateCommand();

            //showing user the sports that are avilable in the tournament to participate
            Console.WriteLine("Sports and Tournament Available");

            cmd.CommandText = "select t.tournamentId,s.sportId,s.sportName,t.tournamentName,t.tournamentDate from SportsTable s join Tournament t on t.sportId = s.sportId where s.numberOfPlayers > 1;";


            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader[i] + "\t");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No records found..");
            }
            reader.Close();

            //registering teams 
            Console.WriteLine("Enter team name:");
            string teamName = Console.ReadLine();

            Console.WriteLine("Enter tournament ID:");
            int tournamentId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter sport ID:");
            int sportId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter category:");
            string category = Console.ReadLine();
            cmd.CommandText = $"select numberOfPlayers from SportsTable where sportId = {sportId}";
            int numOfPlayers = (int)cmd.ExecuteScalar();
            Console.WriteLine($"Enter {numOfPlayers} player names one by one: ");
            string playerNames = "";
            for(int i=1;i<=numOfPlayers;i++)
            {
                
                playerNames += Console.ReadLine();
                if(i != numOfPlayers)
                {
                    playerNames += ",";
                }
            }
            string query = "INSERT INTO TEAMSREGISTRATION (tname, tournamentId, sportId, category, players) VALUES (@tname, @tournamentId, @sportId, @category, @players)";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@tname", teamName);
            cmd.Parameters.AddWithValue("@tournamentId", tournamentId);
            cmd.Parameters.AddWithValue("@sportId", sportId);
            cmd.Parameters.AddWithValue("@category", category);
            cmd.Parameters.AddWithValue("@players", playerNames);
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Team registration successful!");
            }
            else
            {
                Console.WriteLine("Team registration failed.");
            }
        }

        public static void MakePayment(SqlConnection conn)
        {
            SqlCommand cmd = conn.CreateCommand();
            Console.WriteLine("Choose an option \n 1 : Group Sport \n 2 : Individual Sport");
            int c = int.Parse(Console.ReadLine());
            if(!(c == 1 || c == 2) ){
                Console.WriteLine("Please enter a valid input: ");
            }
            else
            {
                Console.WriteLine("Initiating payment : \n ");
                if(c == 1)
                {
                    Console.WriteLine("your fees is Rs.1000 \n ");
                   
                    Console.WriteLine("Enter your TID..");
                    int TID = int.Parse(Console.ReadLine());
                    cmd.CommandText = $"update TEAMSREGISTRATION set paid = 'paid' where tid={TID} and paid = 'not paid'";
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if(rowsAffected > 0)
                    {
                        Console.WriteLine("Payment updated Sucessfully...");
                    }
                    else
                    {
                        Console.WriteLine("Payment failed...");
                    }

                }else
                {
                    Console.WriteLine("your fees is Rs.180 \n ");
                    
                    Console.WriteLine("Enter your RegistrationID..");
                    int REG_ID = int.Parse(Console.ReadLine());
                    cmd.CommandText = $"update PLAYERREGISTRATION set paid = 'paid' where registration_id={REG_ID} and paid ='not paid';";
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Payment updated Sucessfully...");
                    }
                    else
                    {
                        Console.WriteLine("Payment failed...");
                    }
                }
            }
        }


        static void Main(string[] args)
        {
            string connString = "Data Source=F48DPF2;Initial Catalog=SportsManagementSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";


            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            

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
                Console.WriteLine("8. Register Individual");
                Console.WriteLine("9. Register Group");
                Console.WriteLine("10. Make Payment for registration");
                Console.WriteLine("0.Exit");


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
                            Console.WriteLine("Current Score Cards:");
                            GetTableData(conn,"ScoreCard");
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
                            Console.WriteLine("Register Individual:");
                            RegisterIndividual(conn,"individual");

                            break; 
                        case 9:
                            Console.WriteLine("Register Group");
                            RegisterGroup(conn);

                            break;
                        case 10:
                            Console.WriteLine("Make a Payment for registration:");
                            MakePayment(conn);
                            break;
                        case 0:
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