﻿using Dapper;
using EmailService.Infrastructure.IRepository;
using EmailService.Models.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string _conString;
        public UserRepository( IConfiguration config)
        {
            _conString = config.GetConnectionString("DefaultConnection");
        }

        public async Task<GetUserByNameResponse> GetUserByName(GetUserByNameCommand command)
        {
            try
            {
                using ( var connection = new SqlConnection(_conString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@FirstName", command.FirstName);
                    parameters.Add("@LastName", command.LastName);
                    parameters.Add("@Message", dbType: DbType.String, size: 50, direction: ParameterDirection.Output);

                    var result = await connection.QueryAsync<Employee>("GetNameById", parameters, commandType: CommandType.StoredProcedure);
                    var employee = result.Single();
                    var message = parameters.Get<string>("@Message");

                    if (result == null)
                    {
                        return new GetUserByNameResponse
                        {
                            IsSuccess = false,
                            Message = message
                        };
                    }
                    else
                    {
                        return new GetUserByNameResponse
                        {
                            IsSuccess = true,
                            Message = message,
                            employee = employee
                        };
                    }
                }
            }
            catch(Exception ex)
            {
                return new GetUserByNameResponse
                {
                    IsSuccess = false,
                    Message = $"Error: { ex.Message }"
                };
            }
        }
    }
}
