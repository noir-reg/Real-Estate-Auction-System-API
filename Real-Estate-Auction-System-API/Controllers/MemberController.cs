﻿using System.Net;
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

  
}