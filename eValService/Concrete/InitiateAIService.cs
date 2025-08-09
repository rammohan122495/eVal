using eValDTO.DTOs;
using eValService.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eValService.Concrete
{
    public class InitiateAIService : IInitiateAIService
    {

        public async Task<string> InitiateAI(InitiateAIRequest objInitiateAIRequest)
        {
            return "Initiated Sucessfully";
        }
    }
}
