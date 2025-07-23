using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Create;

public class CreateExpenseUseCase : ICreateExpenseUseCase
{
    private readonly IExpensesRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateExpenseUseCase(IExpensesRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public ResponseCreateExpenseJson Execute(RequestExpenseJson request)
    {
        Validate(request);

        var entity = new Expense
        {
            Title = request.Title,
            Description = request.Description,
            Date = request.Date,
            Amount = request.Amount,
            PaymentType = (PaymentType)request.PaymentType
        };

        _repository.Add(entity);

        _unitOfWork.Commit();

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
