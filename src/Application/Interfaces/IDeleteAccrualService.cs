using Ardalis.Result;

namespace Domain.Interfaces;

public interface IDeleteAccrualService
{
    // Esse serviço e método existem para fornecer um local para disparar eventos de domínio
    // ao deletar a entidade raiz agregada.
    public Task<Result> DeleteAccrual(int accrualId);
}