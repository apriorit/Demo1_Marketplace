using Marketplace.Contracts.Orders;
using Marketplace.Contracts.Payments;
using Marketplace.Data.Infrastructure;
using Marketplace.Data.Models.Payments;
using Marketplace.Infrastructure.Exceptions;
using Marketplace.Models.Dtos.Payments;
using Marketplace.Models.Enum.Payment;
using Marketplace.Models.Models.Payments;
using System;
using System.Linq;
using System.Threading.Tasks;
using DAL = Marketplace.Data.Models.Payments;

namespace Marketplace.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentProvider _paymentProvider;
        private readonly IRepository<Payment> _paymentRepo;
        private readonly IOrderService _orderService;

        public PaymentService(IPaymentProvider paymentProvider,
            IOrderService orderService,
            IRepository<DAL.Payment> paymentRepo)
        {
            _paymentProvider = paymentProvider;
            _orderService = orderService;
            _paymentRepo = paymentRepo;
        }

        public async Task<PaymentStatus> GetPaymentStatusAsync(int orderId)
        {
            var payment = _paymentRepo.Get().Where(p => p.OrderId == orderId).FirstOrDefault();
            if (payment == null)
            {
                return null;
            }

            var paymentSystem = _paymentProvider.GetPaymentSystem((PaymentSystemType)payment.PaymentSystem);

            return await paymentSystem.GetPaymentStatusAsync(orderId);
        }

        public async Task<PaymentResult> RequestPaymentAsync(CreatePaymentDto createPaymentModel)
        {
            if (!await _orderService.IsOrderExistsAsync(createPaymentModel.OrderId))
            {
                return null;
            }

            var paymentSystem = _paymentProvider.GetPaymentSystem(createPaymentModel.PaymentSystemType);

            var paymentInfo = new PaymentInfoDto
            {
                Action = createPaymentModel.Action,
                Amount = await _orderService.GetAmountAsync(createPaymentModel.OrderId),
                Currency = createPaymentModel.Currency,
                Description = createPaymentModel.Description,
                OrderId = createPaymentModel.OrderId,
                Language = Language.UK
            };

            var paymentResult = paymentSystem.RequestPayment(paymentInfo);

            await SavePaymentAsync(paymentInfo, paymentResult.Signature, createPaymentModel.PaymentSystemType);

            return new PaymentResult
            {
                Url = paymentResult.Url,
                Data = paymentResult.Data,
                Signature = paymentResult.Signature,
                ClientSecret = paymentResult.ClientSecret
            };
        }

        public async Task<PaymentStatusType> ReceiveResponseAsync(string data, string signature)
        {
            var payment = await _paymentRepo.GetFirstOrDefaultAsync(p => p.Signature == signature);
            if (payment == null)
            {
                throw new NotFoundException("Received payment response is incorrect");
            }
            if ((PaymentSystemType)payment.PaymentSystem != PaymentSystemType.LiqPay)
            {
                throw new NotSupportedException("This method is supported by LiqPay only!");
            }

            var paymentSystem = _paymentProvider.GetPaymentSystem((PaymentSystemType)payment.PaymentSystem);

            var response = paymentSystem.DecodePaymentResponse(data);
            var status = response.ErrorCode == null
                ? PaymentStatusType.Completed
                : PaymentStatusType.Error;
            payment.Status = (int)status;

            _ = await _paymentRepo.UpdateAsync(p => p.Id.Equals(payment.Id), payment);
            return status;
        }

        private async Task SavePaymentAsync(PaymentInfoDto paymentInfo, string signarure, PaymentSystemType paymentSystemType)
        {
            await _paymentRepo.CreateAsync(new Payment
            {
                PaymentSystem = (int)paymentSystemType,
                PaymentTypeId = (int)paymentInfo.Action,
                Amount = paymentInfo.Amount,
                Status = (int)PaymentStatusType.WaitForResponse,
                OrderId = paymentInfo.OrderId,
                Signature = signarure
            });
        }
    }
}
