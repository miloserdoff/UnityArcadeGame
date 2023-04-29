using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;
using System;

public class DataBase : MonoBehaviour
{
    private bool isAuthorized;
    private string dataBase = "URI=file:User.db";
    public Text _coins;
    public Button LevelOneButton;
    public Button LevelTwoButton;
    public Canvas RegPanel;
    public Canvas AuthPanel;

    public Canvas MoneyInfo;

    public Button RegButton;
    public Button AuthButton;

    public Text RegLogin;
    public Text RegPassword;

    public Text AuthLogin;
    public Text AuthPassword;

    public Text WelcomeText;
    public Text InformationText;

    void Start()
    {
        RegPanel.enabled = false;
        AuthPanel.enabled = false;
        ClearDB();
        CreateDB();

        WelcomeText.enabled = false;
        if (!isAuthorized)
        {
            MoneyInfo.gameObject.SetActive(false);
            LevelOneButton.gameObject.SetActive(false);
            LevelTwoButton.gameObject.SetActive(false);
            LevelOneButton.enabled = false;
            LevelTwoButton.enabled = false;
        }

    }

    void Update()
    {
        if (isAuthorized)
        {
            MoneyInfo.gameObject.SetActive(true);
            LevelOneButton.enabled = true;
            LevelTwoButton.enabled = true;
            InformationText.gameObject.SetActive(false);
            LevelOneButton.gameObject.SetActive(true);
            LevelTwoButton.gameObject.SetActive(true);
        }
    }

    public void ShowRegPanel()
    {
        RegPanel.enabled = true;
    }

    public void ShowAuthPanel()
    {
        AuthPanel.enabled = true;
    }

    public void AuthorizeToAccount()
    {
        Authorize(AuthLogin.text, AuthPassword.text);
        AuthPanel.enabled = false;
    }

    public void CreateAccount()
    {
        AddMember(RegLogin.text, RegPassword.text);
        RegPanel.enabled = false;
    }

    public void Authorize(string login, string password)
    {
        using (var connection = new SqliteConnection(dataBase))
        {
            connection.Open();

            var adapter = new SqliteDataAdapter();
            var table = new DataTable();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT * FROM User WHERE Name = '{login}' AND Password = '{password}'";
                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count == 1)
                {
                    WelcomeText.enabled = true;
                    WelcomeText.text += login + "!";
                    RegButton.gameObject.SetActive(false);
                    AuthButton.gameObject.SetActive(false);

                    using (var getCoinsCommand = connection.CreateCommand())
                    {
                        getCoinsCommand.CommandText = $"SELECT Money FROM User WHERE Name = '{login}'";
                        using (var reader = getCoinsCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _coins.text = reader["Money"].ToString();
                            }
                        }
                    }

                    isAuthorized = true;
                }

                else
                {
                    InformationText.text = "Такого аккаунта не существует!";
                }
            }
        }
    }

    public void CreateDB()
    {
        using (var connection = new SqliteConnection(dataBase))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS User (Name VARCHAR(200), Password VARCHAR(200), Money INT);";
                command.ExecuteNonQuery();
                Debug.Log("Success");
            }

            connection.Close();
        }
    }

    public void ShowInformation()
    {
        using (var connection = new SqliteConnection(dataBase))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM User;";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.Log(reader);
                    }
                }
            }
        }
    }

    public void ClearDB()
    {
        using (var connection = new SqliteConnection(dataBase))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DROP TABLE User;";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void AddMember(string login, string password)
    {
        using (var connection = new SqliteConnection(dataBase))
        {
            connection.Open();

            var adapter = new SqliteDataAdapter();
            var table = new DataTable();

            using (var isExistAccountCommand = connection.CreateCommand())
            {
                isExistAccountCommand.CommandText = $"SELECT * FROM User WHERE Name = '{login}'";
                adapter.SelectCommand = isExistAccountCommand;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    InformationText.text = "Такой аккаунт уже есть!";
                }

                else
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"INSERT INTO User (Name, Password, Money) VALUES ('{login}', '{password}', 5);";
                        command.ExecuteNonQuery();
                    }

                    Debug.Log("Создано!");
                    InformationText.text = "Зарегистрировано! Теперь авторизуйтесь!";
                }

                connection.Close();

            }
        }
    }
    public void ShowScore()
    {
        try
        {
            using (var connection = new SqliteConnection(dataBase))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM User;";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Debug.Log($"Name: {reader["Name"]}. Password: {reader["Password"]}. Money: {reader["Money"]}");
                            var money = reader["Money"].ToString();
                            _coins.text = money;
                        }

                        reader.Close();
                    }
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
