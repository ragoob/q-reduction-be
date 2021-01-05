using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using QReduction.Api;
using QReduction.Api.Models;
using QReduction.Apis.Controllers;
using QReduction.Apis.Infrastructure;
using QReduction.Core.Domain;
using QReduction.Core.Domain.Acl;
using QReduction.Core.Repository.Custom;
using QReduction.Core.Service.Custom;
using QReduction.Core.Service.Generic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QReduction.QReduction.Infrastructure.DbMappings.Domain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("QReductionPolicy")]
    [ValidateModelFilter]
    public class ShiftQueueController : CustomBaseController
    {
        #region Fields
        //  private readonly IService<ShiftQueue> _shiftQueueService;
        private readonly IShiftQueueService _shiftQueueService;
        private readonly IService<ShiftUser> _shiftUserService;
        private readonly IService<Shift> _shiftService;
        private readonly IService<BranchService> _branchServiceService;
        private readonly IService<Evaluation> _evaluationService;
        private readonly IService<User> _UserService;
        private readonly IShiftRepository _shiftRepository;
        IConfiguration _configuration;

        #endregion

        #region ctor
        public ShiftQueueController(IShiftQueueService shiftQueueService, IService<ShiftUser> shiftUserService, IService<Shift> shiftService,
            IService<BranchService> branchServiceService, IService<Evaluation> evaluationService,
            IService<User> UserService,
            IShiftRepository shiftRepository, 
            IConfiguration configuration)
        {
            _shiftQueueService = shiftQueueService;
            _shiftUserService = shiftUserService;
            _shiftService = shiftService;
            _branchServiceService = branchServiceService;
            _evaluationService = evaluationService;
            _shiftRepository = shiftRepository;
            _configuration = configuration;
            _UserService = UserService;
        }
        #endregion

        #region New
        [HttpGet]
        [Route("SetMobileUserQueue")]
        //[CustomAuthorizationFilter("Shift.Add")]
        
        [ApiExplorerSettings(GroupName = "Mobile")]
        public async Task<IActionResult> SetMobileUserQueue(int branchServiceId, string currentTime)
        {

            if (branchServiceId == 0)
                return BadRequest(Messages.InvalidShiftId);

            // get branch Service
            BranchService branchService = (await _branchServiceService.FindAsync(item => item.Id == branchServiceId)).SingleOrDefault();
            if (branchService == null)
                return BadRequest(Messages.InvalidQueueId);

            // get oppening shift
            
            Shift OpenShift = _shiftRepository.GetBranchOpenShiftIds(branchService.BranchId, currentTime).Where(c => !c.IsEnded).FirstOrDefault();
            if (OpenShift == null)
                return BadRequest(Messages.NoShiftOpenInThisBranch);

            //get shift user
            List<ShiftUser> shiftUsers = (await _shiftUserService.FindAsync(a => a.ShiftId == OpenShift.Id && a.ServiceId == branchService.ServiceId)).ToList();
            if (shiftUsers?.Count <= 0)
                return BadRequest(Messages.NotAssignedUserInShift);

            //Queue Number
            var PreviousUserQueue = (await _shiftQueueService.FindAsync(a => a.ShiftId == OpenShift.Id && a.ServiceId == branchService.ServiceId
                                    )).OrderBy(a => a.UserTurn).LastOrDefault();

            var QueueNo = PreviousUserQueue == null ? 1 : PreviousUserQueue.UserTurn + 1;


            //Current Queue
            var CurrentServiedQueue = (await _shiftQueueService.FindAsync(a => a.ShiftId == OpenShift.Id && a.ServiceId == branchService.ServiceId
                                  && !a.IsServiceDone /*&& a.UserIdBy != null && a.WindowNumber != null*/ )).OrderBy(a => a.UserTurn).FirstOrDefault();

            var CurrentServiedQueueNo = CurrentServiedQueue == null ? QueueNo : CurrentServiedQueue.UserTurn;

            var stringPushId = SendMessage(new Record() { queueNumber = QueueNo, Counter = null, currentQueue = CurrentServiedQueueNo });

            try
            {
                _shiftQueueService.Add(new ShiftQueue
                {
                    IsServiceDone = false,
                    UserIdBy = null,
                    UserTurn = QueueNo,
                    //WindowNumber=null,
                    UserIdMobile = UserId,
                    ServiceId = branchService.ServiceId,
                    ShiftId = OpenShift.Id,
                    PushId = stringPushId
                        ,
                    CreatedDate = DateTime.Now
                });

            }
            catch (Exception ex)
            {

                throw;
            }



            return Ok(new
            {
                QueueNo,
                CurrentServiedQueueNo,
                WaitNo =  QueueNo - CurrentServiedQueueNo ,//- 1,
                branchService.ServiceId,
                PushId=stringPushId
            });

        }

        [HttpGet]
        [Route("GetFirstQueueUser")]
        [CustomAuthorizationFilter("Shift.Add")]
        [ApiExplorerSettings(GroupName = "Customer")]
        public async Task<IActionResult> GetFirstQueueUser()
        {
            // UserId=Login Tailor UserId
            var firstQueue =await GetNextQueue(UserId);
            return Ok(firstQueue);
        }

        [HttpPost]
        [Route("GetQueueNextUser")]
        [CustomAuthorizationFilter("Shift.Add")]
        [ApiExplorerSettings(GroupName = "Customer")]
        public async Task<IActionResult> GetQueueNextUser(ShiftQueue shift)
        {

            if (shift.Id == 0)
                return BadRequest(Messages.InvalidShiftId);
            if (shift.ShiftId == 0)
                return BadRequest(Messages.InvalidQueueId);

            shift.IsServiceDone = true;
            _shiftQueueService.UpdateQueue(shift);

            // remove PushId
            DeleteRecord(shift.PushId);

            // Add evaluationRecord To Mobile User Queue
            Evaluation evaluation = new Evaluation()
            {
                ShiftQueueId = shift.Id,
                IsDeleted = false,
              //  EvaluationValue = null,
                CreateAt = DateTime.UtcNow,
            };
            _evaluationService.Add(evaluation);




            var nextQueue =await GetNextQueue(UserId);
            //if(nextQueue!=null)
            //{
            //    nextQueue.UserIdBy = UserId;
            //    nextQueue.WindowNumber = shift.WindowNumber;
            //    _shiftQueueService.UpdateQueue(nextQueue);
            //}
           var userMobile= _UserService.GetById(shift.UserIdMobile);


            var data = new FirebaseMessage()
            {
                to = userMobile.UserDeviceId, 
                data = new MessageData()
                {

                    EvalutionId = evaluation.Id


                },
                notification = new Notification()
                {
                    Body = "تقييم الخدمه ",
                    content_available = true,
                    priority = "high",
                    title = "تقييم الخدمه "
                }



            };
            new Thread(delegate () {
                SendNotification(data);
            }).Start();
           
          //  if (SendNotification(data) == 1)
              


            return Ok(nextQueue);
        }
        
        [HttpPost]
        [Route("CancelCurrentMobileUser")]
        [CustomAuthorizationFilter("Shift.Add")]
        [ApiExplorerSettings(GroupName = "Customer")]
        public async Task<IActionResult> CancelAndGetNextUser(ShiftQueue shift)
        {
            if (shift.Id == 0)
                return BadRequest(Messages.InvalidShiftId);
            if (shift.ShiftId == 0)
                return BadRequest(Messages.InvalidQueueId);

            //remove current queue
            _shiftQueueService.Remove(shift);
            var nextQueue =await GetNextQueue(UserId);

            return Ok(nextQueue);
        }


        private async Task<ShiftQueue> GetNextQueue(int UserID)
        {
            ShiftUser shiftUser = (await _shiftUserService.FindAsync(a => a.UserId == UserID && !a.Shift.IsEnded)).FirstOrDefault();
            if (shiftUser == null)
                return null;

            ShiftQueue firstQueue = null;
            // Check If Teller has Pending User

            var Queues=(await _shiftQueueService.FindAsync(a => a.ShiftId == shiftUser.ShiftId && !a.IsServiceDone
               && a.UserIdBy == shiftUser.UserId && a.WindowNumber == shiftUser.WindowNumber && a.ServiceId == shiftUser.ServiceId, "UserMobile"));

            if(Queues!=null && Queues.Any())
            {
                firstQueue = Queues.OrderBy(a => a.UserTurn).FirstOrDefault();
                
                return firstQueue;
            }
            //
            Queues = (await _shiftQueueService.FindAsync(a => a.ShiftId == shiftUser.ShiftId && !a.IsServiceDone
               && a.UserIdBy == null && a.WindowNumber == null && a.ServiceId == shiftUser.ServiceId, "UserMobile"));

            
             firstQueue = Queues.OrderBy(a => a.UserTurn).FirstOrDefault();
            if (firstQueue == null)
                return null;
            //update this queue
            firstQueue.UserIdBy = UserId;
            firstQueue.WindowNumber = shiftUser.WindowNumber;
            _shiftQueueService.UpdateQueue(firstQueue);
            var similerServicedQueue = (await _shiftQueueService.FindAsync(a => a.ShiftId == firstQueue.ShiftId && a.ServiceId == firstQueue.ServiceId
                                  && !a.IsServiceDone && a.UserIdBy != null && a.WindowNumber != null, "Service")).OrderBy(a => a.UserTurn).LastOrDefault();
            var CurrentQueueNo = similerServicedQueue == null ? 0 : similerServicedQueue.UserTurn;
            UpdateRecord(new UpdateQueueRequest() { PushId = firstQueue.PushId, UpdatedRecord = new Record() {Counter= shiftUser.WindowNumber,queueNumber=firstQueue.UserTurn,currentQueue= CurrentQueueNo } });
            _shiftQueueService.UpdateQueue(firstQueue);

            return firstQueue;
        }

        [HttpGet]
        [Route("LoadMobileUserQueus")]
        //[CustomAuthorizationFilter("Shift.Add")]
        [ApiExplorerSettings(GroupName = "Mobile")]
        public async Task<IActionResult> LoadMobileUserQueus()
        {
            ////userId =UserId of mobile User
            //_shiftQueueService.FindAsync(a => a.ShiftId == OpenShift.Id && a.ServiceId == branchService.ServiceId
            //                      && !a.IsServiceDone /*&& a.UserIdBy != null && a.WindowNumber != null*/ )).OrderBy(a => a.UserTurn).FirstOrDefault();
            var queuse = (await _shiftQueueService.FindAsync(a => a.UserIdMobile == UserId && !a.IsServiceDone
                            && !a.Shift.IsEnded, "Service")).ToList();
            List<MobileUserQueue> mobileUserQueues = new List<MobileUserQueue>();
            foreach (var item in queuse)
            {
                //(await _shiftQueueService.FindAsync(a => a.ShiftId == OpenShift.Id && a.ServiceId == branchService.ServiceId
                //                     && !a.IsServiceDone /*&& a.UserIdBy != null && a.WindowNumber != null*/ )).OrderBy(a => a.UserTurn).FirstOrDefault();

                var similerServicedQueue = (await _shiftQueueService.FindAsync(a => a.ShiftId == item.ShiftId && a.ServiceId == item.ServiceId
                                  && !a.IsServiceDone /*&& a.UserIdBy != null && a.WindowNumber != null*/, "Service")).OrderBy(a => a.UserTurn).FirstOrDefault();
                var CurrentQueueNo = similerServicedQueue == null ? 0 : similerServicedQueue.UserTurn;

                mobileUserQueues.Add(new MobileUserQueue() {
                    QueueNo=item.UserTurn,
                    CurrentServiedQueueNo=CurrentQueueNo,
                    WaitNo = item.UserTurn - CurrentQueueNo - 1,
                    ServiceId=item.ServiceId,
                    PushId=item.PushId,
                    Service=item.Service,
                    WindowNumber=item.WindowNumber
                });
            }

            return Ok(mobileUserQueues);

        }

        #endregion

        #region Firebase
        [ApiExplorerSettings(IgnoreApi = true)]
        public string SendMessage(Record record)
        {

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(record);
            var request = WebRequest.CreateHttp("https://qreduction.firebaseio.com/.json?auth=QkVyC6849JtnoWmNi0dNPcuXBS5XxLwLsaf0y1k4");
            request.Method = "POST";
            request.ContentType = "application/json";
            var buffer = Encoding.UTF8.GetBytes(json);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            var response = request.GetResponse();
            json = (new StreamReader(response.GetResponseStream())).ReadToEnd();

          var obj=  Newtonsoft.Json.JsonConvert.DeserializeObject<PushIdObj>(json);

            return obj.Name;

        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public void DeleteRecord(string PushId)
        {
            var request = WebRequest.CreateHttp("https://qreduction.firebaseio.com/" + PushId + ".json?auth=QkVyC6849JtnoWmNi0dNPcuXBS5XxLwLsaf0y1k4");
            request.Method = "DELETE";
            request.ContentType = "application/json";
          
            var response = request.GetResponse();
            var json = (new StreamReader(response.GetResponseStream())).ReadToEnd();



           // return Ok(json);












        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public string UpdateRecord(UpdateQueueRequest updateQueueRequest)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(updateQueueRequest.UpdatedRecord);
            var request = WebRequest.CreateHttp("https://qreduction.firebaseio.com/" + updateQueueRequest.PushId + ".json?auth=QkVyC6849JtnoWmNi0dNPcuXBS5XxLwLsaf0y1k4");
            request.Method = "PUT";
            request.ContentType = "application/json";
            var buffer = Encoding.UTF8.GetBytes(json);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            var response = request.GetResponse();
            json = (new StreamReader(response.GetResponseStream())).ReadToEnd();



            return json;



        }

        #endregion

        #region old

        //[HttpPost]
        //[Route("GetQueueNextUser1")]
        //[CustomAuthorizationFilter("Shift.Add")]
        //[ApiExplorerSettings(GroupName = "Customer")]
        //public async Task<IActionResult> GetQueueNextUser1(ShiftQueue shift)
        //{

        //    if (shift.Id == 0)
        //        return BadRequest(Messages.InvalidShiftId);
        //    if (shift.ShiftId == 0)
        //        return BadRequest(Messages.InvalidQueueId);

        //    var NextUser = _shiftQueueService.GetNextQueueUser(shift);

        //    //if (await _shiftQueueService.AnyAsync(s => s.BranchId == shift.BranchId && !s.IsEnded))
        //    //    return BadRequest(Messages.CantOpenShiftWhileOpendOne);

        //    //if (await _shiftQueueService.AnyAsync(s => s.Code == shift.Code))
        //    //    return BadRequest(Messages.Exist_Code);

        //    //shift.StartAt = DateTime.UtcNow;
        //    //shift.QRCode = Guid.NewGuid().ToString();
        //    //shift.IsEnded = false;
        //    //shift.EndAt = null;

        //    //shift.CreateAt = DateTime.UtcNow;
        //    //shift.CreateBy = UserId;
        //    //await _shiftService.AddAsync(shift);
        //    return Ok(NextUser);
        //}

        //[HttpGet]
        //[Route("LoadUserQueue")]
        //[CustomAuthorizationFilter("Shift.Add")]
        //[ApiExplorerSettings(GroupName = "Customer")]
        //public async Task<IActionResult> LoadUserQueue1()
        //{
        //    //getShiftUser
        //    ShiftUser shiftUser = (await _shiftUserService.FindAsync(a => a.UserId == UserId && !a.Shift.IsEnded)).FirstOrDefault();
        //    if (shiftUser == null)
        //        return BadRequest(Messages.NotAssignedUserInShift);

        //    List<ShiftQueue> shiftQueues = (await _shiftQueueService.FindAsync(a => a.ShiftId == shiftUser.ShiftId && a.WindowNumber == shiftUser.WindowNumber
        //            && a.UserIdBy == UserId && !a.IsServiceDone)).OrderBy(a => a.UserTurn).ToList();


        //    //LoadQueueResponse loadQueueResponse = new LoadQueueResponse();

        //    //var queue = await _shiftQueueService.FindAsync(q => q.UserIdBy == UserId, "Service");


        //    //loadQueueResponse.Queue = queue.OrderBy(r => r.UserTurn).ToList();

        //    //    loadQueueResponse.ServiceToProvide = loadQueueResponse?.Queue?.FirstOrDefault()?.Service;


        //    return Ok(shiftQueues);
        //}
        //[HttpGet]
        //[Route("SetMobileUserQueue")]
        ////[CustomAuthorizationFilter("Shift.Add")]
        //[ApiExplorerSettings(GroupName = "Mobile")]
        //public async Task<IActionResult> SetMobileUserQueue1(int branchServiceId)
        //{

        //    if (branchServiceId == 0)
        //        return BadRequest(Messages.InvalidShiftId);

        //    // get branch Service
        //    BranchService branchService = (await _branchServiceService.FindAsync(item => item.Id == branchServiceId)).SingleOrDefault();
        //    if (branchService == null)
        //        return BadRequest(Messages.InvalidQueueId);

        //    // get oppening shift
        //    Shift OpenShift = (await _shiftService.FindAsync(a => !a.IsEnded && a.BranchId == branchService.BranchId)).SingleOrDefault();
        //    if (OpenShift == null)
        //        return BadRequest(Messages.NoShiftOpenInThisBranch);

        //    //get shift user
        //    List<ShiftUser> shiftUsers = (await _shiftUserService.FindAsync(a => a.ShiftId == OpenShift.Id && a.ServiceId == branchService.ServiceId)).ToList();
        //    if (shiftUsers?.Count <= 0)
        //        return BadRequest(Messages.NotAssignedUserInShift);

        //    // loop in users
        //    // if i have one user in this shift, this mean on window number and one queue


        //    List<ClassQueue> queues = new List<ClassQueue>();
        //    foreach (var item in shiftUsers)
        //    {
        //        queues.Add(new ClassQueue()
        //        {
        //            UserWindow = item,
        //            QueueLst = _shiftQueueService.Find(a => a.ServiceId == branchService.ServiceId && a.ShiftId == item.ShiftId &&
        //          a.WindowNumber == item.WindowNumber && !a.IsServiceDone).OrderBy(a => a.UserTurn).ToList()
        //        });
        //    }
        //    ClassQueue OptimumQueu = queues.OrderBy(q => q.QueueLst.Count()).FirstOrDefault();
        //    // add in ShiftQueue
        //    _shiftQueueService.Add(new ShiftQueue()
        //    {
        //        IsServiceDone = false,
        //        ServiceId = branchService.ServiceId,
        //        ShiftId = OpenShift.Id,
        //        UserTurn = OptimumQueu.QueueLst?.Count <= 0 ? 1 : OptimumQueu.QueueLst.LastOrDefault().UserTurn + 1,
        //        UserIdMobile = UserId,
        //        WindowNumber = OptimumQueu.UserWindow.WindowNumber,
        //        UserIdBy = OptimumQueu.UserWindow.UserId
        //    });

        //    return Ok(new
        //    {
        //        OptimumQueu.UserWindow.WindowNumber,
        //        branchService.ServiceId,
        //        ShiftId = OpenShift.Id,
        //        UserIdBy = OptimumQueu.UserWindow.UserId,
        //        UserTurn = OptimumQueu.QueueLst?.Count <= 0 ? 1 : OptimumQueu.QueueLst.LastOrDefault().UserTurn + 1
        //    });

        //    //ShiftQueue   MobileUserQueue = new ShiftQueue()
        //    //{
        //    //    WindowNumber=OptimumQueu.UserWindow.WindowNumber,
        //    //     UserIdBy=OptimumQueu.UserWindow.UserId,
        //    //     UserTurn= OptimumQueu.QueueLst?.Count <= 0 ? 1 :OptimumQueu.QueueLst.FirstOrDefault().UserTurn+1,
        //    //     ShiftId=OpenShift.Id,
        //    //     ServiceId=branchService.ServiceId

        //    //};


        //    //foreach (var item in shiftUsers) 
        //    //{
        //    //    var queues = _shiftQueueService.Find(a => a.ServiceId == branchService.ServiceId && a.ShiftId == item.ShiftId &&
        //    //      a.WindowNumber == item.WindowNumber && !a.IsServiceDone ).ToList();
        //    //    if (queues?.Count <= 0) //this mean this queue if first one
        //    //        shiftQueues.Add(new ShiftQueue() {
        //    //            IsServiceDone=false,
        //    //            ShiftId=OpenShift.Id,
        //    //            ServiceId=branchService.ServiceId,
        //    //            WindowNumber=item.WindowNumber,
        //    //            UserTurn=0,
        //    //            UserIdBy=item.UserId
        //    //        });
        //    //    else
        //    //        shiftQueues.AddRange(queues);

        //    //}

        //}
        // cancel function mean this mobileUser is canceled and must remove it
        //[HttpPost]
        //[Route("CancelCurrentMobileUser1")]
        ////[CustomAuthorizationFilter("Shift.Add")]
        //[ApiExplorerSettings(GroupName = "Customer")]
        //public async Task<IActionResult> CancelAndGetNextUser1(ShiftQueue shift)
        //{

        //    if (shift.Id == 0)
        //        return BadRequest(Messages.InvalidShiftId);
        //    if (shift.ShiftId == 0)
        //        return BadRequest(Messages.InvalidQueueId);

        //    var NextUser = _shiftQueueService.CancelAndNextQueueUser(shift);

        //    return Ok(NextUser);
        //}

        #endregion


        [HttpPost]
        [Route("SendMessageToMe")]
        [ProducesResponseType(typeof(SendInstantMessageResponse), 200)]
        [ApiExplorerSettings(GroupName = "Mobile")]
        public async Task<IActionResult> SendMessage(SendInstantMessageRequest UserGuid)
        {

            User CurrentUser = (await _UserService.FindAsync(s => s.UserGuid.ToString() == UserGuid.UserGuid)).SingleOrDefault();

            if (CurrentUser == null)
                return NotFound(new UserDeviceResponse { Message = "User Not Found" });
            if (CurrentUser.UserDeviceId == null)
                return NotFound(new UserDeviceResponse { Message = "User Doesnt have Device token / Device ID" });

            Random rnd = new Random();

            var data = new FirebaseMessage()
            {
                to = CurrentUser.UserDeviceId, // iphone 6s test token
                data = new MessageData()
                {

                    EvalutionId = 100


                },
                notification = new Notification()
                {
                    Body = "تم تحديث الطابور ",
                    content_available = true,
                    priority = "high",
                    title = "تم تحديث ترتيبك "
                }



            };
            if (SendNotification(data) == 1)
                return Ok(new SendInstantMessageResponse() { Message = "Ok you should Receive message Now :)" });
            else
                return Ok(new SendInstantMessageResponse() { Message = "Sorry Fail to Send :(" });

        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public int SendNotification(object data)
        {


            var json = JsonConvert.SerializeObject(data, Formatting.Indented);


            Byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(json);

            return SendNotification(byteArray);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public int SendNotification(Byte[] byteArray)
        {

            try
            {
                String server_api_key = _configuration.GetValue<string>("NotificationService:SERVER_API_KEY", "defualtKey");
                String senderid = _configuration.GetValue<string>("NotificationService:senderid", "defualtKey");

                WebRequest type = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                type.Method = "post";
                type.ContentType = "application/json";
                type.Headers.Add($"Authorization: key={server_api_key}");
                type.Headers.Add($"Sender: id={senderid}");

                type.ContentLength = byteArray.Length;
                Stream datastream = type.GetRequestStream();
                datastream.Write(byteArray, 0, byteArray.Length);
                datastream.Close();

                WebResponse respones = type.GetResponse();
                datastream = respones.GetResponseStream();
                StreamReader reader = new StreamReader(datastream);

                String sresponessrever = reader.ReadToEnd();
                reader.Close();
                datastream.Close();
                respones.Close();
                return 1;

            }
            catch (Exception)
            {
                return 0;
            }

        }

    

    }
    #region Models


    public class ScanResponse
    {
        public string Message { get; set; }

        public ScanData data { get; set; }
    }

    public class ScanData
    {
        public Organization Organization { get; set; }

        public Branch Branch { get; set; }


        public List<BranchService> Serivces { get; set; }


        public List<Instruction> Instructions { get; set; }
    }


    public class UserDeviceRequest
    {
        public string DeviceId { get; set; }

        public string UserGuid { get; set; }
    }



    public class SelectServiceRequest
    {
        public string BranchId { get; set; }

        public string ServiceId { get; set; }
    }

    public class UserDeviceResponse
    {
        public string Message { get; set; }
        public User data { get; set; }
    }

    public class SendInstantMessageRequest
    {
        public string UserGuid { get; set; }


    }
    public class SendInstantMessageResponse
    {
        public string Message { get; set; }


    }


    #endregion #Models
    public class UpdateQueueRequest
    {

        public string PushId { get; set; }

        public Record UpdatedRecord { get; set; }
    }

    public class Record
    {
        public Record()
        {
        }

        public int currentQueue { get; set; }
        public string Counter { get; set; }
        public int queueNumber { get; set; }
    }

    public class LoadQueueResponse
    {
        public  Service ServiceToProvide { get; set; }

        public  List<ShiftQueue> Queue { get; set; }
    }
  
    public class MobileUserQueue
    {
        public int QueueNo { get; set; }
        public int CurrentServiedQueueNo { get; set; }
        public int WaitNo { get; set; }
        public int ServiceId { get; set; }
        public string PushId { get; set; }
        public Service Service { get; set; }
        public string WindowNumber { get; set; }
      //  public int ServiceName { get; set; }

    }

    public class PushIdObj
    {
        public string Name { get; set; }
    }
}