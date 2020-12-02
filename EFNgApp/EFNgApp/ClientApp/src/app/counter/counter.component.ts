import { Component, OnInit, AfterViewInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  referncenum: string;
  Date: any;
  constructor(private _router: Router) {
   
  }

  public ok() {
    this._router.navigate(['']);
  }
  ngOnInit() {
    const getrefernce = JSON.parse(localStorage.getItem('reference'));
    console.log(getrefernce);
    console.log(getrefernce[0]);
    this.referncenum = getrefernce.transaction_reference_number;
    this.Date = getrefernce.transaction_date;
  }

}
