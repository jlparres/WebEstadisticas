-- WS8_ObtenerDatosCliente
-- WS32_GetBillingData
-- WS65_ConsultaServicios
-- WS206_GetEstadoCliente
-- WS254_LoginAccount
-- WS316_ObtenerDocumento
-- WS318_ConsultaDocumentos
-- WS423_GetServiceBalanceList
-- UPDATE DatosDynatrace SET Promedio = ROUND(Promedio, 0), Percentil95 = ROUND(Percentil95, 0);
-- UPDATE DatosDynatrace SET Promedio = ROUND(Promedio, 0), Percentil95 = ROUND(Percentil95, 0);


SELECT d.Fecha_dato, d.Metrica, SUM(d.NumPromedio) AS 'Total llamadas', ROUND(AVG(d.Promedio), 0) AS 'Promedio (ms)'
FROM DatosDynatrace d
WHERE d.Fecha_dato = '20200315'
	AND d.Canal NOT LIKE 'TODOS%'
	AND (d.Metrica LIKE 'WS8_ObtenerDatosCliente%' 
	--OR d.Metrica LIKE 'WS32_GetBillingData%' 
	OR d.Metrica LIKE 'WS254_LoginAccount%' 
	--OR d.Metrica LIKE 'WS65_ConsultaServicios%'
	--OR d.Metrica LIKE 'WS206_GetEstadoCliente%' OR d.Metrica LIKE 'WS316_ObtenerDocumento%'
	--OR d.Metrica LIKE 'WS318_ConsultaDocumentos%' OR d.Metrica LIKE 'WS423_GetServiceBalanceList%'
	)
GROUP BY d.Metrica, d.Fecha_dato
ORDER BY d.Metrica

SELECT d.Fecha_dato, SUM(NumPromedio) AS 'Volumetria', AVG(Promedio) AS 'Promedio (ms)'
FROM DatosDynatrace d
WHERE d.Fecha_dato >= '20200301'
	AND d.Canal NOT LIKE 'TODOS%'
	AND d.Metrica LIKE 'WS254_LoginAccount%'
GROUP BY d.Fecha_dato

SELECT d.Fecha_dato, SUM(NumPromedio) AS 'Total llamadas WS' 
FROM DatosDynatrace d
WHERE d.Fecha_dato >= '20200301'
	AND d.Canal LIKE 'TODOS%'
GROUP BY d.Fecha_dato
ORDER BY d.Fecha_dato;

SELECT w.Fecha_dato, SUM(Numero) AS 'Total llamadas'
FROM WebRequestsDynatrace w 
WHERE w.Fecha_dato >= '20200301'
	AND w.URI LIKE '%.svc%'
GROUP BY w.Fecha_dato
ORDER BY w.Fecha_dato;

