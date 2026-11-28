USE BD_TRANSACCIONES_LDCC;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Cuentas_transaccion
(
	@IdCuentaOrigen INT,
	@IdCuentaDestino INT,
	@Importe DECIMAL(18,2)
)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	BEGIN TRY
		BEGIN TRANSACTION;
		-- Cuenta de origen (Descontar)
		UPDATE dbo.CUENTA
		SET SALDO_DE = SALDO_DE - @Importe,
			FECHA_MODIFICACION_DT = SYSDATETIME()
		WHERE PK_ID_CUENTA_IN = @IdCuentaOrigen
			AND SALDO_DE >= @Importe;
		
		 IF @@ROWCOUNT = 0
        BEGIN
            ;THROW 50015, N'La cuenta de origen no tiene saldo suficiente.', 1;
        END;

		-- Cuenta de destino (Incrementar)
		UPDATE dbo.CUENTA
		SET SALDO_DE = SALDO_DE + @Importe,
			FECHA_MODIFICACION_DT = SYSDATETIME()
		WHERE PK_ID_CUENTA_IN = @IdCuentaDestino;

		IF @@ROWCOUNT = 0
			BEGIN
				;THROW 50016, N'No se pudo actualizar la cuenta d
				e destino.', 1;
			END;

		-- Registro en Movimiento
		INSERT INTO dbo.MOVIMIENTO
		(
			FK_ID_CUENTA_IN,
			TIPO_CH,
			IMPORTE_DE
		)
		VALUES
			(@IdCuentaOrigen, 'D', @Importe),
			(@IdCuentaDestino, 'A', @Importe);
		COMMIT TRANSACTION;
	END TRY
	 BEGIN CATCH
        IF XACT_STATE() <> 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH;
END
GO
