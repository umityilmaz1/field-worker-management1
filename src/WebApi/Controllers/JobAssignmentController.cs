using CleanArchitecture.Application.Fields.Commands.RemoveField;
using CleanArchitecture.Application.Fields.Commands.UpdateField;
using CleanArchitecture.Application.Fields.Queries.GetFieldById;
using CleanArchitecture.Application.Fields.Queries.GetFields;
using CleanArchitecture.Application.JobAssignments.Commands.Assign;
using CleanArchitecture.Application.JobAssignments.Commands.DeleteAssignment;
using CleanArchitecture.Application.JobAssignments.Commands.UpdateAssignment;
using CleanArchitecture.Application.JobAssignments.Queries.GetAssignmentById;
using CleanArchitecture.Application.JobAssignments.Queries.GetAssignmentsByAccountId;
using CleanArchitecture.Application.JobAssignments.Queries.GetAssignmentsForList;
using CleanArchitecture.Model.Commons;
using CleanArchitecture.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
public class JobAssignmentController : ApiControllerBase
{
    [HttpPost]
    [Route("[action]")]
    public async Task<ReturnData<bool>> Assign(AssignCommand command)
    {
        await Mediator.Send(command);
        return ReturnData<bool>.Success();
    }

    [HttpPut]
    [Route("[action]")]
    public async Task<ReturnData<bool>> UpdateAssignment(UpdateAssignmentCommand command)
    {
        await Mediator.Send(command);
        return ReturnData<bool>.Success();
    }

    [HttpPut]
    [Route("[action]/{id}")]
    public async Task<ReturnData<bool>> DeleteAssignment([FromRoute] DeleteAssignmentCommand command)
    {
        await Mediator.Send(command);
        return ReturnData<bool>.Success();
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<ReturnData<GetAssignmentByIdResponseDto>> GetAssignmentById([FromRoute] GetAssignmentByIdQuery query)
    {        
        return ReturnData<GetAssignmentByIdResponseDto>.Success(Mediator.Send(query).Result.Data);
    }

    [HttpGet]
    [Route("[action]/{accountId}")]
    public async Task<ReturnData<List<GetAssignmentsByAccountIdResponseDto>>> GetAssignmentsByAccountId([FromRoute] GetAssignmentsByAccountIdQuery query)
    {
        return ReturnData<List<GetAssignmentsByAccountIdResponseDto>>.Success(Mediator.Send(query).Result.Data);
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<ReturnData<List<GetAssignmentsForListResponseDto>>> GetAssignmentsForList()
    {
        return ReturnData<List<GetAssignmentsForListResponseDto>>.Success(Mediator.Send(new GetAssignmentsForListQuery()).Result.Data);
    }
}
