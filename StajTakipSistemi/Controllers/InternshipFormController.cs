using MediatR;
using Microsoft.AspNetCore.Mvc;
using StajTakipSistemi.Business.Authentication;
using StajTakipSistemi.Business.InternshipFormBusiness;
using StajTakipSistemi.Controllers.Base;
using StajTakipSistemi.Models;

namespace StajTakipSistemi.Controllers;

public class InternshipFormController : ApiController
{
    private readonly ISender _mediator;

    public InternshipFormController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("internships")]
    public async Task<ActionResult<IEnumerable<InternshipForm>>> GetInternshipForms()
    {
        var query = new ListInternshipFormQuery();
        var internshipForms = await _mediator.Send(query);
        return Ok(internshipForms);
    }

    [HttpGet("internships/{id}")]
    public async Task<ActionResult<InternshipForm>> GetInternshipForm([FromRoute] Guid id)
    {
        var query = new GetInternshipFormByIdQuery(id);
        var internshipForms = await _mediator.Send(query);
        return Ok(internshipForms);
    }

    // POST: api/InternshipForms
    [HttpPost("internships")]
    public async Task<ActionResult<InternshipForm>> PostInternshipForm([FromBody]InternshipForm request)
    {
        var command = new CreateInternshipCommand(request);
        var internshipForm = await _mediator.Send(command);
        return Ok(internshipForm);
    }

    [HttpPut("internships/{id}")]
    public async Task<IActionResult> PutInternshipForm(
        [FromRoute]Guid id, 
        [FromBody]InternshipForm request)
    {
        var command = new UpdateInternshipFormCommand(
            id,
            request.Company,
            request.Description
        );
        await _mediator.Send(command);
        return Ok();
    }

    [HttpDelete("internships/{id}")]
    public async Task<IActionResult> DeleteInternshipForm([FromRoute]Guid id)
    {
        var command = new DeleteInternshipCommand(id);
        await _mediator.Send(command);
        return Ok();
    }
    
    [HttpPut("approveInternshipForm/{id}")]
    public async Task<IActionResult> ApproveInternshipForm(
        [FromRoute]Guid id)
    {
        var command = new ApproveInternshipFormCommand(
            id
        );
        await _mediator.Send(command);
        return Ok();
    }
    
    [HttpPut("approveInternshipProgram/{id}")]
    public async Task<IActionResult> ApproveInternshipProgram(
        [FromRoute]Guid id)
    {
        var command = new ApproveInternshipProgramCommand(
            id
        );
        await _mediator.Send(command);
        return Ok();
    }
}