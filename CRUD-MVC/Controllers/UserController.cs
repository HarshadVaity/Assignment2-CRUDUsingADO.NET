using DAL;
using DataModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_MVC.Controllers
{
    public class UserController : Controller
    {
        public UserService userService;
        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Index()
        {
            var users = userService.GetAllUsers();
            return View(users);
        }

        public IActionResult Create()
        {
            ViewBag.States = userService.GetAllStates();
            return View();
        }
        [HttpPost]
        public IActionResult Create(User model)
        {
            try
            {
                var created = userService.CreateUser(model);

                if(created)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }
                

            }
            catch (Exception)
            {


            }

            ViewBag.States = userService.GetAllStates();

            return View();

        }

        public IActionResult Edit(int id)
        {
            var user = userService.GetUser(id);
            ViewBag.States = userService.GetAllStates();
            return View("Create", user);
        }
        [HttpPost]
        public IActionResult Edit(User model)
        {
            try
            {
                var created = userService.UpdateUser(model);

                if (created)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }


            }
            catch (Exception)
            {


            }

            ViewBag.States = userService.GetAllStates();

            return View("Create", model);

        }

        public IActionResult Delete(int id)
        {

            userService.DeleteUser(id);


            return RedirectToAction("Index");
        }
    }
}
