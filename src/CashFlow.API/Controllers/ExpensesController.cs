using CashFlow.Application.UseCases.Expenses.Create;
using CashFlow.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Create([FromBody] RequestExpenseJson request)
    {
        var useCase = new CreateExpenseUseCase();

        var response = useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
