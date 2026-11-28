USE BD_TRANSACCIONES_LDCC;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Cuenta_Crear
(
    @NumeroCuenta NVARCHAR(14),
    @TipoCuenta CHAR(3),
    @Nombre NVARCHAR(40),
    @IdMoneda INT
)

AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.CUENTA
    (
        FK_ID_MONEDA_IN,
        NRO_CUENTA_VC,
        TIPO_CH,
        NOMBRE_VC
    )
    VALUES
    (
        @IdMoneda,
        @NumeroCuenta,
        @TipoCuenta,
        @Nombre
    );

    DECLARE @IdCuenta INT = CONVERT(INT, SCOPE_IDENTITY());

    SELECT
        c.PK_ID_CUENTA_IN AS Id,
        c.FK_ID_MONEDA_IN AS IdMoneda,
        c.NRO_CUENTA_VC AS NumeroCuenta,
        c.TIPO_CH AS TipoCuenta,
        c.NOMBRE_VC AS Nombre,
        c.SALDO_DE AS Saldo,
        m.CODIGO_CH AS CodigoMoneda,
        m.NOMBRE_VC AS NombreMoneda,
        c.FECHA_CREACION_DT AS FechaCreacion,
        c.FECHA_MODIFICACION_DT AS FechaModificacion
    FROM dbo.CUENTA AS c
    INNER JOIN dbo.MONEDA AS m
        ON m.PK_ID_MONEDA_IN = c.FK_ID_MONEDA_IN
    WHERE c.PK_ID_CUENTA_IN = @IdCuenta;
END
GO