SELECT Fecha_dato, COUNT(*) AS Cantidad
FROM DatosDynatrace GROUP BY Fecha_dato ORDER BY Fecha_dato DESC;

SELECT Anyo, Mes, COUNT(*) AS Cantidad
FROM DatosDynatraceMes
GROUP BY Anyo, Mes
ORDER BY Anyo, Mes DESC;

SELECT Anyo, Semana, COUNT(*) AS Cantidad
FROM DatosDynatraceSemana
GROUP BY Anyo, Semana
ORDER BY Anyo, Semana DESC;

