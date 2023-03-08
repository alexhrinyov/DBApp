﻿using BDApp;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBApp
{
    public class DbExecutor
    {
        private MainConnector connector;
        public DbExecutor(MainConnector connector)
        {
            this.connector = connector;
        }

        public DataTable SelectAll(string table)
        {
            var selectcommandtext = "select * from " + table;
            var adapter = new SqlDataAdapter(
              selectcommandtext,
              connector.GetConnection()
            );
            var  ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];

        }

        // через присоединенную модель
        public SqlDataReader SelectAllCommandReader(string table)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = "select * from " + table,
                Connection = connector.GetConnection()
            };
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                return reader;
            }

            return null;
        }

        public int DeleteByColumn(string table, string column, string value)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = "delete from " + table + " where " + column + " = '" + value + "';",
                Connection = connector.GetConnection(),
            };
            return command.ExecuteNonQuery();

        }

        public int ExecProcedureAdding( string name, string login)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "AddingUserProc",
                Connection = connector.GetConnection()
            };
            command.Parameters.Add(new SqlParameter("@Name", name));
            command.Parameters.Add(new SqlParameter("@Login", login));
            return command.ExecuteNonQuery();

        }

        public int UpdateByLogin(string table, string login, string value)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = $"update {table} set Name = '{value}' where Login = '{login}'; " ,
                Connection = connector.GetConnection(),
            };
            return command.ExecuteNonQuery();
        }


    }
}
