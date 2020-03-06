import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EstadisticasFiltroComponent } from './estadisticas-filtro.component';

describe('EstadisticasFiltroComponent', () => {
  let component: EstadisticasFiltroComponent;
  let fixture: ComponentFixture<EstadisticasFiltroComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EstadisticasFiltroComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EstadisticasFiltroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
