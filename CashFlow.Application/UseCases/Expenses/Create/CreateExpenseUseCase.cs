using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Create;

public class CreateExpenseUseCase
{
    public ResponseCreateExpenseJson Execute(RequestExpenseJson request)
    {
        Validate(request);

        return new ResponseCreateExpenseJson();
    }

    private void Validate(RequestExpenseJson request)
    {
        var validator = new CreateExpenseValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
