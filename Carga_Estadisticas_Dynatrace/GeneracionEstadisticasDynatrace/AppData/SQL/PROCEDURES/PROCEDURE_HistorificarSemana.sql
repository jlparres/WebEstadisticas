ALTER PROCEDURE HistorificarSemana @FechaIni DATETIME, @FechaFin DATETIME, @Semana INTEGER, @Anyo INTEGER
AS 

-- Historificando Semana de DatosDynatrace
SELECT @Anyo, @Semana, Metrica, Canal, SUM(NumPromedio) AS Volumetria, SUM(NumPercentil) AS 'Percentil (Volumetria)', 
	ROUND(AVG(Promedio), 0) AS 'Promedio AVG (ms)',
	SUM(Excepciones) AS 'Excepciones',
	COUNT(Promedio) AS 'NumDiasActividad'
FROM DatosDynatrace 
WHERE Fecha_dato >= @FechaIni AND Fecha_dato < @FechaFin
GROUP BY Canal, Metrica
HAVING COUNT(Promedio) > 0 
ORDER BY Metrica, 'Promedio AVG (ms)' DESC



-- Historificando Semana de WebRequest
SELECT @Anyo, @Semana, URI, Canal, ROUND(AVG(TasaFallo), 3) AS 'Tasa fallo', SUM(Numero) AS 'Peticiones'
FROM WebRequestsDynatrace
WHERE (URI LIKE '%.svc%' OR URI LIKE '%.asmx%') AND Fecha_dato >= @FechaIni AND Fecha_dato < @FechaFin
GROUP BY URI, Canal
HAVING COUNT(Numero) > 0
ORDER BY URI, Canal;

GO
