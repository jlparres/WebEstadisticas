--TRUNCATE TABLE DatosDynatrace;
--TRUNCATE TABLE DatosDynatraceSemana;

SELECT Fecha_dato, COUNT(*) AS Cantidad 
FROM DatosDynatrace 
GROUP BY Fecha_dato
ORDER BY Fecha_dato ASC;


SELECT Anyo, Semana, COUNT(*) AS Cantidad
FROM DatosDynatraceSemana
GROUP BY Anyo, Semana
ORDER BY Anyo, Semana ASC;

SELECT * FROM DatosDynatrace;
SELECT * FROM DatosDynatraceMes;
SELECT * FROM DatosDynatraceSemana;

SELECT * FROM DatosDynatraceSemana WHERE Metrica = 'WS10_ObtenerDisponibilidadCita                    ';


SELECT MAX(Anyo) AS Año, Max(Semana) AS Semana
FROM DatosDynatraceSemana;

SELECT * FROM DatosDynatrace;

SELECT * FROM DatosDynatrace WHERE Fecha_dato >= '20180801' AND Fecha_dato <= '20180831';


SELECT Mes, Anyo, COUNT(*) AS Cantidad
FROM DatosDynatraceMes
GROUP BY Mes, Anyo
ORDER BY Anyo, Mes;

-- TRUNCATE TABLE DatosDynatraceSemana;

-- CREATE UNIQUE INDEX IDX_MetricaCanalFecha ON DatosDynatrace (Metrica ASC, Canal ASC, Fecha_dato DESC);

SELECT * FROM DatosDynatrace;

SELECT d.Fecha_dato, COUNT(*) AS Registros FROM DatosDynatrace d GROUP BY d.Fecha_dato ORDER BY d.Fecha_dato DESC;

SELECT * FROM DatosDynatrace WHERE Fecha_dato = '20181202';
SELECT * FROM DatosDynatrace WHERE Fecha_dato = '20180921';

-- var datosBruto = db.DatosDynatrace.Where(p => p.Fecha_dato >= fechaInicial && p.Fecha_dato < fechaFin).GroupBy(s => new { s.Metrica, s.Canal }).ToList();

SELECT Metrica, Canal, SUM(Excepciones) AS Excepciones, SUM(Promedio) AS Promedio, SUM(Percentil95) AS Percentil95, SUM(NumPromedio) AS NumPromedio, SUM(NumPercentil) AS NumPercentil
FROM DatosDynatrace WHERE Fecha_dato > '20181225' AND Fecha_dato < '20181231'
GROUP BY Metrica, Canal

-- DELETE FROM DatosDynatrace WHERE Fecha_dato = '20181202'

SELECT Metrica FROM DatosDynatrace GROUP BY Metrica ORDER BY Metrica ASC;
SELECT Canal FROM DatosDynatrace GROUP BY Canal ORDER BY Canal ASC;
