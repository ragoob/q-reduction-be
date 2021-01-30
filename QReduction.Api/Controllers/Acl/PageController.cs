using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QReduction.Apis.Controllers;
using QReduction.Apis.Infrastructure;
using QReduction.Apis.Models;
using QReduction.Core.Service.Generic;

namespace QReduction.Apis.Controllers.Acl
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("QReductionPolicy")]
    [ValidateModelFilter]
    public class PageController : CustomBaseController
    {
        #region Fields

        private readonly IService<Page> _pageService;

        #endregion

        #region ctor

        public PageController(IService<Page> pageService)
        {
            _pageService = pageService;
        }

        #endregion

    }
}