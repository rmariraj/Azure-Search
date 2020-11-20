using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApp
{
    public class CustomRetryPolicy : ExecutionStrategy
    {
        public CustomRetryPolicy(ExecutionStrategyDependencies dependencies, int maxRetryCount, TimeSpan maxRetryDelay)
      : base(dependencies, maxRetryCount, maxRetryDelay) { }
        protected override bool ShouldRetryOn(Exception e)
        {
            return IsTransient(e) ? true : false;
        }
        private static bool IsTransient(Exception ex)
        {
            if (ex != null)
            {
                SqlException sqlException;
                if ((sqlException = ex as SqlException) != null)
                {
                    foreach (SqlError err in sqlException.Errors)
                    {
                        switch (err.Number)
                        {
                            //Cannot process request. Too many operations in progress for subscription "%ld".
                            case 49920:
                            //Cannot process create or update request. Too many create or update operations in progress for subscription "%ld".
                            case 49919:
                            //Cannot process request.Not enough resources to process request.
                            case 49918:
                            //The service is currently busy. Retry the request after 10 seconds. Incident ID: %ls. Code: %d. For more information
                            case 40501:
                            case 10928:
                            case 10929:
                            //A transport-level error has occurred when receiving results from the server
                            case 10053:
                            case 10054:
                            case 10060:
                            case 40197:
                            case 40540:
                            case 40613:
                            case 40143:
                            //Login to read - secondary failed due to long wait on 'HADR_DATABASE_WAIT_FOR_TRANSITION_TO_VERSIONING'.
                            case 4221:
                            //Cannot open database "%.*ls" requested by the login. The login failed.
                            case 4060:
                            case 233:
                            case 64:
                            //deadlock
                            case 1205:
                            //timeout
                            case -2:
                            //could not open a connection to SQL server
                            case 40:
                                return true;
                        }
                    }
                }
                else if (ex is TimeoutException)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
