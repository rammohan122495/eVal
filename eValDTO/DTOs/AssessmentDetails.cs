using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eValDTO.DTOs
{
    public class AssessmentDetails
    {
        public int Id { get; set; }
        public int AssessmentId { get; set; }
        public int AttemptId { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class ScoredItemDTO
    {
        public int ItemResponseId { get; set; }
        public int Score { get; set; }
    }

    public class ScoredItemRequest
    {
        public List<ScoredItemDTO> ScoredItemDTOs { get; set; }
    }

}
