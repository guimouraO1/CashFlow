using CashFlow.Application.UseCases.Expenses.Create;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Create([FromBody] RequestExpenseJson request)
    {
        try
        {             
            var useCase = new CreateExpenseUseCase();
            var response = useCase.Execute(request);
            return Created(string.Empty, response);
        }
        catch (ErrorOnValidationException ex)
        {
            var errorMessage = new ResponseErrorJson(ex.Errors);

            return BadRequest(errorMessage);
        }
        catch
        {
            var errorMessage = new ResponseErrorJson("Unexpected error occurred while processing your request.");

            return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
        }
    }
}
