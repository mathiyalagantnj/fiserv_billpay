using EFNgApp.Interfaces;
using EFNgApp.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace EFNgApp.DataAccess
{
    public class EmployeeDataAccessLayer : IEmployee
    {
        private myTestDBContext db;

        public EmployeeDataAccessLayer(myTestDBContext _db)
        {
            db = _db;
        }

        public IEnumerable<TblEmployee> GetAllEmployees()
        {
            try
            {
                return db.TblEmployee.ToList().OrderBy(x => x.EmployeeId);
            }
            catch
            {
                throw;
            }
        }

        //To Add new employee record 
        public int AddEmployee(TblEmployee employee)
        {
            try
            {
                db.TblEmployee.Add(employee);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        //To Update the records of a particluar employee
        public int UpdateEmployee(TblEmployee employee)
        {
            try
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();

                return 1;
            }
            catch
            {
                throw;
            }
        }

        //Get the details of a particular employee
        public TblEmployee GetEmployeeData(int id)
        {
            try
            {
                TblEmployee employee = db.TblEmployee.Find(id);
                return employee;
            }
            catch
            {
                throw;
            }
        }

        public Account Getaccountinfo(int id)
        {
            try
            {
                var account = (from x in db.account
                             where x.user_id == id
                             select x).FirstOrDefault();

                return account;
            }
            catch
            {
                throw;
            }
        }


        public User GetUserData(string emailId)
        {
            try
            {
               // User user = db.user.Find(id);
                var users = (from x in db.user
                             where x.email_id == emailId
                             select x).FirstOrDefault();

                return users;
            }
            catch
            {
                throw;
            }
        }

        public User GetMobileData(string mobile)
        {
            try
            {
                //User user = db.user.Find();

                var users = (from x in db.user
                             where x.mobile_number == mobile
                             select x).FirstOrDefault();
                return users;
            }
            catch
            {
                throw;
            }
        }

        public int UpdateAmount(object amountupdate)
        {
            try
            {
                var setobject = amountupdate;
                var details = JObject.Parse(amountupdate.ToString());

                int fromamount = Convert.ToInt32(details["sendAmount"]);
                int toamount = Convert.ToInt32(details["toAmount"]);
                int fromId = Convert.ToInt32(details["senduserId"]);
                int toId = Convert.ToInt32(details["touserId"]);
             
                Account prod = (from product in db.account
                                where product.user_id == fromId
                                select product).Single();

                prod.balance = Convert.ToDouble(fromamount-toamount);
                db.SaveChanges(); // ditto

                Account prodcredit = (from product in db.account
                                where product.user_id == toId
                                      select product).Single();

                prodcredit.balance = Convert.ToDouble(prodcredit.balance + toamount);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                throw;
            }
        }



        public Ledger LedgeUpdate(object ledger)
        {
            try
            {

                var setobject = ledger;
                var details = JObject.Parse(ledger.ToString());

                int fromamount = Convert.ToInt32(details["sendAmount"]);
                int toamount = Convert.ToInt32(details["toAmount"]);
                int fromId = Convert.ToInt32(details["transaction_from_id"]);
                int toId = Convert.ToInt32(details["transaction_to_id"]);



                Account prod = (from product in db.account
                                where product.user_id == fromId
                                select product).Single();

               


                //Account prodcredit = (from product in db.account
                //                      where product.user_id == toId
                //                      select product).Single();

                //prodcredit.balance = Convert.ToDouble(prodcredit.balance + toamount);

                Ledger ledgertbl = new Ledger();
                double accountbalance = prod.balance;
                ledgertbl.acc_balance = accountbalance;
                ledgertbl.ledger_balance = prod.balance+ Convert.ToInt32(details["transaction_amt"]);
                ledgertbl.transaction_amt= Convert.ToInt32(details["transaction_amt"]);
                ledgertbl.transaction_date = DateTime.Now;
                ledgertbl.transaction_from = details["transaction_from"].ToString();
                ledgertbl.transaction_from_id= Convert.ToInt32(details["transaction_from_id"]);
                ledgertbl. transaction_reference_number= details["transaction_reference_number"].ToString();
                ledgertbl.transaction_remark= details["transaction_remark"].ToString();
                ledgertbl.transaction_status = "Success";
                ledgertbl.transaction_to= details["transaction_to"].ToString();
                ledgertbl.transaction_to_id= Convert.ToInt32(details["transaction_to_id"]);
                ledgertbl.transaction_type= "DEBIT";
                ledgertbl.user_id = Convert.ToInt32(details["user_id"]);
                db.ledger.Add(ledgertbl);           // pass the table object 
                db.SaveChanges();

                Account debit = (from product in db.account
                                where product.user_id == toId
                                 select product).Single();


                Ledger ledgertbls = new Ledger();
                double beditbalance = debit.balance;
                ledgertbls.acc_balance = beditbalance;
                ledgertbls.ledger_balance = debit.balance- Convert.ToInt32(details["transaction_amt"]);
                ledgertbls.transaction_amt = Convert.ToInt32(details["transaction_amt"]);
                ledgertbls.transaction_date = DateTime.Now;
                ledgertbls.transaction_from = details["transaction_from"].ToString();
                ledgertbls.transaction_from_id = Convert.ToInt32(details["transaction_from_id"]);
                ledgertbls.transaction_reference_number = details["transaction_reference_number"].ToString();
                ledgertbls.transaction_remark = details["transaction_remark"].ToString();
                ledgertbls.transaction_status = details["transaction_status"].ToString();
                ledgertbls.transaction_to = details["transaction_to"].ToString();
                ledgertbls.transaction_to_id = Convert.ToInt32(details["transaction_to_id"]);
                ledgertbls.transaction_type = details["transaction_type"].ToString();
                ledgertbls.user_id = Convert.ToInt32(debit.user_id);
                db.ledger.Add(ledgertbls);           // pass the table object 
                db.SaveChanges();

                var ledgerdetails = (from x in db.ledger
                             where x.transaction_reference_number == ledgertbls.transaction_reference_number 
                                     select x).FirstOrDefault();


                return ledgerdetails;
                
            }
            catch
            {
                throw;
            }
        }



        //To Delete the record on a particular employee
        public int DeleteEmployee(int id)
        {
            try
            {
                TblEmployee emp = db.TblEmployee.Find(id);
                db.TblEmployee.Remove(emp);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        //To Get the list of Cities
        public List<TblCities> GetCities()
        {
            List<TblCities> lstCity = new List<TblCities>();
            lstCity = (from CityList in db.TblCities select CityList).ToList();

            return lstCity;
        }
    }
}
