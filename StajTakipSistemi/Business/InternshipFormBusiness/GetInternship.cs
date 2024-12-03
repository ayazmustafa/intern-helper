using MediatR;
using StajTakipSistemi.Database;
using StajTakipSistemi.Models;
using StajTakipSistemi.Repositories.InternshipFormRepository;

namespace StajTakipSistemi.Business.InternshipFormBusiness;

public record GetInternshipFormByIdQuery(Guid Id) : IRequest<InternshipForm>;

public class GetInternshipFormByIdQueryHandler : IRequestHandler<GetInternshipFormByIdQuery, InternshipForm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInternshipFormRepository _internshipFormRepository;

    public GetInternshipFormByIdQueryHandler(
        IUnitOfWork unitOfWork, IInternshipFormRepository internshipFormRepository)
    {
        _unitOfWork = unitOfWork;
        _internshipFormRepository = internshipFormRepository;
    }


    public async Task<InternshipForm> Handle(GetInternshipFormByIdQuery request, CancellationToken cancellationToken)
    {
        var internshipForm = await _internshipFormRepository.GetByIdAsync(request.Id);
        if (internshipForm is null)
        {
            throw new Exception($"Internship with id {request.Id} not found");
        }
        return internshipForm;
    }
}