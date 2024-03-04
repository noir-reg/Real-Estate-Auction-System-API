using Repositories;

namespace Services;

public class RealEstateService : IRealEstateService
{
    private readonly IRealEstateRepository _realEstateRepository;

    public RealEstateService(IRealEstateRepository realEstateRepository)
    {
        _realEstateRepository = realEstateRepository;
    }
    
    
}