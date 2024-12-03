using MediatR;
using StajTakipSistemi.Database;
using StajTakipSistemi.Repositories.InternshipFormRepository;

namespace StajTakipSistemi.Business.InternshipFormBusiness;

public record DeleteInternshipCommand(Guid Id) : IRequest;

public class DeleteInternshipCommandHandler : IRequestHandler<DeleteInternshipCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInternshipFormRepository _internshipFormRepository;

    public DeleteInternshipCommandHandler(
        IUnitOfWork unitOfWork, IInternshipFormRepository internshipFormRepository)
    {
        _unitOfWork = unitOfWork;
        _internshipFormRepository = internshipFormRepository;
    }


    public async Task Handle(DeleteInternshipCommand request, CancellationToken cancellationToken)
    {
        await _internshipFormRepository.DeleteByIdAsync(request.Id);
        await _unitOfWork.CommitChangesAsync();
    }
}