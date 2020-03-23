export interface IDatosDynatrace {
  id: number;
  metrica: string;
  canal: string;
  excepciones: number;
  promedio: number;
  percentil95: number;
  numPromedio: number;
  numPercentil: number;
  fecha_dato: Date;
}

export interface IDatosDynatraceMes {
  id: number;
  metrica: string;
  canal: string;
  excepciones: number;
  promedio: number;
  percentil95: number;
  numPromedio: number;
  numPercentil: number;
  numDiasActividad: number;
  mes: number;
  anyo: number;
}

export interface IDatosDynatraceSemana {
  id: number;
  metrica: string;
  canal: string;
  excepciones: number;
  promedio: number;
  percentil95: number;
  numPromedio: number;
  numPercentil: number;
  numDiasActividad: number;
  semana: number;
  anyo: number;
}

export interface IEstadisticas {
  fecha: Date;
  volumeetria: number;
  promedio: number;
  percentil: number;
  percentil95: number;
  excepciones: number;
  canal: string;
  metrica: string;
}

