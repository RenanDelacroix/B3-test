import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppConfigService } from '../services/app-config.service';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'calculadora-aplicacoes';

  formattedAmount: string = '';
  decimalSeparator: string = ','; 
  term: number = 1;
  netAmountResult: number = 0;
  grossAmountResult: number = 0;
  errorMessage: string = '';
  showErrorMessage: boolean = false;

  constructor(
    private appConfigService: AppConfigService,
    private http: HttpClient,
    private toastr: ToastrService
  ) { }

  calculate(): void {
    let amountWithoutFormat = this.formattedAmount.toString().replace(/\./g, '').replace(',', '.');

    if (isNaN(parseFloat(amountWithoutFormat))) {
      amountWithoutFormat = '0';
    }
    if (isNaN(this.term) || this.term === undefined || this.term === null) {
      this.term = 1;
    }

    this.http
      .get<any>(this.appConfigService.apiCdbUrl + amountWithoutFormat + '/' + this.term)
      .pipe(
        catchError((error) => {
          if (error.status === 400) {
            const responseError = error.error;
            if (responseError && responseError.success === false && responseError.message) {
              this.setErrorMessage(responseError.message);
              this.showErrorMessage = true;
            } else {
              this.toastr.error('Necessário passar valores numéricos validos', 'Erro na requisição:');
            }
          } else {
            this.toastr.error('Ocorreu um erro ao processar a solicitação.', 'Erro');
            this.showErrorMessage = true;
          }
          console.error('Erro na requisição:', error);
          throw new Error('Erro na requisição');
        })
      )
      .subscribe((response) => {
        if (response && response.success) {
          this.netAmountResult = response.data.netAmount;
          this.grossAmountResult = response.data.grossAmount;
        }
      });
  }

  setErrorMessage(message: string): void {
    this.errorMessage = message;
    this.showErrorMessage = true;
    setTimeout(() => {
      this.showErrorMessage = false;
    }, 4000); 
  }

  restrictToNumbers(event: any): void {
    const input = event.target as HTMLInputElement;
    input.value = input.value.replace(/[^0-9]/g, '');
  }

  formatNumber(value: number): string {
    return value.toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
  }

  onKeyDown(event: KeyboardEvent) {
    const allowedKeys = ['Backspace', 'Tab', 'ArrowLeft', 'ArrowRight', 'Delete'];
    const inputElement = event.target as HTMLInputElement;
    const key = event.key;

    if (!allowedKeys.includes(key) && isNaN(parseFloat(key))) {
      event.preventDefault();
    }
  }

  formatInput(event: any): void {
    const input = event.target as HTMLInputElement;
    const unformattedValue = input.value.replace(/[^\d]/g, ''); 

    if (unformattedValue === '') {
      this.formattedAmount = '';
      return;
    }

    const intValue = parseInt(unformattedValue, 10);
    const formattedValue = this.formatNumber(intValue / 100); 
    this.formattedAmount = formattedValue; 
  }

  
}
