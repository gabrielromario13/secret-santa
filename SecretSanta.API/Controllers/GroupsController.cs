using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SecretSanta.API.Domain.Requests;
using SecretSanta.API.Services.Interfaces;

namespace SecretSanta.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupsController(IGroupService service, IParticipantService participantService, IMatchService matchService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateGroup(GroupRequest request)
    {
        var result = await service.CreateGroupAsync(request);

        return result is null
            ? BadRequest()
            : Ok(result);
    }
    
    [HttpPost("{groupId:int}/participants")]
    public async Task<IActionResult> CreateParticipant(int groupId, ParticipantRequest request)
    {
        if (!request.ConfirmPassword.Equals(request.Password))
            return BadRequest("As senhas não coincidem.");
        
        var result = await participantService.CreateParticipantAsync(groupId, request);
        
        return result is null
            ? NotFound()
            : Ok(result);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await service.GetById(id);
        
        return result is null
            ? NotFound()
            : Ok(result);
    }
    
    [HttpPost("{groupId:int}/matches")]
    public async Task<IActionResult> GenerateMatchesByGroupIdAsync(int groupId)
    {
        var result = await matchService.GenerateMatchesByGroupIdAsync(groupId);
        
        return result.IsNullOrEmpty()
            ? BadRequest(result)
            : Ok(result);
    }
    
    [HttpGet("{groupId:int}/participants/{giverId:int}/receiver")]
    public async Task<IActionResult> GetMatchAsync(int groupId, int giverId)
    {
        var result = await matchService.GetMatchAsync(giverId);
        
        return result is null
            ? NotFound()
            : Ok(result);
    }
}