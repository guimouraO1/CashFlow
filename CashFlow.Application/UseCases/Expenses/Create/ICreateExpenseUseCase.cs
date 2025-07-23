using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Create;

public interface ICreateExpenseUseCase
{
    ResponseCreateExpenseJson Execute(RequestExpenseJson request);
}
