USE GranjaControlDB;
GO

CREATE OR ALTER PROC sp_CrearAdministradorInicial2
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @idRol INT;
	DECLARE @usuarioApp NVARCHAR(25) = 'SISTEMA';

	BEGIN TRY
		BEGIN TRANSACTION;
			-- Obtener el id del rol "Administrador", si no existe, crearlo
			SELECT @idRol = id FROM tblRoles WHERE nombreRol = 'Administrador';

			IF @idRol IS NULL
			BEGIN
				INSERT INTO tblRoles(nombreRol)
				VALUES('Administrador');

				SET @idRol = SCOPE_IDENTITY();
			END

			-- Crear el usuario admin solo si no existe
			IF NOT EXISTS (SELECT 1 FROM tblUsuarios WHERE nombreUsuario = 'AdminMiguelBricen')
			BEGIN
				INSERT INTO tblUsuarios(nombreCompleto, nombreUsuario, correoElectronico, idRol, clave)
				VALUES('Administrador del Sistema',
					   'admin',
					   'admin@gmail.com',
					   @idRol,
					   HASHBYTES('SHA2_256', N'Bricen123'));
			END
		COMMIT TRANSACTION;			
	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;

		DECLARE
			@msg NVARCHAR(MAX) = ERROR_MESSAGE(),
			@num INT = ERROR_NUMBER(),
			@proc NVARCHAR(150) = ERROR_PROCEDURE(),
			@line INT = ERROR_LINE();

		EXEC sp_LogErrores_Insertar @msg, @num, @proc, @usuarioApp, @line;

		THROW;
	END CATCH
END
GO


CREATE OR ALTER PROC sp_MostrarTecnicosActivos2
AS
BEGIN
	SET NOCOUNT ON;	

	SELECT u.id, u.nombreCompleto
	FROM tblUsuarios u
	INNER JOIN tblRoles r ON u.idRol = r.id
	WHERE u.activo = 1
		AND r.activo = 1 
		AND r.nombreRol = 'Técnico'
END
GO


CREATE OR ALTER PROC sp_MostrarUsuarios2
(
	@buscar NVARCHAR(100) = NULL,
	@pagina INT  = 1,
	@tamPagina INT = 10
)
AS
BEGIN
	SET NOCOUNT ON;
	
	IF @pagina < 1 SET @pagina = 1;
    IF @tamPagina < 1 SET @tamPagina = 10;
	
	SELECT u.id AS idUsuario, 
		   r.id AS idRol,
		   u.nombreCompleto,
		   u.nombreUsuario,
		   u.correoElectronico,
		   r.nombreRol,
		   u.activo
	FROM tblUsuarios u
	INNER JOIN tblRoles r ON u.idRol = r.id
	WHERE u.nombreUsuario <> 'admin'
		AND (ISNULL(@buscar, '') = ''
			OR u.nombreCompleto LIKE '%' + @buscar + '%'
			OR u.nombreUsuario LIKE '%' + @buscar + '%'
			OR r.nombreRol LIKE '%' + @buscar + '%')
	ORDER BY u.nombreCompleto
	OFFSET (@pagina - 1) * @tamPagina ROWS
	FETCH NEXT @tamPagina ROWS ONLY;
END
GO


