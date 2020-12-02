import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Employee } from 'src/models/employee';
import { User } from 'src/models/user';
import { Observable } from 'rxjs';
import { Amount } from 'src/models/Amount';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  myAppUrl = '';

  constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl;
  }

  getCityList() {
    return this._http.get(this.myAppUrl + 'api/Employee/GetCityList')
      .pipe(map(
        response => {
          return response;
        }));
  }

  getEmployees() {
    console.log('service called');
    return this._http.get<Employee[]>(this.myAppUrl + 'api/Employee/Index').pipe(map(
      response => {
        return response;
      }));
  }

  getEmployeeById(id: number) {
    return this._http.get(this.myAppUrl + 'api/Employee/Details/' + id)
      .pipe(map(
        response => {
          return response;
        }));
  }

  saveEmployee(employee: Employee) {
    console.log('service called');
    return this._http.post(this.myAppUrl + 'api/Employee/Create', employee)
      .pipe(map(
        response => {
          return response;
        }));
  }

  updateEmployee(employee: Employee) {
    return this._http.put(this.myAppUrl + 'api/Employee/Edit', employee)
      .pipe(map(
        response => {
          return response;
        }));
  }

  deleteEmployee(id: number): Observable<any> {
    return this._http.delete(this.myAppUrl + 'api/Employee/Delete/' + id)
      .pipe(map(
        response => {
          return response;
        }));
  }


  updateamount(amountupdate: object) {
    console.log("Amoutdetails--", amountupdate);
    return this._http.post(this.myAppUrl + 'api/Employee/updateamount', amountupdate)
      .pipe(map(
        response => {
          return response;
        }));
  }

  InsertLedger(ledger: object) {
    console.log("ledger--", ledger);
    return this._http.post(this.myAppUrl + 'api/Employee/ledgerupdate', ledger)
      .pipe(map(
        response => {
          return response;
        }));
  }


  getuserById(emailId: string) {
    console.log(emailId);
    return this._http.get(this.myAppUrl + 'api/Employee/check/'+ emailId)
      .pipe(map(
        response => {
          return response;
        }));
  }

  getaccountById(id: number) {
    return this._http.get(this.myAppUrl + 'api/Employee/account/' + id)
      .pipe(map(
        response => {
          return response;
        }));
  }


 

  checkmobileno(mobile: string) {
    console.log(mobile);
    return this._http.get(this.myAppUrl + 'api/Employee/verify/'+ mobile)
      .pipe(map(
        response => {
          return response;
        }));
  }
}
