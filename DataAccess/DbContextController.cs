using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.TicketData;

namespace DataAccess
{
    public class DbContextController
    {
        private readonly string _connection;
        public MyDbContext _dbContext { get; private set; }
        public DbContextController(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("DefaultConnection");
        }

        public List<UserData> GetUsers()
        {
            var query = "SELECT * FROM users";
            List<UserData> users = new List<UserData>();

            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserData user = new UserData()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Username = reader.GetString(reader.GetOrdinal("Username")),
                                UserType = reader.GetInt32(reader.GetOrdinal("UserType"))
                            };
                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }
        public UserData GetUserData(string email, string password)
        {
            var query = "SELECT * FROM users WHERE Email = @Email AND Password = @Password";
            UserData user = null; // Use null to indicate no user found

            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    // Use parameters to avoid SQL injection
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new UserData
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Username = reader.GetString(reader.GetOrdinal("Username")),
                                UserType = reader.GetInt32(reader.GetOrdinal("UserType"))
                            };
                        }
                    }
                }
            }

            return user; // Return null if no user was found
        }

        public bool IsValid(string Email, string Password)
        {
            var query = "SELECT * FROM users WHERE Email = @Email AND Password = @Password";

            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", Password);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())  return true;
                        else return false;
                    }
                }
            }
        }
        public UserData GetUserData(string Email)
        {
            var query = "SELECT * FROM users WHERE Email = @Email";
            UserData user = null;

            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Email", SqlDbType.VarChar).Value = Email;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = new UserData()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Username = reader.GetString(reader.GetOrdinal("Username")),
                                UserType = reader.GetInt32(reader.GetOrdinal("UserType"))
                            };
                        }
                    }
                }
            }
            return user;
        }
        public UserData GetUserData(int Id)
        {
            var query = "SELECT * FROM users WHERE Id = @Id";
            UserData user = null;

            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new UserData()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Username = reader.GetString(reader.GetOrdinal("Username")),
                                UserType = reader.GetInt32(reader.GetOrdinal("UserType"))
                            };
                        }
                    }
                }
                connection.Close();
            }
            return user;
        }
        public void AddUser(string Username, string Email, string Password, int UserType)
        {
            var query = "INSERT INTO users (Username, Email, Password, UserType) VALUES (@Username, @Email, @Password, @UserType)";
            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();
                using(var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", Username);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@UserType", UserType);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        public void DeleteUser(int Id)
        {
            var query = "DELETE FROM users WHERE Id = @Id";

            using(var connection = new SqlConnection(_connection))
            {
                connection.Open();
                using(var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    
                    var rowAffected = command.ExecuteNonQuery();

                }
                connection.Close();
            }
        }



        public List<TicketData> getTickets()
        {
            var query = "SELECT * FROM tickets";
            var tickets = new List<TicketData>();

            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var ticket = new TicketData
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                EmployeeId = reader.IsDBNull(reader.GetOrdinal("EmployeeId"))
                                    ? null
                                    : reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Status = reader.GetString(reader.GetOrdinal("Status")),
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                                ResolvedAt = reader.IsDBNull(reader.GetOrdinal("ResolvedAt"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("ResolvedAt")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                            };
                            tickets.Add(ticket);
                        }
                    }
                }
            }
            return tickets;
        }
        public List<TicketData> getTicketsByTitle(string title)
        {
            var tickets = new List<TicketData>();
            var ticket = new TicketData();

            var query = "SELECT * FROM tickets WHERE Title = @title";

            using(var connection = new SqlConnection(_connection))
            {
                connection.Open();
                using(var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", title);
                    using(var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ticket = new TicketData()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                EmployeeId = reader.IsDBNull(reader.GetOrdinal("EmployeeId"))
                                    ? null
                                    : reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Status = reader.GetString(reader.GetOrdinal("Status")),
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                                ResolvedAt = reader.IsDBNull(reader.GetOrdinal("ResolvedAt"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("ResolvedAt")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                            };
                            tickets.Add(ticket);
                        }
                    }
                }
            }
             return tickets;
        }
        public List<EmployeeData> getEmployeesInfo()
        {
            var query = "SELECT tickets.Id, ticketing.dbo.users.Username, ticketing.dbo.users.Id, ticketing.dbo.tickets.* " +
                "FROM ticketing.dbo.tickets " +
                "INNER JOIN ticketing.dbo.users ON ticketing.dbo.tickets.UserId = ticketing.dbo.users.Id";

            var employeesData = new List<EmployeeData>();
            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var employeeData = new EmployeeData()
                            {
                                TicketId = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                UserId = reader.GetInt32(2),
                                EmployeeId = reader.IsDBNull(reader.GetOrdinal("EmployeeId"))
                                    ? null
                                    : reader.GetInt32(5),
                                Description = reader.GetString(6),
                                Status = reader.GetString(7),
                                CreatedAt = reader.GetDateTime(8),
                                ResolvedAt = reader.IsDBNull(reader.GetOrdinal("ResolvedAt"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(9),

                            };
                            employeesData.Add(employeeData);
                        }
                    }
                }
            }
            return employeesData;
        }
        public EmployeeData getEmployeeInfo(int Id)
        {

            {
                var query = "SELECT * FROM users Where Id = @Id";

                var employeeData = new EmployeeData();
                using (var connection = new SqlConnection(_connection))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                employeeData = new EmployeeData()
                                {
                                    TicketId = reader.GetInt32(0),
                                    Username = reader.GetString(1),
                                    UserId = reader.GetInt32(2),
                                    EmployeeId = reader.GetInt32(5),
                                    Description = reader.GetString(6),
                                    Status = reader.GetString(7),
                                    CreatedAt = reader.GetDateTime(8),
                                    ResolvedAt = reader.GetDateTime(9)

                                };
                            }
                        }
                    }
                }
                return employeeData;
            }
        }
        public TicketData getTicketById(int Id)
        {
            var query = "SELECT * FROM tickets WHERE Id = @Id";
            var ticket = new TicketData();
            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ticket = new TicketData()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                EmployeeId = reader.IsDBNull(reader.GetOrdinal("EmployeeId"))
                                    ? null
                                    : reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Status = reader.GetString(reader.GetOrdinal("Status")),
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                                ResolvedAt = reader.IsDBNull(reader.GetOrdinal("ResolvedAt"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("ResolvedAt")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            };
                        }
                    }
                }
            }
            return ticket;
        }
        public void deleteTicket(int Id)
        {
            var query = "DELETE FROM tickets WHERE Id=@Id";

            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();
                using(var command = new SqlCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void AddTicket(string Title, string Description, DateTime CreatedAt, string Status, int UserId)
        {
            var query = "INSERT INTO tickets (Title, Description, CreatedAt, Status, UserId) VALUES (@Title, @Description, @CreatedAt, @Status, @UserId)";

            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    // Add parameters to prevent SQL injection
                    command.Parameters.AddWithValue("@Title", Title);
                    command.Parameters.AddWithValue("@Description", Description);
                    command.Parameters.AddWithValue("@CreatedAt", CreatedAt);
                    command.Parameters.AddWithValue("@Status", Status);
                    command.Parameters.AddWithValue("@UserId", UserId);

                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }
        }
        public void AssignTicket(int employeeId, int ticketId)
        {
            var query = "UPDATE tickets SET EmployeeId = @employeeId, Status = @Status WHERE Id = @ticketId";

            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@employeeId", employeeId);
                    command.Parameters.AddWithValue("@Status", "InProgress");
                    command.Parameters.AddWithValue("@ticketId", ticketId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void changeToSolved(int ticketId)
        {
            var now = DateTime.Now;
            var query = "UPDATE tickets SET Status = 'Solved', ResolvedAt = @now WHERE Id = @ticketId";

            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ticketId", ticketId);
                    command.Parameters.AddWithValue("@now", now);

                    command.ExecuteNonQuery();
                }
            }
        }
        public List<TicketData> getUserTickets(int userId)
        {
            var query = "SELECT * FROM tickets WHERE UserId = @userId";
            var tickets = new List<TicketData>();

            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();
                using(var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    using(var reader =  command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            var ticket = new TicketData()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                EmployeeId = reader.IsDBNull(reader.GetOrdinal("EmployeeId"))
                                    ? null
                                    : reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Status = reader.GetString(reader.GetOrdinal("Status")),
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                                ResolvedAt = reader.IsDBNull(reader.GetOrdinal("ResolvedAt"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("ResolvedAt")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                            };
                            tickets.Add(ticket);
                        }
                    }
                }
            }
            return tickets;
        }





        public List<ManagerData> GetManagerInfo()
        {
            var query = "SELECT tickets.Id AS TicketId, " +
                        "       users.Id AS UserId, " +
                        "       users.Username AS Username, " +
                        "       tickets.EmployeeId, " +
                        "       users.UserType AS UserType, " +
                        "       tickets.CreatedAt AS CreatedAt, " +
                        "       tickets.ResolvedAt AS ResolvedAt, " +
                        "       tickets.Status AS TicketStatus " +
                        "FROM ticketing.dbo.tickets AS tickets " +
                        "INNER JOIN ticketing.dbo.users AS users " +
                        "ON tickets.UserId = users.Id;";

            List<ManagerData> list = new List<ManagerData>();

            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var info = new ManagerData()
                            {
                                TicketId = reader.GetInt32(reader.GetOrdinal("TicketId")),
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                EmployeeId = reader.IsDBNull(reader.GetOrdinal("EmployeeId"))
                                    ? null
                                    : reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                UserType = reader.GetInt32(reader.GetOrdinal("UserType")),
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                                ResolvedAt = reader.IsDBNull(reader.GetOrdinal("ResolvedAt"))
                                    ? (DateTime?) null
                                    : reader.GetDateTime("ResolvedAt"),
                                Status = reader.GetString(reader.GetOrdinal("TicketStatus")),
                                Username = reader.GetString(reader.GetOrdinal("Username")),
                            };
                            list.Add(info);
                        }
                    }
                }
            }
            return list;
        }


        public List<ManagerData> GetManagerInfo(string title)
        {
            var query = "SELECT tickets.Id AS TicketId, " +
            "       users.Id AS UserId, " +
            "       users.Username AS Username, " +
            "       tickets.EmployeeId, " +
            "       users.UserType AS UserType, " +
            "       tickets.CreatedAt AS CreatedAt, " +
            "       tickets.ResolvedAt AS ResolvedAt, " +
            "       tickets.Status AS TicketStatus " +
            "FROM ticketing.dbo.tickets AS tickets " +
            "INNER JOIN ticketing.dbo.users AS users " +
            "ON tickets.UserId = users.Id " +
            "WHERE tickets.Title = @ticket;";


            List<ManagerData> list = new List<ManagerData>();

            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ticket", title);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var info = new ManagerData()
                            {
                                TicketId = reader.GetInt32(reader.GetOrdinal("TicketId")),
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                EmployeeId = reader.IsDBNull(reader.GetOrdinal("EmployeeId"))
                                    ? null
                                    : reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                UserType = reader.GetInt32(reader.GetOrdinal("UserType")),
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                                ResolvedAt = reader.IsDBNull(reader.GetOrdinal("ResolvedAt"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime("ResolvedAt"),
                                Status = reader.GetString(reader.GetOrdinal("TicketStatus")),
                                Username = reader.GetString(reader.GetOrdinal("Username")),
                            };
                            list.Add(info);
                        }
                    }
                }
            }
            return list;
        }



    }
}
