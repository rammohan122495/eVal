using eValDTO.DTOs;
using eValRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eValRepository.Concrete
{
    public class RunReportRepository : IRunReportRepository
    {
        private readonly string _connectionString;
        public RunReportRepository(IConfiguration configuration) 
        {
            _connectionString = configuration.GetConnectionString("SqlConnection");
        }

        public async Task<List<AssessmentDetails>> GetReportDetails()
        {
            try
            {
                var audits = new List<AssessmentDetails>();

                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand("Get_All_ReportRunAudit", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var audit = new AssessmentDetails
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                AssessmentId = reader.GetInt32(reader.GetOrdinal("AssessmentId")),
                                AttemptId = reader.GetInt32(reader.GetOrdinal("AttemptId")),
                                IsProcessed = reader.GetBoolean(reader.GetOrdinal("Processed"))
                            };

                            audits.Add(audit);
                        }
                    }
                }

                return audits;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
