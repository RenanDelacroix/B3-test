import { NgModule } from '@angular/core';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms'; // Importe o FormsModule

@NgModule({
  imports: [HttpClientTestingModule, FormsModule],
})
export class HttpClientMockModule { }
