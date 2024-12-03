﻿using MediatR;
using StajTakipSistemi.Authentication;
using StajTakipSistemi.Database;
using StajTakipSistemi.Repositories.InternshipFormRepository;

namespace StajTakipSistemi.Business.InternshipFormBusiness;


[Authorize(Roles = "admin")]
public record ApproveInternshipProgramCommand(
    Guid Id
) : IRequest;

public class ApproveInternshipProgramCommandHandler : IRequestHandler<ApproveInternshipFormCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInternshipFormRepository _internshipFormRepository;

    public ApproveInternshipProgramCommandHandler(
        IUnitOfWork unitOfWork, 
        IInternshipFormRepository internshipFormRepository)
    {
        _unitOfWork = unitOfWork;
        _internshipFormRepository = internshipFormRepository;
    }

    public async Task Handle(ApproveInternshipFormCommand request, CancellationToken cancellationToken)
    {
        var internshipForm = await _internshipFormRepository.GetByIdAsync(request.Id);

        if (internshipForm == null)
        {
            throw new Exception($"Internship Form with id of {request.Id} not found");
        }
        
        internshipForm.IsSucessfullyFinished = true;

        await _internshipFormRepository.UpdateAsync(internshipForm);
        await _unitOfWork.CommitChangesAsync();
    }
}