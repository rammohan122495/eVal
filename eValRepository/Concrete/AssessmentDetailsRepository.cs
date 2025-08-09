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
    public class AssessmentDetailsRepository : IAssessmentDetailsRepository
    {
        private readonly string _connectionString;
        public AssessmentDetailsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConnection");
        }

        public async Task<List<AssessmentResultAudit>> GetAllAssessmentResultAudits(string attemptId)
        {
            var audits = new List<AssessmentResultAudit>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("Get_All_AssessmentResultAudit", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AssessmentId", attemptId);
                conn.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var audit = new AssessmentResultAudit
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            AssessmentId = reader.GetInt32(reader.GetOrdinal("AssessmentId")),
                            AttemptId = reader.GetInt32(reader.GetOrdinal("AttemptId")),
                            QuestionId = reader.GetInt32(reader.GetOrdinal("QuestionId")),
                            Question = reader.GetString(reader.GetOrdinal("Question")),
                            Answer = reader.GetString(reader.GetOrdinal("Answer")),
                            ModelAnswer = reader.GetString(reader.GetOrdinal("ModelAnswer")),
                            AIResponse = reader.GetString(reader.GetOrdinal("AIResponse")),
                            Score = reader.GetDecimal(reader.GetOrdinal("Score")),
                            Comment = reader.GetString(reader.GetOrdinal("Comment")),
                            IsVerify = reader.GetBoolean(reader.GetOrdinal("IsVerify")),
                            ActionStatus = reader.GetString(reader.GetOrdinal("ActionStatus")),
                            PushedToAccelerate = reader.GetBoolean(reader.GetOrdinal("PushedToAccelerate")),
                            UpdateComment = reader.GetString(reader.GetOrdinal("UpdateComment")),
                            StudentId = reader.GetInt32(reader.GetOrdinal("StudentId")),
                            StudentName = reader.GetString(reader.GetOrdinal("StudentName")),
                            CreatedOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn")),
                            IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"))
                        };

                        audits.Add(audit);
                    }
                }
            }

            return audits;
        }

        public async Task<bool> UpdateAssessmentResultAudit(int id, string actionStatus, string updateComment)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("Update_AssessmentResultAudit", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@ActionStatus", (object)actionStatus ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UpdateComment", (object)updateComment ?? DBNull.Value);

                conn.Open();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
        }

        public async Task<int> InsertAssessmentResultAudit(AssessmentResultAudit model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("Insert_AssessmentResultAudit", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AssessmentId", model.AssessmentId);
                cmd.Parameters.AddWithValue("@AttemptId", model.AttemptId);
                cmd.Parameters.AddWithValue("@QuestionId", model.QuestionId);
                cmd.Parameters.AddWithValue("@Question", (object)model.Question ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Answer", (object)model.Answer ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ModelAnswer", (object)model.ModelAnswer ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@AIResponse", (object)model.AIResponse ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Score", model.Score);
                cmd.Parameters.AddWithValue("@Comment", (object)model.Comment ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IsVerify", model.IsVerify);
                cmd.Parameters.AddWithValue("@ActionStatus", (object)model.ActionStatus ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PushedToAccelerate", model.PushedToAccelerate);
                cmd.Parameters.AddWithValue("@UpdateComment", (object)model.UpdateComment ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@StudentId", model.StudentId);
                cmd.Parameters.AddWithValue("@StudentName", (object)model.StudentName ?? DBNull.Value);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected;
            }
        }

        public async Task<bool> UpdateIAResponseScoreAndComment(int id, decimal score, string comment)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("Update_AuditIAReponse", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@Score", score);
                cmd.Parameters.AddWithValue("@Comment", comment);

                conn.Open();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
        }

    }
}
