using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Services;

public class RealEstateService : IRealEstateService
{
    private readonly IRealEstateRepository _realEstateRepository;

    public RealEstateService(IRealEstateRepository realEstateRepository)
    {
        _realEstateRepository = realEstateRepository;
    }


    public async Task<ResultResponse<CreateRealEstateResponseDto>> CreateRealEstate(CreateRealEstateRequestDto request)
    {
        try
        {
            var toBeAdded = new RealEstate()
            {
                RealEstateName = request.RealEstateName,
                Address = request.Address,
                Status = "Active",
                ImageUrl = request.ImageUrl,
                Description = request.Description,
            };

            RealEstate result = await _realEstateRepository.AddRealEstateAsync(toBeAdded);

            var data = new CreateRealEstateResponseDto
            {
                RealEstateId = result.RealEstateId,
                RealEstateName = result.RealEstateName,
                Address = result.Address,
                Status = result.Status,
                ImageUrl = result.ImageUrl,
                Description = result.Description,
                OwnerId = result.OwnerId,
            };

            return new ResultResponse<CreateRealEstateResponseDto>
            {
                IsSuccess = true,
                Data = data,
                Messages = new[] { "Created successfully" }, Status = Status.Ok
            };
        }
        catch (Exception e)
        {
            return new ResultResponse<CreateRealEstateResponseDto>()
            {
                IsSuccess = false,
                Messages = new[] { e.Message }, Status = Status.Error
            };
        }
    }

    public async Task<ListResponseBaseDto<GetRealEstatesResponseDto>> GetRealEstates(RealEstateQuery request)
    {
        try
        {
            var query = _realEstateRepository.GetRealEstatesQuery();

            if (request.Search != null)
                query = query.Where(x => x.RealEstateName.Contains(request.Search.RealEstateName));

            query = query.Skip(request.Offset).Take(request.PageSize);

            var data =
                await query.Select(x => new GetRealEstatesResponseDto
                {
                    RealEstateId = x.RealEstateId,
                    RealEstateName = x.RealEstateName,
                    Address = x.Address,
                    Status = x.Status,
                    ImageUrl = x.ImageUrl,
                    Description = x.Description,
                    OwnerId = x.OwnerId
                }).AsNoTracking().ToListAsync();


            return new ListResponseBaseDto<GetRealEstatesResponseDto>
            {
                Data = data,
                Total = await _realEstateRepository.GetCount(request.Search),
                PageSize = request.PageSize,
                Page = request.Page
            };
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<ResultResponse<UpdateRealEstateResponseDto>> UpdateRealEstate(Guid realEstateId,
        UpdateRealEstateRequestDto request)
    {
        try
        {
            var toBeUpdated = await _realEstateRepository.GetRealEstate(x => x.RealEstateId == realEstateId);

            if (toBeUpdated == null)
            {
                return new ResultResponse<UpdateRealEstateResponseDto>()
                {
                    IsSuccess = false,
                    Messages = new[] { "RealEstate not found" },
                    Status = Status.NotFound
                };
            }

            toBeUpdated.RealEstateName = !string.IsNullOrEmpty(request.RealEstateName)
                ? request.RealEstateName
                : toBeUpdated.RealEstateName;
            toBeUpdated.Address = !string.IsNullOrEmpty(request.Address) ? request.Address : toBeUpdated.Address;
            toBeUpdated.Status = !string.IsNullOrEmpty(request.Status) ? request.Status : toBeUpdated.Status;
            toBeUpdated.ImageUrl = !string.IsNullOrEmpty(request.ImageUrl) ? request.ImageUrl : toBeUpdated.ImageUrl;
            toBeUpdated.Description = !string.IsNullOrEmpty(request.Description)
                ? request.Description
                : toBeUpdated.Description;
            toBeUpdated.OwnerId = request.OwnerId != Guid.Empty ? request.OwnerId : toBeUpdated.OwnerId;

            await _realEstateRepository.UpdateRealEstateAsync(toBeUpdated);

            var data = new UpdateRealEstateResponseDto
            {
                RealEstateId = toBeUpdated.RealEstateId,
                RealEstateName = toBeUpdated.RealEstateName,
                Address = toBeUpdated.Address,
                Status = toBeUpdated.Status,
                ImageUrl = toBeUpdated.ImageUrl,
                Description = toBeUpdated.Description,
                OwnerId = toBeUpdated.OwnerId
            };

            return new ResultResponse<UpdateRealEstateResponseDto>()
            {
                IsSuccess = true,
                Data = data,
                Status = Status.Ok,
                Messages = new[] { "Updated successfully" }
            };
        }
        catch (Exception e)
        {
            return new ResultResponse<UpdateRealEstateResponseDto>()
            {
                IsSuccess = false,
                Messages = new[] { e.Message }, Status = Status.Error
            };
        }
    }

    public async Task<ResultResponse<DeleteRealEstateResponseDto>> DeleteRealEstate(Guid id)
    {
        try
        {
            var toBeDeleted = await _realEstateRepository.GetRealEstate(x => x.RealEstateId == id);
            
            if (toBeDeleted == null)
            {
                return new ResultResponse<DeleteRealEstateResponseDto>()
                {
                    IsSuccess = false,
                    Messages = new[] { "RealEstate not found" },
                    Status = Status.NotFound
                };
            }
            
            await _realEstateRepository.DeleteRealEstateAsync(toBeDeleted);
            
            return new ResultResponse<DeleteRealEstateResponseDto>()
            {
                IsSuccess = true,
                Messages = new[] { "Deleted successfully" },
                Status = Status.Ok,
                Data = new DeleteRealEstateResponseDto
                {
                    RealEstateId = toBeDeleted.RealEstateId,
                    RealEstateName = toBeDeleted.RealEstateName,
                    Address = toBeDeleted.Address,
                    Status = toBeDeleted.Status,
                    ImageUrl = toBeDeleted.ImageUrl,
                    Description = toBeDeleted.Description,
                    OwnerId = toBeDeleted.OwnerId
                }
            };
        }
        catch (Exception e)
        {
            return new ResultResponse<DeleteRealEstateResponseDto>()
            {
                IsSuccess = false,
                Messages = new[] { e.Message }, Status = Status.Error
            };
        }
    }
}