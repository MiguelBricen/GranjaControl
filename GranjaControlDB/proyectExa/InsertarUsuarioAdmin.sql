
USE GranjaControlDB;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @idRol INT;

    -- Obtener el rol Administrador.
    SELECT @idRol = id
    FROM tblRoles
    WHERE nombreRol = 'Administrador';

    -- Crear el rol si no existe.
    IF @idRol IS NULL
    BEGIN
        INSERT INTO tblRoles (nombreRol)
        VALUES ('Administrador');

        SET @idRol = SCOPE_IDENTITY();
    END;

    -- Crear el usuario si no existe.
    IF NOT EXISTS (
        SELECT 1
        FROM tblUsuarios
        WHERE nombreUsuario = 'AdminMiguelBricen'
    )
    BEGIN
        INSERT INTO tblUsuarios
        (
            nombreCompleto,
            nombreUsuario,
            correoElectronico,
            idRol,
            clave
        )
        VALUES
        (
            'Administrador del Sistema',
            'AdminMiguelBricen',
            'admin@gmail.com',
            @idRol,
            HASHBYTES('SHA2_256', N'Bricen123')
        );
    END;

    COMMIT TRANSACTION;

    PRINT 'Proceso completado correctamente.';

END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    PRINT ERROR_MESSAGE();
    THROW;
END CATCH;
GO
