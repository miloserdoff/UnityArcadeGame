using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;

public class DataBase : MonoBehaviour
{
    private string dataBase = "URI=file:score.db";
    public Text txt;
    List<string> people = new List<string>();

    void Start()
    {
        CreateDB();
    }

    void Update()
    {
        
    }

    public void CreateDB()
    {
        using (var connection = new SqliteConnection(dataBase))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXIST score (name VARCHAR(200), points INT);";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void ClearDB()
    {
        using (var connection = new SqliteConnection(dataBase))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM score;";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void AddMember(string name, int score)
    {
        using (var connection = new SqliteConnection(dataBase))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"INSERT INTO score (name, points) VALUES ('{name}', '{score}');";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void ShowScore()
    {
        using (var connection = new SqliteConnection(dataBase))
        {
            connection.Open();
            var result = "";
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM score;";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.Log($"Name: {reader["name"]}. Damage: {reader["points"]}");
                        var tmp = reader["name"].ToString();
                        people.Add(tmp);
                    }

                    reader.Close();
                }
                command.ExecuteNonQuery();
            }
            connection.Close();
            result = string.Join(", ", people);
            txt.text = result;
        }
    }
}
