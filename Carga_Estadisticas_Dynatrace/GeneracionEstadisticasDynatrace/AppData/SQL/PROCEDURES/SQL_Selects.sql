SELECT Fecha_dato, COUNT(*) AS Cantidad
FROM DatosDynatrace GROUP BY Fecha_dato ORDER BY Fecha_dato DESC;

SELECT Anyo, Semana, COUNT(*) AS Cantidad
FROM DatosDynatraceSemana
GROUP BY Anyo, Semana
ORDER BY Anyo DESC, Semana DESC;



SELECT Anyo, Mes, COUNT(*) AS Cantidad
FROM DatosDynatraceMes
GROUP BY Anyo, Mes
ORDER BY Anyo DESC, Mes ASC;

SELECT COUNT(*) FROM DatosDynatrace;
SELECT COUNT(*) FROM DatosDynatraceSemana;
SELECT COUNT(*) FROM DatosDynatraceMes;

SELECT COUNT(*) FROM WebRequestsDynatrace;
SELECT COUNT(*) FROM WebRequestsDynatraceSemana;


SELECT Anyo, Mes, COUNT(*) AS Cantidad 
FROM WebRequestsDynatraceMes
GROUP BY Anyo, Mes
ORDER BY Anyo DESC, Mes ASC;

-- Date = {05/08/2019 0:00:00} 110961
-- Date = {12/08/2019 0:00:00}

SELECT URI, ROUND(SUM(TasaFallo), 1) AS Fallos, SUM(TasaFallo), SUM(Numero) AS Peticiones
FROM WebRequestsDynatrace d 
WHERE d.Fecha_dato >= '20200201' AND d.Fecha_dato < '20200301' AND URI LIKE '%.svc%'
GROUP BY URI, Fecha_dato

-- var datosBruto = db.DatosDynatrace.Where(p => p.Fecha_dato >= fechaInicial && p.Fecha_dato < fechaFin).GroupBy(s => new { s.Metrica, s.Canal }).ToList();

-- var datosBruto = db.DatosDynatrace.Where(p => p.Fecha_dato >= fechaInicial && p.Fecha_dato < fechaFin).GroupBy(s => new { s.Metrica, s.Canal }).ToList();
SELECT * FROM DatosDynatrace WHERE Fecha_dato >= '20200201' AND Fecha_dato < '20200301';

-- Historificar Mes DatosDynatrace
SELECT 'Febrero' AS Mes, Canal, Metrica, SUM(NumPromedio) AS Volumetria, ROUND(SUM(Promedio)/30, 0) AS 'Promedio (ms)', SUM(Excepciones) AS Excepciones
FROM DatosDynatrace WHERE Fecha_dato >= '20200201' AND Fecha_dato < '20200301'
GROUP BY Canal, Metrica
ORDER BY Metrica;



SELECT Top 100 Metrica, Canal, Excepciones, ROUND(Promedio, 0) AS Promedio, ROUND(Percentil95, 0) AS Percentil95, NumPromedio, NumPercentil FROM DatosDynatrace;

SELECT TOP 100 * FROM DatosDynatrace;
SELECT TOP 100 * FROM DatosDynatraceSemana;
SELECT TOP 100 * FROM DatosDynatraceMes;

-- UPDATE DatosDynatraceMes SET Promedio = ROUND(Promedio, 0), Percentil95 = ROUND(Percentil95, 0);


SELECT * FROM DatosDynatraceSemana WHERE Anyo = 2020 AND Semana = 1; -- 1142




SELECT Metrica, Canal,
	SUM(Excepciones) AS 'Excepciones',
	ROUND(AVG(Promedio), 0) AS 'Promedio AVG (ms)',
	ROUND(AVG(Percentil95), 0) AS 'Percentil96',
	SUM(NumPromedio) AS Volumetria, 
	SUM(NumPercentil) AS 'Percentil (Volumetria)', 
	COUNT(Promedio) AS 'NumDiasActividad',
	1 AS Semana,
	2020 As 'Año'
FROM DatosDynatrace 
WHERE Fecha_dato >= '20191230' AND Fecha_dato <= '20200105'
GROUP BY Canal, Metrica
HAVING COUNT(Promedio) > 0 

SELECT 1 AS ID, Metrica, Canal, 
	SUM(Excepciones) AS 'Excepciones',
	ROUND(AVG(Promedio), 0) AS 'Promedio AVG (ms)',
	ROUND(AVG(Percentil95), 0) AS 'Percentil96',
	SUM(NumPromedio) AS Volumetria, 
	SUM(NumPercentil) AS 'Percentil (Volumetria)', 
	COUNT(Promedio) AS 'NumDiasActividad',
	1 AS Semana,
	2020 As 'Año'
FROM DatosDynatrace 
WHERE Fecha_dato >= '20191230' AND Fecha_dato <= '20200105'
GROUP BY Canal, Metrica
HAVING SUM(NumPromedio) > 0
