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