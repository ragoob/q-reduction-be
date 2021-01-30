using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QReduction.Apis.Controllers;
using QReduction.Apis.Infrastructure;
using Microsoft.AspNetCore.Cors;
using QReduction.Core.Service.Generic;
using QReduction.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using QReduction.Core.Infrastructure;
using QReduction.Core.Domain.Acl;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using QReduction.Api.Models;
//using Swashbuckle.AspNetCore.Annotations;
using QReduction.Apis.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QReduction.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [EnableCors("QReductionPolicy")]
    [ValidateModelFilter]
  //  [ApiExplorerSettings(GroupName = "Mobile")]
 
    public class MobileUserController :CustomBaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IService<Shift> _shiftService;
        private readonly IService<Branch> _branchService;
        private readonly IService<BranchService> _branchServiceService;
        private readonly IService<Organization> _OrganizationService;

        private readonly IService<Instruction> _OrgInstructions;

        private readonly IService<User> _UserService;
        public MobileUserController(IService<Shift> shiftService, IService<Branch> branchService,
            IService<BranchService> branchServiceService, IService<Organization> OrganizationService,
             IService<User> UserService, IConfiguration configuration,
             IService<Instruction> OrgInstructions
            )
        {
            _shiftService = shiftService;
            _branchService = branchService;
            _branchServiceService = branchServiceService;
            _OrganizationService = OrganizationService;
            _configuration = configuration;
            _UserService = UserService;

            _OrgInstructions = OrgInstructions;
        }
        [HttpGet]
        [Route("GetBranchServicesByQrCode/{qrCode}")]
     
        [ProducesResponseType(typeof(ScanResponse), 200)]
        [ProducesResponseType(typeof(string), 404)]
       
        public async Task<IActionResult> GetBranchServicesByQrCode(string qrCode)
        {
            Branch branch = (await _branchService.FindAsync(s => s.QrCode == qrCode, "Organization", "BranchServices")).SingleOrDefault();

            if (branch == null)
                return NotFound();

            IEnumerable<BranchService> services =
              (await _branchServiceService.FindAsync(bs => bs.BranchId == branch.Id,
              "Service"));
            

            
            return Ok(
                new ScanResponse
                {
                    Message="success",
                    data= new ScanData()
                    {
                        
                        Serivces = services?.ToList(),
                        Instructions = _OrgInstructions.Find(i=>i.OrganizationId==branch.OrganizationId)?.ToList()
                    }
                });
        }


        [HttpPost]
        [Route("UpdateUserDeviceId")]
    
      
        [ApiExplorerSettings(GroupName = "Mobile")]
        public async Task<IActionResult> UpdateUserDeviceId(UserDeviceRequest userDeviceRequest)
        {

            if (string.IsNullOrEmpty(userDeviceRequest.DeviceId))
                return BadRequest(new UserDeviceResponse() { Message = "Device Id is Required", data = null });

            if (string.IsNullOrEmpty(userDeviceRequest.UserGuid))
                return BadRequest(new UserDeviceResponse() { Message = "User GUID is Required", data = null });

            User CurrentUser = (await _UserService.FindAsync(s => s.UserGuid.ToString() == userDeviceRequest.UserGuid)).SingleOrDefault();

            if (CurrentUser == null)
                return NotFound(new UserDeviceResponse() { Message = "User is Not Found", data = null });


            CurrentUser.UserDeviceId = userDeviceRequest.DeviceId;
            await _UserService.EditAsync(CurrentUser);
            return Ok(
                new UserDeviceResponse
                {
                    Message = "success",
                    data = CurrentUser
                });
        }




        //[HttpPost]
        //[Route("TestPushNotification")]
        //[ApiExplorerSettings(GroupName = "Mobile")]
        //public async Task<IActionResult> TestPushNotification([FromBody]SendInstantMessageRequest UserGuid)
        //{

        //    User CurrentUser = (await _UserService.FindAsync(s => s.UserGuid.ToString() == UserGuid.UserGuid)).SingleOrDefault();

        //    if (CurrentUser == null)
        //        return NotFound(new UserDeviceResponse { Message = "User Not Found" });
        //    if (CurrentUser.UserDeviceId == null)
        //        return NotFound(new UserDeviceResponse { Message = "User Doesnt have Device token / Device ID" });

        //    Random rnd = new Random();

        //    var data = new FirebaseMessage()
        //    {
        //        to = CurrentUser.UserDeviceId, // iphone 6s test token
        //        data = new MessageData()
        //        {

        //            QueueOrder = rnd.Next(1, 10).ToString()


        //        },
        //        notification = new Notification()
        //        {
        //            Body = "تم تحديث الطابور ",
        //            content_available = true,
        //            priority = "high",
        //            title = "تم تحديث ترتيبك "
        //        }



        //    };
        //    if (SendNotification(data) == 1)
        //        return Ok(new SendInstantMessageResponse() { Message = "Ok you should Receive message Now :)" });
        //    else
        //        return Ok(new SendInstantMessageResponse() { Message = "Sorry Fail to Send :(" });

        //}
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


}
