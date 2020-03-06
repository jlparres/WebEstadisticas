--INDICES EN DatosDynatrace
CREATE INDEX IDX_MetricaCanal ON DatosDynatrace (Metrica, Canal);
CREATE INDEX IDX_MetricaCanalFecha ON DatosDynatrace (Metrica, Canal, Fecha_dato);


