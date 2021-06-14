﻿using Application.Common.Models;
using Application.MVC.Models;
using Application.Service.Common;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserIdentityService userIdentityService;

        public UserController(IMapper mapper, IUserIdentityService UserIdentityService)
        {
            this.mapper = mapper;
            userIdentityService = UserIdentityService;
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel userViewModel)
        {
            try
            {
                //await userIdentityService.AddAsync(mapper.Map<UserIdentityDTO>(userViewModel));
                return RedirectToAction(nameof(Users));
            }
            catch
            {
                return View();
            }
        }

        // POST: UserIdentityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserViewModel userViewModel)
        {
            try
            {
                //await userIdentityService.AddAsync(mapper.Map<UserIdentityDTO>(userViewModel));
                return RedirectToAction(nameof(Users));
            }
            catch
            {
                return View();
            }
        }

        // GET: SeminarController
        [HttpGet("Users", Name = "get-users")]
        public async Task<ActionResult> Users(string sortOrder, string sortBy, string searchBy, string search, int? pageNumber, int? pageSize)
        {
            var paging = new Paging(pageNumber, pageSize);

            sortOrder = string.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;
            ViewBag.Sorting = sortOrder;
            ViewBag.SortBy = sortBy;
            ViewBag.Search = !string.IsNullOrEmpty(search) ? search : "";
            ViewBag.SearchBy = !string.IsNullOrEmpty(searchBy) ? searchBy : "Name";
            ViewBag.CurrentPage = paging.PageNumber;
            ViewBag.PageSize = paging.PageSize;

            var result = await userIdentityService.GetAllAsync(new Sorting(sortOrder, sortBy), new Filtering(searchBy, search), paging);

            var pageCount = paging.TotalItemsCount / paging.PageSize;
            ViewBag.TotalPageCount = paging.TotalItemsCount % paging.PageSize == 0 ? pageCount : pageCount + 1;

            return View(mapper.Map<IEnumerable<UserViewModel>>(result));
        }

        // GET: UserIdentityController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var result = await userIdentityService.GetAsync(id);
            return View();
        }

        // GET: UserIdentityController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var result = await userIdentityService.GetAsync(id);
            return View();
        }

        // POST: UserIdentityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Users));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserIdentityController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserIdentityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(UserViewModel userViewModel)
        {
            try
            {
                var result = await userIdentityService.DeleteAsync(mapper.Map<UserIdentityDTO>(userViewModel));
                return RedirectToAction(nameof(Users));
            }
            catch
            {
                return View();
            }
        }
    }
}
