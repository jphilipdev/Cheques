import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'cheques',
    templateUrl: './cheques.component.html',
    styleUrls: ['./cheques.component.css']
})
export class ChequesComponent {
    public name: string;
    public amount: number;
    public date: Date;
    public cheque: Cheque;

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {        
    }

    public generateCheque() {
        var url = 'api/cheques/generate?name=' + this.name + '&amount=' + this.amount + '&date=' + this.date;

        this.http.get(this.baseUrl + url).subscribe(result => {
            this.cheque = result.json() as Cheque;
        }, error => console.error(error));        
    }
}

interface Cheque {
    name: string;
    amount: number;
    amountInWords: string;
    formattedDate: string;
}
