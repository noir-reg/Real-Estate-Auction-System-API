using System.Net;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
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
    public async Task<ActionResult<ListResponseBaseDto<MemberListResponseDto>>> GetAll([FromQuery] MemberQuery request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _memberService.GetMembersAsync(request);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> UpdateMember([FromBody] UpdateMemberRequestDto request, [FromRoute] Guid id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _memberService.UpdateMemberAsync(id, request);
        return Ok("Update successfully");
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> DeleteMember([FromRoute] Guid id)
    {
        await _memberService.DeleteMemberAsync(id);
        return Ok("Delete Successfully");
    }
}