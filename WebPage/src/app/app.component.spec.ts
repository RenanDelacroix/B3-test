import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ToastrModule } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';

describe('AppComponent', () => {
  let fixture: ComponentFixture<AppComponent>;
  let component: AppComponent;
  let httpTestingController: HttpTestingController;

  beforeEach(
    waitForAsync(() => {
      TestBed.configureTestingModule({
        declarations: [AppComponent],
        imports: [
          RouterTestingModule,
          HttpClientTestingModule,
          ToastrModule.forRoot(),
          FormsModule, 
        ],
      }).compileComponents();
    })
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    httpTestingController = TestBed.inject(HttpTestingController);
    fixture.detectChanges();
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should create the app', () => {
    expect(component).toBeTruthy();
  });

  it('should calculate net and gross amounts', () => {
    // Coloca valores nos inputs
    component.formattedAmount = '1000,00';
    component.term = 12;

    // Mock do retorno da Api
    const mockResponse = {
      success: true,
      data: {
        netAmount: 950,
        grossAmount: 1100,
      },
    };

    component.calculate();

    const formattedAmountForUrl = component.formattedAmount.replace(',', '.');
    const termForUrl = component.term.toString();

    const req = httpTestingController.expectOne(`https://localhost:7217/api/v1/Calculator/cdb/${formattedAmountForUrl}/${termForUrl}`);
    expect(req.request.method).toBe('GET');

    // resposta com mock
    req.flush(mockResponse);

    // verificar resultados
    expect(component.netAmountResult).toBe(950);
    expect(component.grossAmountResult).toBe(1100);
  });

});
