USE BD_TRANSACCIONES_LDCC;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Obtener_cuenta_movimientos
(
	@IdCuenta INT
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT 
		m.PK_ID_MOVIMIENTO_IN as Id,
		c.PK_ID_CUENTA_IN as IdCuenta,
		c.NRO_CUENTA_VC as Cuenta,
		m.TIPO_CH as TipoMovimiento,
		m.IMPORTE_DE as Importe,
		m.FECHA_DT as FechaMovimiento
	FROM dbo.MOVIMIENTO as m 
		INNER JOIN dbo.CUENTA as c 
		ON m.FK_ID_CUENTA_IN = c.PK_ID_CUENTA_IN 
	WHERE c.PK_ID_CUENTA_IN = @IdCuenta
	ORDER BY m.FECHA_DT DESC, m.PK_ID_MOVIMIENTO_IN DESC;
END
GO
