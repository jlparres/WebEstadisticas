-- COMPROBAR FECHA, SEMANA y MESES
SELECT Fecha_dato, COUNT(*) AS Cantidad 
FROM DatosDynatrace 
GROUP BY Fecha_dato
ORDER BY Fecha_dato DESC;

SELECT Fecha_dato, COUNT(*) AS Cantidad
FROM WebRequestsDynatrace
GROUP BY Fecha_dato
ORDER BY Fecha_dato DESC;

-- SEMANA
SELECT Anyo, Semana, COUNT(*) AS Cantidad
FROM DatosDynatraceSemana
GROUP BY Anyo, Semana
ORDER BY Anyo DESC, Semana DESC;

SELECT Anyo, Semana, COUNT(*) AS Cantidad
FROM WebRequestsDynatraceSemana
GROUP BY Anyo, Semana
ORDER BY Anyo DESC, Semana DESC;

-- MES
SELECT Anyo, Mes, COUNT(*) AS Cantidad
FROM DatosDynatraceMes
GROUP BY Anyo, Mes
ORDER BY Anyo DESC, Mes DESC;

SELECT Anyo, Mes, COUNT(*) AS Cantidad
FROM WebRequestsDynatraceMes
GROUP BY Anyo, Mes
ORDER BY Anyo, Mes DESC;



SELECT Metrica, Canal, COUNT(*) AS Cantidad
FROM DatosDynatrace 
WHERE Fecha_dato >= '20181112' AND Fecha_dato <= '20181118' 
	AND Metrica LIKE 'WS154%'
GROUP BY Metrica, Canal
ORDER BY Metrica, Canal;

SELECT * FROM DatosDynatraceSemana WHERE Metrica = 'WS10_ObtenerDisponibilidadCita                    ';


SELECT Metrica, Canal
FROM DatosDynatraceSemana 
WHERE Semana = 45 AND Anyo = 2018
ORDER BY Metrica, Canal;


SELECT Mes, Anyo, COUNT(*) AS Cantidad
FROM DatosDynatraceMes
GROUP BY Mes, Anyo
ORDER BY Anyo, Mes;

SELECT * FROM [WS-END];


SELECT Fecha_dato, COUNT(*) AS Cantidad 
FROM DatosDynatrace 
WHERE Fecha_dato >= GETDATE()-7
	AND Canal = 'TODOS'
GROUP BY Fecha_dato
ORDER BY Fecha_dato DESC;



select * from DatosDynatrace;

SELECT Fecha_dato, Metrica, SUM(Promedio) AS Promedio
FROM DatosDynatrace
WHERE Metrica LIKE '%WS8_Obtener%'
	AND Fecha_dato >= '01/11/2018' 
	AND Fecha_dato <= '31/12/2018'
GROUP BY Fecha_dato, Metrica;

SELECT * 
FROM DatosDynatrace 
WHERE Fecha_dato = '01/11/2018' AND Metrica LIKE '%WS8_Obtener%'


-- VOLUMETRIA POR CANAL
SELECT Fecha_dato, COUNT(*) AS Cantidad 
FROM DatosDynatrace 
GROUP BY Fecha_dato
ORDER BY Fecha_dato ASC;

SELECT Fecha_dato AS Dia, Canal, Metrica AS WebService, NumPromedio
FROM DatosDynatrace
WHERE Canal = 'IIS-DXL                       ' AND Metrica = 'WS254_LoginAccount                                '
	AND Fecha_dato >= '20200101' --AND Fecha_dato < '20200201'
GROUP BY Canal, Metrica, Fecha_dato, NumPromedio
ORDER BY Fecha_dato ASC;
