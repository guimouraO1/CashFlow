using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Create
{
    public class CreateExpenseValidator : AbstractValidator<RequestExpenseJson>
    {
        public CreateExpenseValidator()
        {
            RuleFor(expense => expense.Title).NotEmpty().WithMessage("Title cannot be empty.");
            RuleFor(expense => expense.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero.");
            RuleFor(expense => expense.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Date cannot be in the future.");
            RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage("Invalid payment type.");
        }
    }
}
