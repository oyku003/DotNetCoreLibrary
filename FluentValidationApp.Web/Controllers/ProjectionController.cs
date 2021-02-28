using AutoMapper;
using FluentValidationApp.Web.DTOs;
using FluentValidationApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationApp.Web.Controllers
{
    public class ProjectionController : Controller
    {
        private readonly IMapper mapper;
        public ProjectionController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Index(EventDateDto eventDateDto)
        {
            var eventDate = mapper.Map<EventDate>(eventDateDto);

            ViewBag.date = eventDate.Date.ToShortDateString();
            return View();
        }
    }
}
