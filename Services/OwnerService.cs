﻿using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services;

public class OwnerService : IOwnerService
{
    private readonly IOwnerRepository _ownerRepository;

    public OwnerService(IOwnerRepository ownerRepository)
    {
        _ownerRepository = ownerRepository;
    }

    public async Task<List<OwnerResponse>> GetOwnersAsync(string? name)
    {
        try
        {

            var query = _ownerRepository.GetOwnerQuery();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(x => x.FullName.Contains(name));


            
            
            

            var data = await query.Select(x => new OwnerResponse
            {
                RealEstateOwnerId = x.RealEstateOwnerId,
                FullName = x.FullName,
                CitizenId = x.CitizenId,
                ContactInformation = x.ContactInformation,
            }).ToListAsync();


            return data;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<ResultResponse<AddOwnerResponseDto>> AddOwnerAsync(AddOwnerRequestDto request)
    {
        try
        {
            var owner = await _ownerRepository.GetOwnerAsync(x =>
                x.CitizenId == request.CitizenId);
            if (owner != null)
            {
                return ErrorResponse.CreateErrorResponse<AddOwnerResponseDto>(message: "Owner already exists",
                    status: Status.Duplicate);
            }


            var toBeAdded = new RealEstateOwner
            {
                FullName = request.FullName,
                ContactInformation = request.ContactInformation,
                CitizenId = request.CitizenId
            };


            var addedOwner = await _ownerRepository.AddOwnerAsync(toBeAdded);


            var data = new AddOwnerResponseDto()
            {
                RealEstateOwnerId = addedOwner.RealEstateOwnerId,
                FullName = addedOwner.FullName,
                ContactInformation = addedOwner.ContactInformation,
                CitizenId = addedOwner.CitizenId
            };

            var successResponse = new ResultResponse<AddOwnerResponseDto>
            {
                IsSuccess = true,
                Messages = new[] { "Owner added successfully" },
                Status = Status.Ok,
                Data = data
            };
            return successResponse;
        }
        catch (Exception e)
        {
            return ErrorResponse.CreateErrorResponse<AddOwnerResponseDto>(e);
        }
    }

    public async Task<ResultResponse<OwnerUpdateResponseDto>> UpdateOwnerAsync(Guid id, OwnerUpdateRequestDto request)
    {
        try
        {
            var toBeUpdated = await _ownerRepository.GetOwnerAsync(x => x.RealEstateOwnerId == id);

            if (toBeUpdated is null)
            {
                return ErrorResponse.CreateErrorResponse<OwnerUpdateResponseDto>(message: "Owner not found",
                    status: Status.NotFound);
            }


            toBeUpdated.FullName = request.FullName ?? toBeUpdated.FullName;
            toBeUpdated.ContactInformation = request.ContactInformation ?? toBeUpdated.ContactInformation;
            toBeUpdated.CitizenId = request.CitizenId ?? toBeUpdated.CitizenId;


            await _ownerRepository.UpdateOwnerAsync(toBeUpdated);

            var data = new OwnerUpdateResponseDto
            {
                RealEstateOwnerId = toBeUpdated.RealEstateOwnerId,
                FullName = toBeUpdated.FullName,
                ContactInformation = toBeUpdated.ContactInformation,
                CitizenId = toBeUpdated.CitizenId
            };

            var successResult = new ResultResponse<OwnerUpdateResponseDto>
            {
                IsSuccess = true,
                Messages = new[] { "Owner updated successfully" },
                Status = Status.Ok,
                Data = data
            };
            return successResult;
        }
        catch (Exception e)
        {
            return ErrorResponse.CreateErrorResponse<OwnerUpdateResponseDto>(e);
        }
    }

    public async Task<ResultResponse<OwnerResponse>> GetOwnerAsync(Guid id)
    {
        try
        {
            var owner = await _ownerRepository.GetOwnerAsync(x => x.RealEstateOwnerId == id);

            if (owner == null)
            {
                return ErrorResponse.CreateErrorResponse<OwnerResponse>(status: Status.NotFound,
                    message: "Owner not found");
            }

            var data = new OwnerResponse
            {
                RealEstateOwnerId = owner.RealEstateOwnerId,
                FullName = owner.FullName,
                CitizenId = owner.CitizenId,
                ContactInformation = owner.ContactInformation,
            };


            var successResult = new ResultResponse<OwnerResponse>
            {
                IsSuccess = true,
                Messages = new[] { "Owner found successfully" },
                Status = Status.Ok,
                Data = data
            };
            return successResult;
        }
        catch (Exception e)
        {
            return ErrorResponse.CreateErrorResponse<OwnerResponse>(e);
        }
    }

    public async Task<ResultResponse<OwnerDeleteResponse>> DeleteOwnerAsync(Guid id)
    {
        try
        {
            var toBeDeleted = await _ownerRepository.GetOwnerAsync(x => x.RealEstateOwnerId == id);

            if (toBeDeleted is null)
            {
                return ErrorResponse.CreateErrorResponse<OwnerDeleteResponse>(status: Status.NotFound,
                    message: "Owner not found");
            }

            await _ownerRepository.DeleteOwnerAsync(toBeDeleted);
            return new ResultResponse<OwnerDeleteResponse>()
            {
                Status = Status.Ok,
                Messages = new[] { "Delete successfully" },
                IsSuccess = true,
                Data = new OwnerDeleteResponse
                {
                    RealEstateOwnerId = toBeDeleted.RealEstateOwnerId,
                    FullName = toBeDeleted.FullName,
                    ContactInformation = toBeDeleted.ContactInformation,
                    CitizenId = toBeDeleted.CitizenId,
                }
            };
        }
        catch (Exception e)
        {
            return ErrorResponse.CreateErrorResponse<OwnerDeleteResponse>(e);
        }
    }

    public List<RealEstateOwner> GetAllOwners()
    {
        var list = _ownerRepository.GetOwnerQuery().Include(x => x.Auctions).Select(x => new RealEstateOwner
        {
            RealEstateOwnerId = x.RealEstateOwnerId,
            FullName = x.FullName,
            ContactInformation = x.ContactInformation,
            CitizenId = x.CitizenId,
            Auctions = x.Auctions
        }).ToList();
        return list;
    }
}