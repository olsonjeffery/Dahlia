﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Dahlia.Models;
using Dahlia.Repositories;
using Dahlia.ViewModels;

namespace Dahlia.Controllers
{
    public class RetreatController : Controller
    {
        IRetreatRepository _RetreatRepository;

        public RetreatController()
        {
            _RetreatRepository = new RetreatRepository();
        }

        public ActionResult Index()
        {
            var model = new RetreatListViewModel
            {
                CreateLink = new Uri("/Retreat/Create", UriKind.Relative),

                Retreats = _RetreatRepository.GetList().Select(x => new RetreatListRetreatViewModel
                {
                    Date = x.StartDate,
                    AddParticipantLink = new Uri("../Participant/AddToRetreat?retreatDate=" + x.StartDate.ToString("d"), UriKind.Relative)
                })
            };

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Retreat retreatModel)
        {
            if (!ModelState.IsValid)
                return View();
            _RetreatRepository.Add(retreatModel);
            return RedirectToAction("Index");
        }
    }
}
