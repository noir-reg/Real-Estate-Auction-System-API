using System.Net;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Real_Estate_Auction_System_API.Controllers;

[ApiController]
[Route("api/members")]
public class MemberController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IMemberService _memberService;

    public MemberController(IMemberService memberService, IConfiguration configuration)
    {
        _memberService = memberService;
        _configuration = configuration;
    }

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<ActionResult<ListResponseBaseDto<MemberListResponseDto>>> GetAll([FromQuery] MemberQuery request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _memberService.GetMembersAsync(request);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ResultResponse<UpdateMemberResponseDto>>> UpdateMember(
        [FromBody] UpdateMemberRequestDto request, [FromRoute] Guid id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _memberService.UpdateMemberAsync(id, request);

        if (result.Status == Status.NotFound) return NotFound(result);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ResultResponse<DeleteMemberResponseDto>>> DeleteMember([FromRoute] Guid id)
    {
        var result = await _memberService.DeleteMemberAsync(id);


        if (result.Status == Status.NotFound) return NotFound(result);

        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [Authorize]
    public async Task<ActionResult<ResultResponse<MemberDetailResponseDto>>> GetMember([FromRoute] Guid id)
    {
        var result = await _memberService.GetMemberAsync(id);

        if (result.Status == Status.NotFound) return NotFound(result);

        return Ok(result);
    }
}