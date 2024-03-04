﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BusinessObjects.Dtos.Request;

public class BaseQueryDto
{
    [Required(ErrorMessage = "Page is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0")]
    public int Page { get; set; }

    [Required(ErrorMessage = "Page size is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Page size must be greater than 0")]
    public int PageSize { get; set; }

    [BindNever] public int Offset => (Page - 1) * PageSize;
}

public enum OrderDirection
{
    ASC,
    DESC
}

public class UserQuery : BaseQueryDto
{
    public UserSortBy SortBy { get; set; }
    public OrderDirection OrderDirection { get; set; }
    public SearchUserQuery? Search { get; set; }
}

public class AuctionQuery : BaseQueryDto
{
    public AuctionSortBy SortBy { get; set; }
    public OrderDirection OrderDirection { get; set; }
    public SearchAuctionQuery? Search { get; set; }
}

public class StaffQuery : BaseQueryDto
{
    public SearchStaffQuery? Search { get; set; }
    public StaffSortBy SortBy { get; set; }
    public OrderDirection OrderDirection { get; set; }
}

public class AdminQuery : BaseQueryDto
{
    public SearchAdminQuery? Search { get; set; }
    public AdminSortBy SortBy { get; set; }
    public OrderDirection OrderDirection { get; set; }
}

public enum AdminSortBy
{
    Username
}

public class SearchAdminQuery
{
    public string? Username { get; set; }
}

public enum StaffSortBy
{
    Username
}

public class MemberQuery : BaseQueryDto
{
    public SearchMemberQuery? Search { get; set; }
    public MemberSortBy SortBy { get; set; }
    public OrderDirection OrderDirection { get; set; }
}

public enum MemberSortBy
{
    Username
}

public class SearchMemberQuery
{
    public string? Username { get; set; }
}

public class SearchStaffQuery
{
    public string? Username { get; set; }
}

public class AuctionSortBy
{
}

public class SearchAuctionQuery
{
}

public enum UserSortBy
{
    Username
}

public enum UserRole
{
    Admin,
    Member,
    Staff
}

public class SearchUserQuery
{
    public string? Username { get; set; }
}

public class LegalDocumentQuery : BaseQueryDto
{
    public LegalDocumentSortBy SortBy { get; set; } = LegalDocumentSortBy.FileName;
    public string FileName { get; set; } = "";
    public OrderDirection OrderDirection { get; set; }
}

public enum LegalDocumentSortBy
{
    FileName
}