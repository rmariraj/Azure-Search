using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApp
{
    public class AzureRetryPolicy : ExecutionStrategy
    {
        public AzureRetryPolicy(SalesLTContext salesLTContext, int maxRetryCount, TimeSpan maxDelay)
           : base(salesLTContext, maxRetryCount, maxDelay)
        {
        }
        protected override bool ShouldRetryOn(Exception e)
        {
            bool retry = false;
            SqlException sqlException = e as SqlException;
            if (sqlException != null)
            {
                if (e is SqlException ex && IsTransient(ex))
                {
                    retry = true;
                }
            }
            return retry;
        }
        private static bool IsTransient(Exception ex)
        {
            if (ex != null)
            {
                SqlException sqlException;
                if ((sqlException = ex as SqlException) != null)
                {
                    // Enumerate through all errors found in the exception.
                    foreach (SqlError err in sqlException.Errors)
                    {
                        switch (err.Number)
                        {
                            case 49920:
                            case 49919:
                            case 49918:
                            case 40501:
                            case 10928:
                            case 10929:
                            case 10053:
                            case 10054:
                            case 10060:
                            case 40197:
                            case 40540:
                            case 40613:
                            case 40143:
                            case 4221:
                            case 4060:
                            case 233:
                            case 64:
                            case 1205:
                            case -2:
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