CREATE OR ALTER PROC sp_ContarUsuarios2
(
	@buscar NVARCHAR(100) = NULL
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT COUNT(*) AS Total
	FROM tblUsuarios u
	INNER JOIN tblRoles r ON u.idRol = r.id
	WHERE u.nombreUsuario <> 'admin'
		AND (ISNULL(@buscar,'') = ''
			OR u.nombreCompleto LIKE '%' + @buscar + '%'
			OR u.nombreUsuario LIKE '%' + @buscar + '%'
			OR r.nombreRol LIKE '%' + @buscar + '%');
END
GO


CREATE OR ALTER PROC sp_AutenticarUsuario2
(
	@nombreUsuario NVARCHAR(25),
	@claveUsuario NVARCHAR(MAX)
)
AS
BEGIN
	SET NOCOUNT ON;

	-- Verificar si el usuario existe
	IF NOT EXISTS (
		SELECT 1
		FROM tblUsuarios
		WHERE nombreUsuario = @nombreUsuario
	)
		THROW 51002, 'El usuario no existe', 1;

	-- Verificar si está inactivo
	IF EXISTS (
		SELECT 1
		FROM tblUsuarios
		WHERE nombreUsuario = @nombreUsuario
			AND activo = 0
	)
		THROW 51003, 'El usuario está inactivo', 1;

	-- Verificar contraseña
	IF NOT EXISTS (
		SELECT 1
		FROM tblUsuarios
		WHERE nombreUsuario = @nombreUsuario
			AND clave = HASHBYTES('SHA2_256', @claveUsuario)
	)
		THROW 51004, 'La contraseña es incorrecta', 1;

	-- Si todo esta correcto
	SELECT u.id AS idUsuario,
		   u.nombreCompleto,
		   u.nombreUsuario,
		   r.nombreRol,
		   u.activo,
		   r.id AS idRol
	FROM tblUsuarios u
	INNER JOIN tblRoles r ON u.idRol = r.id
	WHERE u.nombreUsuario = @nombreUsuario
		AND u.clave = HASHBYTES('SHA2_256', @claveUsuario)
END
GO



CREATE OR ALTER PROC sp_ObtenerUsuarioPorId2
(
	@Id INT
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Id,
		   nombreCompleto, 
		   nombreUsuario, 
		   correoElectronico,
		   idRol,
		   activo
	FROM tblUsuarios
	WHERE Id = @Id
END
GO


CREATE OR ALTER PROC sp_AgregarUsuario
(
	@nombreCompleto NVARCHAR(75),
	@nombreUsuario NVARCHAR(25),
	@correoElectronico NVARCHAR(100),
	@idRol INT,
	@claveUsuario NVARCHAR(250),
	@usuarioApp NVARCHAR(25)
)
AS
BEGIN
	SET NOCOUNT ON;

	SET @nombreCompleto = TRIM(@nombreCompleto);
	SET @nombreUsuario = TRIM(@nombreUsuario);
	SET @correoElectronico = TRIM(@correoElectronico);
	SET @usuarioApp = TRIM (@usuarioApp);

	BEGIN TRY
		BEGIN TRANSACTION

			IF EXISTS (
				SELECT 1 
				FROM tblUsuarios 
				WHERE nombreUsuario = @nombreUsuario
			)
				THROW 51001, 'El usuario ya existe', 1;

			IF EXISTS (
				SELECT 1 
				FROM tblUsuarios 
				WHERE correoElectronico = @correoElectronico
			)
				THROW 51005, 'El correo ya existe', 1;

			IF NOT EXISTS (
				SELECT 1 
				FROM tblRoles 
				WHERE id = @idRol
					AND activo = 1
				)
				THROW 51006, 'El rol no existe o está inactivo', 1;

			INSERT INTO tblUsuarios(nombreCompleto, nombreUsuario, correoElectronico, idRol, clave)
			VALUES(@nombreCompleto,
				   @nombreUsuario,
				   @correoElectronico,
				   @idRol,
				   HASHBYTES('SHA2_256', @claveUsuario));

			COMMIT TRANSACTION;
	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;

		DECLARE
			@msg NVARCHAR(MAX) = ERROR_MESSAGE(),
			@num INT = ERROR_NUMBER(),
			@proc NVARCHAR(150) = ERROR_PROCEDURE(),
			@line INT = ERROR_LINE();

		EXEC sp_LogErrores_Insertar @msg, @num, @proc, @usuarioApp, @line;

		THROW;
	END CATCH
END
GO


CREATE OR ALTER PROC sp_ActualizarUsuario2
(
	@idUsuario INT,
	@nombreCompleto NVARCHAR(75),
	@correoElectronico NVARCHAR(100),
	@idRol INT,
	@usuarioApp NVARCHAR(25)
)
AS
BEGIN
	SET NOCOUNT ON;

	SET @nombreCompleto = TRIM(@nombreCompleto);
	SET @correoElectronico = TRIM(@correoElectronico);
	SET @usuarioApp = TRIM (@usuarioApp);

	BEGIN TRY
		BEGIN TRANSACTION

			IF NOT EXISTS (
				SELECT 1 
				FROM tblUsuarios WHERE id = @idUsuario
			)
				THROW 51002, 'El usuario no existe', 1;

			IF EXISTS (
				SELECT 1 
				FROM tblUsuarios 
				WHERE correoElectronico = @correoElectronico 
					AND id <> @idUsuario
			)
				THROW 51005, 'El correo ya existe', 1;

			IF NOT EXISTS (
				SELECT 1 
				FROM tblRoles 
				WHERE id = @idRol
					AND activo = 1
			)
				THROW 51006, 'El rol no existe o está inactivo', 1;

			UPDATE tblUsuarios
			SET nombreCompleto = @nombreCompleto,
				correoElectronico = @correoElectronico,
				idRol = @idRol
			WHERE id = @idUsuario;

		COMMIT TRANSACTION;
	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;

		DECLARE
			@msg NVARCHAR(MAX) = ERROR_MESSAGE(),
			@num INT = ERROR_NUMBER(),
			@proc NVARCHAR(150) = ERROR_PROCEDURE(),
			@line INT = ERROR_LINE();

		EXEC sp_LogErrores_Insertar @msg, @num, @proc, @usuarioApp, @line;

		THROW;
	END CATCH
END
GO


CREATE OR ALTER PROC sp_CambiarClaveUsuario2
(
    @idUsuario INT,
    @claveNueva NVARCHAR(250),
    @usuarioApp NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY

        BEGIN TRANSACTION;

            DECLARE @hashNueva VARBINARY(256);
            SET @hashNueva = HASHBYTES('SHA2_256', @claveNueva);


            IF NOT EXISTS (
                SELECT 1
                FROM tblUsuarios
                WHERE id = @idUsuario
            )
                THROW 51002, 'El usuario no existe', 1;

			IF EXISTS (
				SELECT 1
				FROM tblUsuarios
				WHERE id = @idUsuario
					AND clave = @hashNueva
			)
				THROW 51008, 'La nueva contraseña debe ser diferente a la actual', 1;

            UPDATE tblUsuarios
            SET clave = @hashNueva
            WHERE id = @idUsuario;

        COMMIT TRANSACTION;

    END TRY

    BEGIN CATCH
		IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;

		DECLARE
			@msg NVARCHAR(MAX) = ERROR_MESSAGE(),
			@num INT = ERROR_NUMBER(),
			@proc NVARCHAR(150) = ERROR_PROCEDURE(),
			@line INT = ERROR_LINE();

		EXEC sp_LogErrores_Insertar @msg, @num, @proc, @usuarioApp, @line;

		THROW;
	END CATCH
END
GO


CREATE OR ALTER PROC sp_CambiarEstadoUsuario2
(
	@Id INT,
	@activo BIT,
	@usuarioApp NVARCHAR(25)
)
AS
BEGIN
	SET NOCOUNT ON;

	SET @usuarioApp = TRIM (@usuarioApp);

	BEGIN TRY
		BEGIN TRANSACTION;

			IF NOT EXISTS (
				SELECT 1 
				FROM tblUsuarios 
				WHERE id = @Id
			)
				THROW 51002, 'El usuario no existe', 1;

			IF @activo = 0 
				AND EXISTS (
					SELECT 1 
					FROM tblUsuarios 
					WHERE id = @id
						AND nombreUsuario = 'admin'
				)
				THROW 51008, 'No se puede desactivar el administrador', 1;

			UPDATE tblUsuarios
			SET activo = @activo
			WHERE id = @Id;

		COMMIT TRANSACTION;
	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;

		DECLARE
			@msg NVARCHAR(MAX) = ERROR_MESSAGE(),
			@num INT = ERROR_NUMBER(),
			@proc NVARCHAR(150) = ERROR_PROCEDURE(),
			@line INT = ERROR_LINE();

		EXEC sp_LogErrores_Insertar @msg, @num, @proc, @usuarioApp, @line;

		THROW;
	END CATCH
END
GO