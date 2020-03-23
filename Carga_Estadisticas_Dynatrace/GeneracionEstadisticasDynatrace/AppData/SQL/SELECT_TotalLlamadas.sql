-- Obtener Numero Total de llamadas de los WS de Pedro

DECLARE @cols AS NVARCHAR(MAX),
    @query  AS NVARCHAR(MAX);

SET @cols = STUFF((SELECT distinct ',' + QUOTENAME(d.Metrica)
            FROM DatosDynatrace d WHERE (d.Metrica LIKE 'WS8_ObtenerDatosCliente%' OR d.Metrica LIKE 'WS32_GetBillingData%' 
	OR d.Metrica LIKE 'WS254_LoginAccount%' OR d.Metrica LIKE 'WS65_ConsultaServicios%'
	OR d.Metrica LIKE 'WS206_GetEstadoCliente%' OR d.Metrica LIKE 'WS316_ObtenerDocumento%'
	OR d.Metrica LIKE 'WS318_ConsultaDocumentos%' OR d.Metrica LIKE 'WS423_GetServiceBalanceList%')
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,1,'')

set @query = 'SELECT Fecha_dato, ' + @cols + ' from 
            (
                SELECT Fecha_dato
                    , NumPromedio
                    , Metrica
                from DatosDynatrace d WHERE d.Fecha_dato >= ''20200301''
				AND d.Canal LIKE ''TODOS%''
				AND (d.Metrica LIKE ''WS8_ObtenerDatosCliente%'' OR d.Metrica LIKE ''WS32_GetBillingData%'' 
	OR d.Metrica LIKE ''WS254_LoginAccount%'' OR d.Metrica LIKE ''WS65_ConsultaServicios%''
	OR d.Metrica LIKE ''WS206_GetEstadoCliente%'' OR d.Metrica LIKE ''WS316_ObtenerDocumento%''
	OR d.Metrica LIKE ''WS318_ConsultaDocumentos%'' OR d.Metrica LIKE ''WS423_GetServiceBalanceList%'')
           ) x
            pivot 
            (
                SUM(NumPromedio)
                for Metrica in (' + @cols + ')
            ) p ORDER BY Fecha_dato'

execute(@query)