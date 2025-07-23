using CrudSqlServerDapper.Entities;
using CrudSqlServerDapper.Settings;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudSqlServerDapper.Repositories
{
    /// <summary>
    /// Repository for CRUD operations on Client entities.
    /// </summary>
    public class ClientRepository
    {
        /// <summary>
        /// Private attribute to hold an instance of AppSettings.
        /// </summary>
        private AppSettings _appSettings = new AppSettings();

        /// <summary>
        /// Method to insert a new Client object into the database.
        /// </summary>
        /// <param name="client"></param>
        public void Insert(Client client)
        {
            var query = @"INSERT INTO CLIENT (ID, NAME, EMAIL, BIRTHDATE)
                        VALUES (@Id, @Name, @Email, @BirthDate)
                        ";

            //Open a connection to the database and execute the query
            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                connection.Execute(query, client);
            }
        }
    }
}
