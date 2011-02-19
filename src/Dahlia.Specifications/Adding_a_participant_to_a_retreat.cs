﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Dahlia.Controllers;
using Dahlia.Models;
using Dahlia.Repositories;
using Dahlia.ViewModels;
using Machine.Specifications;
using Rhino.Mocks;

namespace Dahlia.Specifications
{
    [Subject("Adding a participant to a retreat")]
    public class When_showing_the_add_participant_screen_for_a_retreat
    {
        Establish context = () =>
                            {
                                _retreatDate = new DateTime(2007, 12, 15);
                                _controller = new ParticipantController(null);
                            };

        Because of = () =>
                                 {
                                     _viewResult = _controller.AddToRetreat(_retreatDate);
                                     _viewModel = (AddParticipantToRetreatViewModel) _viewResult.ViewData.Model;
                                 };

        It should_return_a_view_result_for_the_add_participant_view = () => _viewResult.ViewName.ShouldEqual("AddToRetreat");

        It should_return_a_view_model_that_contains_the_retreat_date = () => _viewModel.RetreatDate.ShouldEqual(_retreatDate);

        It should_have_a_default_date_equal_to_today_for_the_received_date = () => _viewModel.DateReceived.ShouldEqual(DateTime.Today);

        static DateTime _retreatDate;
        static ViewResult _viewResult;
        static AddParticipantToRetreatViewModel _viewModel;
        static ParticipantController _controller;
    }

    [Subject("Adding a participant to a retreat")]
    public class When_posting_back_from_the_add_participant_screen_for_a_retreat
    {
        Establish context = () =>
        {
            var retreatDate = new DateTime(2007, 12, 15);
            _viewModel = new AddParticipantToRetreatViewModel
                         {
                             RetreatDate = retreatDate,
                             BedCode = "bedcode",
                             FirstName = "firstbob",
                             LastName = "lastmartin",
                             DateReceived = DateTime.Today,
                             Notes = "yada yada yada...",
                         };
            _retreatDate = retreatDate;
            
            _retreatRepository = MockRepository.GenerateStub<IRetreatRepository>();
            _controller = new ParticipantController(_retreatRepository);

            _retreat = new Retreat{};
            _retreatRepository.Stub(x => x.Get(retreatDate)).Return(_retreat);
        };

        Because of = () => _controller.DoAddToRetreat(_viewModel);

        It should_save_the_retreat = () => _retreatRepository.AssertWasCalled(x => x.Save(_retreat));
        It should_add_the_participant_to_the_retreat = () => _retreat.RegisteredParticipants.Count.ShouldEqual(1);
        It should_give_the_participant_the_right_first_name = () => _retreat.RegisteredParticipants[0].Participant.FirstName.ShouldEqual(_viewModel.FirstName);
        It should_give_the_participant_the_right_last_name = () => _retreat.RegisteredParticipants[0].Participant.LastName.ShouldEqual(_viewModel.LastName);
        It should_give_the_participant_the_right_date_recieved = () => _retreat.RegisteredParticipants[0].Participant.DateReceived.ShouldEqual(_viewModel.DateReceived);
        It should_give_the_participant_the_right_notes = () => _retreat.RegisteredParticipants[0].Participant.Notes.ShouldEqual(_viewModel.Notes);

        It should_assign_the_right_bed_code = () => _retreat.RegisteredParticipants[0].BedCode.ShouldEqual(_viewModel.BedCode);
        It should_assign_the_retreat = () => _retreat.RegisteredParticipants[0].Retreat.ShouldEqual(_retreat);

        static DateTime _retreatDate;
        static AddParticipantToRetreatViewModel _viewModel;
        static ParticipantController _controller;
        static IRetreatRepository _retreatRepository;
        static Retreat _retreat;
    }
}
