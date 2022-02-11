﻿using Manager.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.API.Utilities
{
    public static class Responses
    {
        public static ResultViewModel ApplicationErroMessage()
        {
            return new ResultViewModel
            {
                Message = "Ocorreu algum erro interno na aplicação, por favor tente novamente.",
                Success = false,
                Data = null
            };
        }

        public static ResultViewModel DomainErrorMessage(string message)
        {
            return new ResultViewModel
            {
                Message = message,
                Success = true,
                Data = null
            };
        }

        public static ResultViewModel DomainErrorMessage(string message, IReadOnlyCollection<string> erros)
        {
            return new ResultViewModel
            {
                Message = message,
                Success = false,
                Data = erros
            };
        }

        public static ResultViewModel UnauthorizedErrorMessage()
        {
            return new ResultViewModel
            {
                Message = "A combinação de login e senha está incorreta!",
                Success = false,
                Data = null
            };
        }
    }
}
