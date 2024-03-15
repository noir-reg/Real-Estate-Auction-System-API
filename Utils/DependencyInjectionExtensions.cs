using Microsoft.Extensions.DependencyInjection;

using Repositories;
using Services;

namespace Utils;

public static class DependencyInjectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<IAuctionService, AuctionService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddTransient<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IStaffService, StaffService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddSingleton<IFirebaseStorageService, FirebaseStorageService>();
        services.AddScoped<ILegalDocumentService, LegalDocumentService>();
        services.AddScoped<IRealEstateService, RealEstateService>();
        services.AddScoped<IBidService, BidService>();
        services.AddScoped<IAuctionPostService, AuctionPostService>();
        services.AddScoped<IMailService, MailService>();
        services.AddScoped<IPaymentService, PaymentService>();

    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IAuctionRepository, AuctionRepository>();
        services.AddScoped<IAuctionRegistrationRepository, AuctionRegistrationRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IBidRepository, BidRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IRealEstateRepository, RealEstateRepository>();
        services.AddScoped<IRealEstateOwnerRepository, RealEstateOwnerRepository>();
        services.AddScoped<ILegalDocumentRepository, LegalDocumentRepository>();
        services.AddScoped<IStaffRepository, StaffRepository>();
        
    }
  
}