import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService } from '../services/employee.service';
import { User } from 'src/models/User';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {

  loginForm: FormGroup;
  emailId: string;
  message: any;
  ifmessage: string="Invalid User";
  constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute,
    private _employeeService: EmployeeService, private _router: Router
  ) {

  }

  ngOnInit() {
    let emailregex: RegExp = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    this.loginForm = this._fb.group({
      name: ['', [Validators.required, Validators.pattern(emailregex)]],
      password: ['', [Validators.required]]
    })

  }

  getErrorEmail() {
    return this.loginForm.get('email').hasError('required') ? 'Field is required' :
      this.loginForm.get('email').hasError('pattern') ? 'Not a valid emailaddress' :
        this.loginForm.get('email').hasError('alreadyInUse') ? 'This emailaddress is already in use' : '';
  }

  Login() {

    if (!this.loginForm.valid) {
      console.log("TEST");
      return;
    }
    console.log(this.loginForm.value.name);
    this.emailId = this.loginForm.value.name;
    this._employeeService.getuserById(this.emailId)
      .subscribe((response: User) => {
        const res = response;
        if (res != null) {
          localStorage.setItem('userdetails', JSON.stringify(res));
          this._router.navigate(['/register-employee']);
        }
        else {
          this.message = "Invalid User";
          console.log("Invalid User");
          //alert(this.message);
        }

      }, error => console.error(error));
  }

    
  

}
