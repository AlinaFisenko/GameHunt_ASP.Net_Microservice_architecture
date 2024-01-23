using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubscriptionAPI.Data;
using SubscriptionAPI.Models;
using SubscriptionAPI.Models.Dto;
using System.Linq;

namespace SubscriptionAPI.Controllers
{
    [ApiController]
    [Route("api/subscription")]
    public class SubscriptionAPIController : Controller
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mappper;
        public SubscriptionAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mappper = mapper;
        }

        [HttpGet]
		//[Authorize(Roles = "ADMIN")]
		public ResponseDto Get()
        {
            try
            {
                IEnumerable<Subscription> subscriptions = _db.Subscriptions.ToList();
                _response.Result = _mappper.Map<IEnumerable<SubscriptionDto>>(subscriptions);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
		[Authorize(Roles = "ADMIN")]
		public ResponseDto Get(int id)
        {
            try
            {
                Subscription subscription = _db.Subscriptions.First(l => l.subscriptionId == id);
                _response.Result = _mappper.Map<SubscriptionDto>(subscription);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("GetByTitle/{title}")]
        public ResponseDto Get(string title)
        {
            try
            {
                Subscription subscription = _db.Subscriptions.First(l => l.subscriptionTitle.ToLower() == title.ToLower());
                _response.Result = _mappper.Map<SubscriptionDto>(subscription);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post([FromBody] SubscriptionDto subscriptionDto)
        {
            try
            {
                Subscription subscription = _mappper.Map<Subscription>(subscriptionDto);
               _db.Subscriptions.Add(subscription);
                _db.SaveChanges();

                _response.Result = _mappper.Map<SubscriptionDto>(subscription);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromBody] SubscriptionDto subscriptionDto)
        {
            try
            {
                Subscription subscription = _mappper.Map<Subscription>(subscriptionDto);
                _db.Subscriptions.Update(subscription);
                _db.SaveChanges();

                _response.Result = _mappper.Map<SubscriptionDto>(subscription);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Subscription subscription = _db.Subscriptions.First(l => l.subscriptionId == id);
                _db.Subscriptions.Remove(subscription);
                _db.SaveChanges();
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }



        [HttpPost]
        [Route("payment/create")]
        public ResponseDto CreatePaymentHistory(Payment_HistoryDto paymentdto)
        {
            try
            {
                Payment_History payment = _mappper.Map<Payment_History>(paymentdto);
                payment.date = DateTime.Now;
                //payment.id_user = id_user;
                //payment.id_subscription = id_subscription;
                payment.price = _db.Subscriptions.FirstOrDefault(s => s.subscriptionId == payment.id_subscription).subscriptionPrice;

                _db.Payment_Historys.Add(payment);
                _db.SaveChanges();

                _response.Result = _mappper.Map<Payment_HistoryDto>(payment);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPut]
        [Route("userSubscription")]
        public ResponseDto UpdateUserSubscription(User_SubscriptionDto usersubdto)
        {
            try
            {
                User_Subscription usersub = _mappper.Map<User_Subscription>(usersubdto);
                //usersub.id_subscription = id_subscription;
                //usersub.id_user = id_user;
                usersub.end_date = DateTime.Now.AddDays(_db.Subscriptions.FirstOrDefault(s => s.subscriptionId == usersub.id_subscription).subscriptionDays);

                if (_db.User_Subscriptions.Any(s => s.id_user.Equals(usersub.id_user)))
                {
                    var line = _db.User_Subscriptions.FirstOrDefault(s => s.id_user.Equals(usersub.id_user));
                    line.id_subscription = usersub.id_subscription;
                    line.end_date = usersub.end_date;
                    _db.User_Subscriptions.Update(line);
                }
                else
                    _db.User_Subscriptions.Add(usersub);

                _db.SaveChanges();
                _response.Result = _mappper.Map<User_Subscription>(usersub);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("userSubscription/GetUserSubscriptionByUserId/{userId}")]
        public ResponseDto GetUserSubscriptionByUserId(string userId)
        {
            try
            {
                User_Subscription user_Subscription = _db.User_Subscriptions.FirstOrDefault(l => l.id_user == userId);
                _response.Result = _mappper.Map<User_Subscription>(user_Subscription);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("payment/GetPaymentByUserId/{userId}")]
        public ResponseDto GetPaymentByUserId(string userId)
        {
            try
            {
                List<Payment_History> payment_History = _db.Payment_Historys.Where(s=>s.id_user== userId).ToList();
                _response.Result = _mappper.Map<List<Payment_History>>(payment_History);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("userSubscription/SubscribtionCheck")]
        public ResponseDto SubscribtionCheck(string userId)
        {
            try
            {
                User_Subscription user_Subscription = _db.User_Subscriptions.FirstOrDefault(l => l.id_user == userId);
                bool result;
                if(user_Subscription==null)
                    result = false;
                else
                    result = user_Subscription.end_date > DateTime.Now;

                _response.Result = _mappper.Map<bool>(result);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }




    }
}
