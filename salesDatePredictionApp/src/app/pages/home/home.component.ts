import { Component } from '@angular/core';
import { DatePredictionComponent } from '../../Components/date-prediction/date-prediction.component';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [ DatePredictionComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}
