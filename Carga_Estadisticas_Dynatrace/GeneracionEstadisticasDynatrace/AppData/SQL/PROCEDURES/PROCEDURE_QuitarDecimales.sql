CREATE PROCEDURE QuitarDecimales
AS 
	UPDATE DatosDynatrace SET Promedio = ROUND(Promedio, 0), Percentil95 = ROUND(Percentil95, 0);
	UPDATE DatosDynatraceSemana SET Promedio = ROUND(Promedio, 0), Percentil95 = ROUND(Percentil95, 0);
	UPDATE DatosDynatraceMes SET Promedio = ROUND(Promedio, 0), Percentil95 = ROUND(Percentil95, 0);
GO