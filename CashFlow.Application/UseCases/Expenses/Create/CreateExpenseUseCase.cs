using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Infrastructure.DataAccess;

namespace CashFlow.Application.UseCases.Expenses.Create;

public class CreateExpenseUseCase
{
    public ResponseCreateExpenseJson Execute(RequestExpenseJson request)
    {
        Validate(request);

        var dbContext = new CashFlowDbContext();

        var entity = new Expense
        {
            Title = request.Title,
            Description = request.Description,
            Date = request.Date,
            Amount = request.Amount,
            PaymentType = (PaymentType)request.PaymentType
        };

        dbContext.Expenses.Add(entity);

        dbContext.SaveChanges();

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
