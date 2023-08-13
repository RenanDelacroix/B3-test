import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: '[appCurrencyFormat]'
})
export class CurrencyFormatDirective {

  constructor(private el: ElementRef) { }

  @HostListener('input', ['$event'])
  onInput(event: any): void {
    const input = event.target;
    let value = input.value.replace(/[^0-9,-]/g, ''); 

    if (value === '' || value === '-') {
      input.value = value;
      return;
    }

    const floatValue = parseFloat(value.replace(',', '.')); 
    const formattedValue = floatValue.toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 });

    input.value = formattedValue; 
  }
}
