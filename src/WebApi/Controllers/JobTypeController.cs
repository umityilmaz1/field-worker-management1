using CleanArchitecture.Application.JobType.Commands.AddJobType;
using CleanArchitecture.Application.JobType.Commands.RemoveJobType;
using CleanArchitecture.Application.JobType.Commands.UpdateJobType;
using CleanArchitecture.Application.JobType.Queries.GetJobTypeById;
using CleanArchitecture.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Model.Commons;

namespace WebApi.Controllers;
public class JobTypeController : ApiControllerBase
{
    [HttpPost]
    [Route("[action]")]
    public async Task<ReturnData<bool>> AddJobType(AddJobTypeCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ReturnData<bool>> UpdateJobType(UpdateJobTypeCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ReturnData<bool>> RemoveJobType(RemoveJobTypeCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet]
    [Route("[action]/{Id}")]
    public async Task<ReturnData<GetJobTypeByIdQueryResponseDTO>> GetJobTypeById([FromRoute]GetJobTypeByIdQuery query)
    {
        return await Mediator.Send(query);
    }
}
