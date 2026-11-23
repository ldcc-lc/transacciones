USE BD_TRANSACCIONES_LDCC;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Cuenta_ObtenerSaldos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.NRO_CUENTA_VC AS NumeroCuenta,
        c.TIPO_CH AS TipoCuenta,
        c.NOMBRE_VC AS Nombre,
        c.SALDO_DE AS Saldo,
        m.CODIGO_CH AS CodigoMoneda,
        c.FECHA_CREACION_DT AS FechaCreacion,
        c.FECHA_MODIFICACION_DT AS FechaModificacion
    FROM dbo.CUENTA AS c
    INNER JOIN dbo.MONEDA AS m
        ON m.PK_ID_MONEDA_IN = c.FK_ID_MONEDA_IN
    ORDER BY c.NRO_CUENTA_VC;
END
GO