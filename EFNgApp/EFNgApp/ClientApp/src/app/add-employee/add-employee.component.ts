import { Component, OnInit } from '@angular/core';
import {  FormGroup,FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService } from '../services/employee.service';
import { City } from 'src/models/city';
import { Employee } from 'src/models/employee';
import { AppState } from '../state/app.state';
import { Store } from '@ngrx/store';
import { User } from 'src/models/User';
import { AddEmployee, EditEmployee } from '../state/actions/employee.actions';


@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css']
})
export class AddEmployeeComponent implements OnInit {

  closeResult: string;
  amountValidation: string;
  amountValidate: string = "0";
  mobileValidate: any;
  mobileValidated: string ="Invalidmobile";
  zeroamount: any;
  tranferForm: FormGroup;
  title = 'Create';
  employeeId: number;
  mobileno: any;
  TransferTo: any;
  transfername: any;
  walletId: any;
  errorMessage: any;
  cityList: City[];
  data = [];
  account = [];

  constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute,
    private _employeeService: EmployeeService, private _router: Router,
    private store: Store<AppState>) {
    if (this._avRoute.snapshot.params['id']) {
      this.employeeId = this._avRoute.snapshot.params['id'];
    }

      
    }
  createForm() {
    this.tranferForm = this._fb.group({
      //employeeId: 0,
      Amount: ['', [Validators.required]],
      Remarks: ['', [Validators.required]],
      Transferto: ['', [Validators.required, Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]],
      ddlwalletId: ['', [Validators.required]],
      //Walletname: ['', [Validators.required]],
    })
  }

  
  ngAfterViewInit() {
   // const getswitchrole = JSON.parse(localStorage.getItem('userdetails'))
   // console.log(getswitchrole.mobile_number);
   // this.mobileno = getswitchrole.mobile_number;
   
  }

  ngOnInit() {
    const getswitchrole = JSON.parse(localStorage.getItem('userdetails'))
    this.mobileno = getswitchrole.mobile_number;
    this.walletId = getswitchrole.first_name + ' ' + getswitchrole.last_name;
    console.log(getswitchrole);
    this.createForm();
    this.mobileValidate = "";
  }

  checkproject(e) {
    this.TransferTo = e.target.value;
    this.transfername = "";
      this._employeeService.checkmobileno(this.TransferTo).subscribe((response: any) => {
        this.data = [response];
        console.log(this.data);
        if (this.data[0] != null) {
          this.transfername = this.data[0].first_name;
          this.mobileValidate = "";
        }
        else {
          console.log("Invalidmobile");
         this.mobileValidate = "Invalidmobile";
        }

      });
    
    console.log(this.TransferTo);

  }

  get name() {
    return this.tranferForm.get('Transferto');
  }

  save() {
    if (!this.tranferForm.valid) {
      
      return;
    }
    console.log("amount", this.tranferForm.value.Amount);
    const amount = this.tranferForm.value.Amount;

    if (amount > 0) {
      const getswitchrole = JSON.parse(localStorage.getItem('userdetails'))
      this._employeeService.getaccountById(getswitchrole.user_id)
        .subscribe((response: User) => {
          const res = response;
          if (res != null) {
            this.account = [res];
           
            if (this.account[0].balance > amount) {

              const amountupdate = {
                "sendAmount": this.account[0].balance,
                "toAmount": amount,
                "senduserId": getswitchrole.user_id,
                "touserId": this.data[0].user_id
              }

              console.log("amountupdate", amountupdate);
              this._employeeService.updateamount(amountupdate)
                .subscribe((response: User) => {
                  const res = response;
                  if (res != null) {
                    var Milliseconds = Date.now();
                    let date = new Date().getTime();
                    var dateInSecs = Math.round(Milliseconds);
                    const ledger = {
                      //"acc_balance":,
                      //"ledger_balance":,
                      "transaction_amt": amount,
                      "transaction_date": date,
                      "transaction_from": getswitchrole.first_name,
                      "transaction_from_id": getswitchrole.user_id,
                      "transaction_reference_number": `Txn${dateInSecs}`,
                      "transaction_remark": this.tranferForm.value.Remarks,
                      "transaction_status": `Success`,
                      "transaction_to": this.data[0].first_name,
                      "transaction_to_id": this.data[0].user_id,
                      "transaction_type": `CREDIT`,
                      "user_id": getswitchrole.user_id
                    }
                    console.log(ledger);

                    //ledger service
                    this._employeeService.InsertLedger(ledger)
                      .subscribe((response) => {
                        const res = response;
                        if (res != null) {
                          console.log("reference", res);
                          localStorage.setItem('reference', JSON.stringify(res));
                          this._router.navigate(['/counter']);
                        }
                        else
                          console.log(res);
                      }, error => console.error(error));
                  }
                  else
                    console.log(res);
                }, error => console.error(error));
            }
            else {
              alert("Insufficient Funds to complete the transaction, please add balance and try again");
              return;
            }
          }
          else
            console.log(res);
        }, error => console.error(error));
    }
    else {
      this.amountValidation = this.tranferForm.value.Amount;
    }
  }

  _keyPress(e) {
    console.log(e.target.value);
    if (e.target.value == 0.00) {
      this.amountValidation = this.tranferForm.value.Amount;
    }
    else {
      this.amountValidation = this.tranferForm.value.Amount;
    }
  }
 
  cancel() {
    this._router.navigate(['']);
  }

  onKey(event: any) {
    console.log(event.target.value);
    const replacezero = event.target.value.replace(".00", "");
    this.zeroamount = replacezero + ".00";

    console.log(this.zeroamount);
  }
  //get name() { return this.employeeForm.get('name'); }
  //get gender() { return this.employeeForm.get('gender'); }
  //get department() { return this.employeeForm.get('department'); }
  //get city() { return this.employeeForm.get('city'); }
}
