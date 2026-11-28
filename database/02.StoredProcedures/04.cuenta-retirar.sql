USE BD_TRANSACCIONES_LDCC;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Cuenta_Retirar
(
	@IdCuenta INT,
	@TipoMovimiento CHAR(1),
	@Importe DECIMAL(18,2)
)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	BEGIN TRY
		BEGIN TRANSACTION;

		--Actualizar Saldo
		UPDATE dbo.Cuenta 
		SET SALDO_DE = SALDO_DE - @Importe,
			FECHA_MODIFICACION_DT = SYSDATETIME()
		WHERE PK_ID_CUENTA_IN = @IdCuenta AND SALDO_DE >= @Importe;

		IF @@ROWCOUNT = 0
        BEGIN
            ;THROW 50004, N'No existe la cuenta indicada.', 1;
        END;

		--Insertar Movimiento
		INSERT INTO dbo.Movimiento
		(
            FK_ID_CUENTA_IN,
            TIPO_CH,
            IMPORTE_DE
		)
		VALUES
		(
			@IdCuenta,
			@TipoMovimiento,
			@Importe
		);
		COMMIT TRANSACTION;
	END TRY
	 BEGIN CATCH
        IF XACT_STATE() <> 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH;
END
GO
