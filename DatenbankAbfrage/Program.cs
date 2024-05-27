using System;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;



string connectionString = "Server=localhost;Database=egamedarling;User Id=root;Password=;";



bool loggedIn = false;

while (!loggedIn)
{
    // Benutzername und Passwort von der Konsole einlesen
    Console.WriteLine("Bitte geben Sie den Benutzernamen ein:");
    string username = Console.ReadLine();
    Console.WriteLine("Bitte geben Sie das Passwort ein:");
    string password = Console.ReadLine();

    string query = "SELECT * FROM t_accounts WHERE nickname = @Username AND passwort = @Password";

    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@Username", username);
        command.Parameters.AddWithValue("@Password", password);

        try
        {
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                Console.WriteLine("Anmeldung erfolgreich. Willkommen, " + username + "!");
                loggedIn = true;
            }
            else
            {
                Console.WriteLine("Anmeldung fehlgeschlagen. Benutzername oder Passwort ist falsch.");
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fehler: " + ex.Message);
        }
    }
}



