USE [DATA_DYNA]
GO


EXEC	[dbo].[HistorificarSemana]
		@FechaIni = N'20200101',
		@FechaFin = N'20200201',
		@Semana = null,
		@Anyo = null
GO

-- Insertar datos en DatosDynatraceSemanal
SELECT Metrica, Canal, SUM(NumPromedio) AS Volumetria, SUM(NumPercentil) AS 'Percentil (Volumetria)', 
	-- ROUND(SUM(Promedio)/(COUNT(*)*7), 0) AS 'Promedio (ms)', 
	ROUND(AVG(Promedio), 0) AS 'Promedio AVG (ms)',
	SUM(Excepciones) AS 'Excepciones',
	COUNT(Promedio) AS 'NumDiasActividad'
FROM DatosDynatrace 
WHERE Fecha_dato >= '20200106' AND Fecha_dato < '20200113'
	-- AND Metrica = 'WS8_ObtenerDatosCliente                           '
GROUP BY Canal, Metrica
HAVING COUNT(Promedio) > 0 
ORDER BY Metrica, 'Promedio AVG (ms)' DESC;

-- Insertar datos en DatosDynatraceMes
SELECT Metrica, Canal, SUM(NumPromedio) AS Volumetria, SUM(NumPercentil) AS 'Percentil (Volumetria)', 
	-- ROUND(SUM(Promedio)/(COUNT(*)*7), 0) AS 'Promedio (ms)', 
	ROUND(AVG(Promedio), 0) AS 'Promedio AVG (ms)',
	SUM(Excepciones) AS 'Excepciones',
	COUNT(Promedio) AS 'NumDiasActividad'
FROM DatosDynatrace 
WHERE Fecha_dato >= '20200106' AND Fecha_dato < '20200113' 
	-- AND Metrica = 'WS8_ObtenerDatosCliente                           '
GROUP BY Canal, Metrica
HAVING COUNT(Promedio) > 0 
ORDER BY Metrica, 'Promedio AVG (ms)' DESC;


SELECT TOP 100 * FROM WebRequestsDynatrace;


SELECT URI, Canal, ROUND(AVG(TasaFallo), 3) AS 'Tasa fallo', SUM(Numero) AS 'Peticiones', COUNT(Numero) AS 'NumActividad'
FROM WebRequestsDynatrace
WHERE (URI LIKE '%.svc%' OR URI LIKE '%.asmx%') AND Fecha_dato >= '20200101' AND Fecha_dato < '20200201'
GROUP BY URI, Canal
HAVING COUNT(Numero) > 0
ORDER BY URI, Canal;

SELECT URI, COUNT(*) AS Num FROM WebRequestsDynatrace 
WHERE URI NOT LIKE '%.svc%' AND URI NOT LIKE '%.asmx%'
GROUP BY URI;




