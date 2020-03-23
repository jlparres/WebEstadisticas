ALTER PROCEDURE HistorificarMes @FechaIni DATETIME, @FechaFin DATETIME, @Mes INTEGER, @Anyo INTEGER
AS 

-- Historificar Semana
SELECT @Anyo, @Mes, Metrica, Canal, SUM(NumPromedio) AS Volumetria, SUM(NumPercentil) AS 'Percentil (Volumetria)', 
	ROUND(AVG(Promedio), 0) AS 'Promedio AVG (ms)',
	SUM(Excepciones) AS 'Excepciones',
	COUNT(Promedio) AS 'NumDiasActividad'
FROM DatosDynatrace 
WHERE Fecha_dato >= @FechaIni AND Fecha_dato < @FechaFin
GROUP BY Canal, Metrica
HAVING COUNT(Promedio) > 0 
ORDER BY Metrica, 'Promedio AVG (ms)' DESC;

-- Historificando Mes de WebRequest
SELECT @Anyo, @Mes, URI, Canal, ROUND(AVG(TasaFallo), 3) AS 'Tasa fallo', SUM(Numero) AS 'Peticiones'
FROM WebRequestsDynatrace
WHERE (URI LIKE '%.svc%' OR URI LIKE '%.asmx%') AND Fecha_dato >= @FechaIni AND Fecha_dato < @FechaFin
GROUP BY URI, Canal
HAVING COUNT(Numero) > 0
ORDER BY URI, Canal;

GO
