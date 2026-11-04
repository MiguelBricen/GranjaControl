USE GranjaControlDB;
GO


CREATE OR ALTER PROCEDURE sp_LogErrores_Insertar
    @mensajeError NVARCHAR(MAX),
    @numeroError INT,
    @procedimiento NVARCHAR(150),
    @usuarioApp NVARCHAR(25),
    @lineaError INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO tblLogErrores
    (
        mensajeError,
        numeroError,
        procedimiento,
        usuarioApp,
        lineaError
    )
    VALUES
    (
        @mensajeError,
        @numeroError,
        @procedimiento,
        @usuarioApp,
        @lineaError
    );
END
GO