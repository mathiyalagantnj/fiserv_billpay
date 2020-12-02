using EFNgApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFNgApp.Interfaces
{
    public interface IEmployee
    {
        IEnumerable<TblEmployee> GetAllEmployees();
        int AddEmployee(TblEmployee employee);
        int UpdateEmployee(TblEmployee employee);

        int UpdateAmount(object amountupdate);

        Ledger LedgeUpdate(object ledger);
        TblEmployee GetEmployeeData(int id);

        User GetUserData(string emailId);

        User GetMobileData(string mobile);
        int DeleteEmployee(int id);

        Account Getaccountinfo (int id);
        List<TblCities> GetCities();
    }
}
