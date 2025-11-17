using Microsoft.Extensions.Diagnostics.HealthChecks;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Work360.Infrastructure.Health
{
    public class OracleHealthCheck : IHealthCheck
    {
        private readonly string _connectionString;

        public OracleHealthCheck(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                using var connection = new OracleConnection(_connectionString);
                await connection.OpenAsync(cancellationToken);

                // Você pode até executar um SELECT 1 para testar a query
                return HealthCheckResult.Healthy("Conexão com Oracle OK!");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Falha ao conectar ao Oracle", ex);
            }
        }
    }
}
