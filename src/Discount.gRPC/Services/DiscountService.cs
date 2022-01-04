using AutoMapper;
using Discount.gRPC.Entities;
using Discount.gRPC.Repositories;
using Grpc.Core;

namespace Discount.gRPC.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IDiscountRepository discountRepository, IMapper mapper, ILogger<DiscountService> logger)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);

            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound,
                    $"DISCOUNT_WITH_PRODUCT_NAME_NOT_FOUND: { request.ProductName }"));
            }

            _logger.LogInformation($"DISCOUNT_RETRIVED_FOR_PRODUCT_NAME: {coupon.ProductName}, AMOUNT: {coupon.Amount}");
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            await _discountRepository.CreateDiscount(coupon);

            _logger.LogInformation($"DISCOUNT_CREATED_FOR_PRODUCT_NAME: {coupon.ProductName}, AMOUNT: {coupon.Amount}");

            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            await _discountRepository.UpdateDiscount(coupon);

            _logger.LogInformation($"DISCOUNT_UPDATED_FOR_PRODUCT_NAME: {coupon.ProductName}, AMOUNT: {coupon.Amount}");

            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _discountRepository.DeleteDiscount(request.ProductName);

            var response = new DeleteDiscountResponse
            {
                Success = deleted
            };

            _logger.LogInformation($"DISCOUNT_DELETED_FOR_PRODUCT_NAME: { request.ProductName }, {response}");
            return response;
        }
    }
}
