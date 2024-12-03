using MediatR;
using StajTakipSistemi.Database;
using StajTakipSistemi.Models;
using StajTakipSistemi.Repositories.InternshipFormRepository;

namespace StajTakipSistemi.Business.InternshipFormBusiness;

public record CreateInternshipCommand(InternshipForm InternshipForm) : IRequest<InternshipForm>;

public class CreateInternshipCommandHandler : IRequestHandler<CreateInternshipCommand, InternshipForm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInternshipFormRepository _internshipFormRepository;

    public CreateInternshipCommandHandler(
        IUnitOfWork unitOfWork, IInternshipFormRepository internshipFormRepository)
    {
        _unitOfWork = unitOfWork;
        _internshipFormRepository = internshipFormRepository;
    }

    public async Task<InternshipForm> Handle(CreateInternshipCommand request, CancellationToken cancellationToken)
    {
        request.InternshipForm.IsApproved = false;
        request.InternshipForm.IsSucessfullyFinished = false;
        
        var internshipForm = await _internshipFormRepository.AddAsync(request.InternshipForm);
        await _unitOfWork.CommitChangesAsync();
        return internshipForm;
    }
}