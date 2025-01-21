import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-form-search',
  standalone: true,
  imports: [],
  templateUrl: './form-search.component.html',
  styleUrl: './form-search.component.css'
})
export class FormSearchComponent implements OnInit{

  constructor(private router:Router){}

  ngOnInit(): void {
    
  }
  search(txt:string,)
  {
   
    this.router.navigate(['home', {query:txt}] );
  }
}
