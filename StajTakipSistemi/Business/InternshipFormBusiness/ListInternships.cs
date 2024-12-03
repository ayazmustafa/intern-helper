using MediatR;
using StajTakipSistemi.Database;
using StajTakipSistemi.Models;
using StajTakipSistemi.Repositories;
using StajTakipSistemi.Repositories.InternshipFormRepository;

namespace StajTakipSistemi.Business.Authentication;

public record ListInternshipFormQuery() : IRequest<List<InternshipForm>>;

public class ListInternshipFormQueryHandler : IRequestHandler<ListInternshipFormQuery, List<InternshipForm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInternshipFormRepository _internshipFormRepository;

    public ListInternshipFormQueryHandler(
        IUnitOfWork unitOfWork, IInternshipFormRepository internshipFormRepository)
    {
        _unitOfWork = unitOfWork;
        _internshipFormRepository = internshipFormRepository;
    }

    public async Task<List<InternshipForm>> Handle(ListInternshipFormQuery request, CancellationToken cancellationToken)
    {
        var internshipForms = await _internshipFormRepository.GetAllAsync();

        await _unitOfWork.CommitChangesAsync();

        return internshipForms;
    }
}