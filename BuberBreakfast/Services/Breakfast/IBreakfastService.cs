namespace BuberBreakfast.Services.Breakfast;
using BuberBreakfast.Models;
using ErrorOr;

public interface IBreakfastService
{
    ErrorOr<Created> CreateBreakfast(Breakfast request);
    ErrorOr<Deleted> DeleteBreakfast(Guid id);
    ErrorOr<Breakfast> GetBreakfast(Guid id);
    ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfast breakfast);
}