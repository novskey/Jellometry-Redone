using System;
using System.Collections;
using System.Collections.Generic;
using UnityNpgsql;
using UnityEngine;
using UnityNpgsqlTypes;

namespace Assets.Scripts.Managers
{
    public class DBInterface
    {
        private static NpgsqlConnection _conn;

        public static IEnumerator Connect()
        {
            // PostgeSQL-style connection string
            string connstring = String.Format("Server={0};Port={1};" +
                                              "User Id={2};Password={3};Database={2};",
                                              "elmer-02.db.elephantsql.com", "5432", "mosurrse",
                                              "7GBFVFoKYBAxm1wkfC_OAtB_9CxAfRBA" );

            // Making connection with Npgsql provider
            _conn = new NpgsqlConnection(connstring);

            _conn.Open();
            yield return null;
        }

        public static IEnumerator CloseConnection()
        {
            _conn.Close();
            yield return null;
        }

        public static IEnumerator SaveScore(string name, float score)
        {
            DateTime time = DateTime.UtcNow;

            string sql = String.Format("INSERT INTO highscores VALUES ('{0}',{1},'{2}')",
                                       name,score,time);

            NpgsqlCommand cmd = new NpgsqlCommand(sql, _conn);

            Debug.Log(cmd.ExecuteNonQuery());

            yield return null;
        }

        public static string[][] GetHighScores()
        {
            string sql = "SELECT *\nFROM highscores\nORDER BY highscores.score DESC\nLIMIT 200;";
            NpgsqlCommand cmd = new NpgsqlCommand(sql,_conn);

            List<string[]> results = new List<string[]>(200);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string[] result = new string[3];

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        switch (reader.GetFieldNpgsqlDbType(i))
                        {
                            case NpgsqlDbType.Varchar:
                                result[i] = reader.GetString(i);
                                break;
                            case NpgsqlDbType.Timestamp:
                                result[i] = reader.GetTimeStamp(i).Date.ToString();
                                break;
                            default:
                                result[i] = reader.GetValue(i).ToString();
                                break;
                        }
                    }
                    results.Add(result);
                }
            }
            return results.ToArray();
        }

    }
}