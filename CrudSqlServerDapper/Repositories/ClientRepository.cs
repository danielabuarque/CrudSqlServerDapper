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

        /// <summary>
        /// Update Method to update an existing Client object in the database.
        /// </summary>
        /// <param name="client"></param>
        public void Update(Client client)
        {
            var query = @"
                        UPDATE CLIENT  
                        SET NAME = @Name,
                            EMAIL = @Email,
                            BIRTHDATE = @BirthDate
                        WHERE 
                            ID = @Id
                        ";

            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                connection.Execute(query, client);
            }
        }

        /// <summary>
        /// Delete Method to remove a Client object from the database by its ID.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Guid id)
        {
            var query = @"
                        DELETE FROM CLIENT
                        WHERE ID = @Id
                        ";

            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                connection.Execute(query, new { Id = id });
            }
        }

        /// <summary>
        /// Method to retrieve a Client object by its ID from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Client? GetById(Guid id)
        {
            var query = @"
                        SELECT * FROM CLIENT
                        WHERE ID = @Id
                        ";

            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                return connection.QueryFirstOrDefault<Client>(query, new { Id = id });
            }
        }



        /// <summary>
        /// Method to return all Client objects from the database.
        /// </summary>
        /// <returns></returns>
        public List<Client> GetAll()
        {
            var query = @"
                        SELECT ID, NAME, EMAIL, BIRTHDATE
                        FROM CLIENT
                        ORDER BY NAME";

            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                //Client represents the entity type
                return connection.Query<Client>(query).ToList();
            }
        }
    }
}
