using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFNgApp.Interfaces;
using EFNgApp.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFNgApp.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployee objemployee;


        public EmployeeController(IEmployee _objemployee)
        {
            objemployee = _objemployee;
        }

        [HttpGet]
        [Route("Index")]
        public IEnumerable<TblEmployee> Index()
        {
            return objemployee.GetAllEmployees();
        }

        [HttpPost]
        [Route("Create")]
        public int Create([FromBody] TblEmployee employee)
        {
            return objemployee.AddEmployee(employee);
        }

        [HttpGet]
        [Route("Details/{id}")]
        public TblEmployee Details(int id)
        {
            return objemployee.GetEmployeeData(id);
        }


        [HttpGet]
        [Route("check/{emailId}")]
        public User check(string emailId)
        {
           
           
            return objemployee.GetUserData(emailId);
        }


        [HttpGet]
        [Route("verify/{mobile}")]
        public User verify(string mobile)
        {
            return objemployee.GetMobileData(mobile);
        }


        [HttpGet]
        [Route("account/{id}")]
        public Account account(int id)
        {

            return objemployee.Getaccountinfo(id);
        }


        [HttpPut]
        [Route("Edit")]
        public int Edit([FromBody]TblEmployee employee)
        {
            return objemployee.UpdateEmployee(employee);
        }


        [HttpPost]
        [Route("updateamount")]
        public int updateamount([FromBody] object amountupdate)
        {
            return objemployee.UpdateAmount(amountupdate);
        }

        [HttpPost]
        [Route("ledgerupdate")]
        public Ledger ledgerupdate([FromBody] object ledger)
        {
            return objemployee.LedgeUpdate(ledger);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public int Delete(int id)
        {
            return objemployee.DeleteEmployee(id);
        }

        [HttpGet]
        [Route("GetCityList")]
        public IEnumerable<TblCities> Details()
        {
            return objemployee.GetCities();
        }

       
    }
}
