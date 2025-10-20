﻿using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_CmmoresWeb.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorsResponse(ActionContext context)
        {
            var errors = context.ModelState.Where(M => M.Value.Errors.Any())
                                                    .Select(M => new ValidationError()
                                                    {
                                                        Failed = M.Key,
                                                        Errors = M.Value.Errors.Select(E => E.ErrorMessage)
                                                    });
            var response = new ValidationErrorsToReturn()
            {
                ValidationErrors = errors
            };

            return new BadRequestObjectResult(response);
        }
    }
}
