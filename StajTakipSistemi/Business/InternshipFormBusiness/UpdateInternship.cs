using MediatR;
using StajTakipSistemi.Database;
using StajTakipSistemi.Repositories;
using StajTakipSistemi.Repositories.InternshipFormRepository;

namespace StajTakipSistemi.Business.Authentication;

public record UpdateInternshipFormCommand(
    Guid Id, 
    string Company, 
    string Description 
) : IRequest;

public class UpdateInternshipFormCommandHandler : IRequestHandler<UpdateInternshipFormCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInternshipFormRepository _internshipFormRepository;

    public UpdateInternshipFormCommandHandler(
        IUnitOfWork unitOfWork, 
        IInternshipFormRepository internshipFormRepository)
    {
        _unitOfWork = unitOfWork;
        _internshipFormRepository = internshipFormRepository;
    }

    public async Task Handle(UpdateInternshipFormCommand request, CancellationToken cancellationToken)
    {
        var internshipForm = await _internshipFormRepository.GetByIdAsync(request.Id);

        if (internshipForm == null)
        {
            throw new Exception($"Internship Form with id of {request.Id} not found");
        }
        // update when more things to update
        internshipForm.Company = request.Company;
        internshipForm.Description = request.Description;

        await _internshipFormRepository.UpdateAsync(internshipForm);
        await _unitOfWork.CommitChangesAsync();
    }
}