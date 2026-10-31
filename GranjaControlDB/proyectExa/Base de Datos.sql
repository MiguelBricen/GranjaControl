USE [master];
GO


--> 1. Validar si la base de datos existe
IF DB_ID('GranjaControlDB') IS NOT NULL
BEGIN
	ALTER DATABASE GranjaControlDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE GranjaControlDB;
END
GO


--> 2. Crear la base de datos
CREATE DATABASE GranjaControlDB;
GO


--> 3. Usar la base de datos
USE GranjaControlDB;
GO


--> 4. Crear tabla Roles

CREATE TABLE tblRoles
(
	id INT IDENTITY(1,1) NOT NULL,
	nombreRol NVARCHAR(50) NOT NULL,
	activo BIT NOT NULL DEFAULT 1,

	CONSTRAINT PK_tblRoles PRIMARY KEY (id),
	CONSTRAINT CK_tblRoles_nombreRol CHECK(nombreRol IN ('Administrador', 'Usuario')),
	CONSTRAINT UQ_tblRoles_nombreRol UNIQUE (nombreRol)
);
GO

--> Crear tabla Usuarios
CREATE TABLE tblUsuarios
(
	id INT IDENTITY(1,1) NOT NULL,
	nombreCompleto NVARCHAR(75) NOT NULL,
	nombreUsuario NVARCHAR(25) NOT NULL,
	correoElectronico NVARCHAR(100) NOT NULL,
	idRol INT NOT NULL,
	clave VARBINARY(MAX) NOT NULL,
	activo BIT NOT NULL DEFAULT 1,
	fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),

	CONSTRAINT PK_tblUsuarios PRIMARY KEY (id),
	CONSTRAINT UQ_tblUsuarios_nombreUsuario UNIQUE (nombreUsuario),
	CONSTRAINT UQ_tblUsuarios_correoElectronico UNIQUE (correoElectronico),
	CONSTRAINT FK_tblUsuarios_tblRoles FOREIGN KEY (idRol) REFERENCES tblRoles(id)
);
GO

CREATE INDEX IX_tblUsuarios_idRol ON tblUsuarios(idRol) WITH (FILLFACTOR = 80);
GO

--> Crear tabla Galpones
CREATE TABLE tblGalpones
(
	id INT IDENTITY(1,1) NOT NULL,
	nombreGalpon NVARCHAR(75) NOT NULL,
	capacidad INT NOT NULL,
	descripcion NVARCHAR(250) NULL,
	activo BIT NOT NULL DEFAULT 1,

	CONSTRAINT PK_tblGalpones PRIMARY KEY (id),
	CONSTRAINT UQ_tblGalpones_nombreGalpon UNIQUE (nombreGalpon),
	CONSTRAINT CK_tblGalpones_capacidad CHECK (capacidad > 0)
);
GO

--> Crear tabla Proveedores
CREATE TABLE tblProveedores
(
	id INT IDENTITY(1,1) NOT NULL,
	nombreProveedor NVARCHAR(100) NOT NULL,
	telefono NVARCHAR(25) NULL,
	correoElectronico NVARCHAR(100) NULL,
	direccion NVARCHAR(200) NULL,
	tipoProveedor NVARCHAR(30) NOT NULL,
	activo BIT NOT NULL DEFAULT 1,

	CONSTRAINT PK_tblProveedores PRIMARY KEY (id),
	CONSTRAINT UQ_tblProveedores_nombreProveedor UNIQUE (nombreProveedor),
	CONSTRAINT CK_tblProveedores_tipoProveedor CHECK(tipoProveedor IN ('Pollitos', 'Alimento', 'Ambos'))
);
GO

--> Crear tabla Lotes
CREATE TABLE tblLotes
(
	id INT IDENTITY(1,1) NOT NULL,
	codigoLote NVARCHAR(25) NOT NULL,
	idGalpon INT NOT NULL,
	idProveedor INT NOT NULL,
	fechaIngreso DATETIME NOT NULL DEFAULT GETDATE(),
	cantidadInicial INT NOT NULL,
	costoPorPollito DECIMAL(10,2) NOT NULL,
	estado NVARCHAR(25) NOT NULL DEFAULT 'En producción',
	observacion NVARCHAR(250) NULL,

	CONSTRAINT PK_tblLotes PRIMARY KEY (id),
	CONSTRAINT UQ_tblLotes_codigoLote UNIQUE (codigoLote),

	CONSTRAINT FK_tblLotes_tblGalpones FOREIGN KEY (idGalpon) REFERENCES tblGalpones(id),
	CONSTRAINT FK_tblLotes_tblProveedores FOREIGN KEY (idProveedor) REFERENCES tblProveedores(id),

	CONSTRAINT CK_tblLotes_cantidadInicial CHECK(cantidadInicial > 0),
	CONSTRAINT CK_tblLotes_costoPorPollito CHECK(costoPorPollito >= 0),
	CONSTRAINT CK_tblLotes_estado CHECK(estado IN('En producción', 'Listo para venta', 'Finalizado', 'Cancelado'))
);
GO

CREATE INDEX IX_tblLotes_idGalpon ON tblLotes(idGalpon) WITH (FILLFACTOR = 80);
CREATE INDEX IX_tblLotes_idProveedor ON tblLotes(idProveedor) WITH (FILLFACTOR = 80);
CREATE INDEX IX_tblLotes_estado ON tblLotes(estado) WITH (FILLFACTOR = 80);
CREATE INDEX IX_tblLotes_fechaIngreso ON tblLotes(fechaIngreso) WITH (FILLFACTOR = 80);
GO

--> Crear tabla Alimentos
CREATE TABLE tblAlimentos
(
	id INT IDENTITY(1,1) NOT NULL,
	nombreAlimento NVARCHAR(100) NOT NULL,
	tipoAlimento NVARCHAR(20) NOT NULL,
	descripcion NVARCHAR(250) NULL,
	activo BIT NOT NULL DEFAULT 1,

	CONSTRAINT PK_tblAlimentos PRIMARY KEY (id),
	CONSTRAINT UQ_tblAlimentos_nombreAlimento UNIQUE (nombreAlimento),
	CONSTRAINT CK_tblAlimentos_tipoAlimento CHECK(tipoAlimento IN ('Inicio', 'Finalizador'))
);
GO

--> Crear tabla Alimentaciones
CREATE TABLE tblAlimentaciones
(
	id INT IDENTITY(1,1) NOT NULL,
	idLote INT NOT NULL,
	idAlimento INT NOT NULL,
	fecha DATE NOT NULL,
	cantidad DECIMAL(10,2) NOT NULL,
	unidadMedida NVARCHAR(20) NOT NULL,
	observacion NVARCHAR(250) NULL,

	CONSTRAINT PK_tblAlimentaciones PRIMARY KEY (id),

	CONSTRAINT FK_tblAlimentaciones_tblLotes FOREIGN KEY (idLote) REFERENCES tblLotes(id),
	CONSTRAINT FK_tblAlimentaciones_tblAlimentos FOREIGN KEY (idAlimento) REFERENCES tblAlimentos(id),

	CONSTRAINT CK_tblAlimentaciones_cantidad CHECK(cantidad > 0),
	CONSTRAINT CK_tblAlimentaciones_unidadMedida CHECK(unidadMedida IN ('Libras', 'Kilogramos'))
);
GO

CREATE INDEX IX_tblAlimentaciones_idLote ON tblAlimentaciones(idLote) WITH (FILLFACTOR = 80);
CREATE INDEX IX_tblAlimentaciones_idAlimento ON tblAlimentaciones(idAlimento) WITH (FILLFACTOR = 80);
CREATE INDEX IX_tblAlimentaciones_fecha ON tblAlimentaciones(fecha) WITH (FILLFACTOR = 80);
GO

--> Crear tabla Vitaminas
CREATE TABLE tblVitaminas
(
	id INT IDENTITY(1,1) NOT NULL,
	nombreVitamina NVARCHAR(100) NOT NULL,
	descripcion NVARCHAR(250) NULL,
	activo BIT NOT NULL DEFAULT 1,

	CONSTRAINT PK_tblVitaminas PRIMARY KEY (id),
	CONSTRAINT UQ_tblVitaminas_nombreVitamina UNIQUE (nombreVitamina)
);
GO

--> Crear tabla Apliaciones de vitaminas
CREATE TABLE tblAplicacionesVitaminas
(
	id INT IDENTITY(1,1) NOT NULL,
	idLote INT NOT NULL,
	idVitamina INT NOT NULL,
	fecha DATETIME NOT NULL DEFAULT GETDATE(),
	cantidad DECIMAL(10,2) NOT NULL,
	unidadMedida NVARCHAR(20) NOT NULL,
	observacion NVARCHAR(250) NULL,

	CONSTRAINT PK_tblAplicacionesVitaminas PRIMARY KEY (id),

	CONSTRAINT FK_tblAplicacionesVitaminas_tblLotes	FOREIGN KEY (idLote) REFERENCES tblLotes(id),
	CONSTRAINT FK_tblAplicacionesVitaminas_tblVitaminas FOREIGN KEY (idVitamina) REFERENCES tblVitaminas(id),

	CONSTRAINT CK_tblAplicacionesVitaminas_cantidad CHECK(cantidad > 0),
	CONSTRAINT CK_tblAplicacionesVitaminas_unidadMedida CHECK(unidadMedida IN ('Mililitros', 'Litros', 'Gramos'))
);
GO

CREATE INDEX IX_tblAplicacionesVitaminas_idLote ON tblAplicacionesVitaminas(idLote) WITH (FILLFACTOR = 80);
CREATE INDEX IX_tblAplicacionesVitaminas_idVitamina ON tblAplicacionesVitaminas(idVitamina) WITH (FILLFACTOR = 80);
CREATE INDEX IX_tblAplicacionesVitaminas_fecha ON tblAplicacionesVitaminas(fecha) WITH (FILLFACTOR = 80);
GO

--> Crear tabla Mortalidades
CREATE TABLE tblMortalidades
(
	id INT IDENTITY(1,1) NOT NULL,
	idLote INT NOT NULL,
	fecha DATETIME NOT NULL DEFAULT GETDATE(),
	cantidad INT NOT NULL,
	causa NVARCHAR(150) NOT NULL,
	observacion NVARCHAR(250) NULL,

	CONSTRAINT PK_tblMortalidades PRIMARY KEY (id),

	CONSTRAINT FK_tblMortalidades_tblLotes FOREIGN KEY (idLote) REFERENCES tblLotes(id),

	CONSTRAINT CK_tblMortalidades_cantidad CHECK(cantidad > 0)
);
GO

CREATE INDEX IX_tblMortalidades_idLote ON tblMortalidades(idLote) WITH (FILLFACTOR = 80);
CREATE INDEX IX_tblMortalidades_fecha ON tblMortalidades(fecha) WITH (FILLFACTOR = 80);
GO

--> Crear tabla Compras de alimento
CREATE TABLE tblComprasAlimento
(
	id INT IDENTITY(1,1) NOT NULL,
	numeroCompra NVARCHAR(25) NOT NULL,
	idProveedor INT NOT NULL,
	fechaCompra DATETIME NOT NULL DEFAULT GETDATE(),
	observacion NVARCHAR(250) NULL,

	CONSTRAINT PK_tblComprasAlimento PRIMARY KEY (id),
	CONSTRAINT UQ_tblComprasAlimento_numeroCompra UNIQUE (numeroCompra),

	CONSTRAINT FK_tblComprasAlimento_tblProveedores FOREIGN KEY (idProveedor) REFERENCES tblProveedores(id)
);
GO

CREATE INDEX IX_tblComprasAlimento_idProveedor ON tblComprasAlimento(idProveedor) WITH (FILLFACTOR = 80);
CREATE INDEX IX_tblComprasAlimento_fechaCompra ON tblComprasAlimento(fechaCompra) WITH (FILLFACTOR = 80);
GO

--> Crear tabla Detalle de compras de alimento
CREATE TABLE tblDetalleComprasAlimento
(
	id INT IDENTITY(1,1) NOT NULL,
	idCompra INT NOT NULL,
	idAlimento INT NOT NULL,
	cantidad DECIMAL(10,2) NOT NULL,
	unidadMedida NVARCHAR(20) NOT NULL,
	costoUnitario DECIMAL(10,2) NOT NULL,

	CONSTRAINT PK_tblDetalleComprasAlimento PRIMARY KEY (id),

	CONSTRAINT FK_tblDetalleComprasAlimento_tblComprasAlimento FOREIGN KEY (idCompra) REFERENCES tblComprasAlimento(id),
	CONSTRAINT FK_tblDetalleComprasAlimento_tblAlimentos FOREIGN KEY (idAlimento) REFERENCES tblAlimentos(id),

	CONSTRAINT CK_tblDetalleComprasAlimento_cantidad CHECK(cantidad > 0),
	CONSTRAINT CK_tblDetalleComprasAlimento_costoUnitario CHECK(costoUnitario >= 0),
	CONSTRAINT CK_tblDetalleComprasAlimento_unidadMedida CHECK(unidadMedida IN ('Libras', 'Kilogramos'))
);
GO

CREATE INDEX IX_tblDetalleComprasAlimento_idCompra ON tblDetalleComprasAlimento(idCompra) WITH (FILLFACTOR = 80);
CREATE INDEX IX_tblDetalleComprasAlimento_idAlimento ON tblDetalleComprasAlimento(idAlimento) WITH (FILLFACTOR = 80);
GO

--> Crear tabla Clientes
CREATE TABLE tblClientes
(
	id INT IDENTITY(1,1) NOT NULL,
	nombreCliente NVARCHAR(100) NOT NULL,
	telefono NVARCHAR(25) NULL,
	direccion NVARCHAR(200) NULL,
	tipoCliente NVARCHAR(30) NOT NULL,
	activo BIT NOT NULL DEFAULT 1,

	CONSTRAINT PK_tblClientes PRIMARY KEY (id),

	CONSTRAINT CK_tblClientes_tipoCliente CHECK(tipoCliente IN('Cliente específico', 'Pulpería', 'Negocio', 'Otro'))
);
GO

CREATE INDEX IX_tblClientes_nombreCliente ON tblClientes(nombreCliente) WITH (FILLFACTOR = 80);
CREATE INDEX IX_tblClientes_tipoCliente ON tblClientes(tipoCliente) WITH (FILLFACTOR = 80);
GO

--> Crear tabla Tarifas del pollo
CREATE TABLE tblTarifasPollo
(
	id INT IDENTITY(1,1) NOT NULL,
	precioPorLibra DECIMAL(10,2) NOT NULL,
	fechaInicio DATE NOT NULL,
	fechaFin DATE NULL,
	activo BIT NOT NULL DEFAULT 1,

	CONSTRAINT PK_tblTarifasPollo PRIMARY KEY (id),

	CONSTRAINT CK_tblTarifasPollo_precio CHECK(precioPorLibra > 0),
	CONSTRAINT CK_tblTarifasPollo_fechas CHECK(fechaFin IS NULL OR fechaFin >= fechaInicio)
);
GO

CREATE INDEX IX_tblTarifasPollo_fechaInicio ON tblTarifasPollo(fechaInicio) WITH (FILLFACTOR = 80);
CREATE INDEX IX_tblTarifasPollo_activo ON tblTarifasPollo(activo) WITH (FILLFACTOR = 80);
GO

--> Crear tabla Ventas
CREATE TABLE tblVentas
(
	id INT IDENTITY(1,1) NOT NULL,
	numeroVenta NVARCHAR(50) NOT NULL,
	idCliente INT NOT NULL,
	fechaVenta DATETIME NOT NULL DEFAULT GETDATE(),
	observacion NVARCHAR(500) NULL,
	estado NVARCHAR(15) NOT NULL DEFAULT 'Registrada',

	CONSTRAINT PK_tblVentas PRIMARY KEY (id),

	CONSTRAINT UQ_tblVentas_numeroVenta UNIQUE (numeroVenta),

	CONSTRAINT FK_tblVentas_tblClientes FOREIGN KEY (idCliente) REFERENCES tblClientes(id),
	CONSTRAINT CK_tblVentas_estado CHECK(estado IN ('Registrada', 'Anulada'))
);
GO

CREATE INDEX IX_tblVentas_idCliente ON tblVentas(idCliente) WITH (FILLFACTOR = 80);
CREATE INDEX IX_tblVentas_fechaVenta ON tblVentas(fechaVenta) WITH (FILLFACTOR = 80);
GO

--> Crear tabla Detalle de ventas
CREATE TABLE tblDetalleVentas
(
	id INT IDENTITY(1,1) NOT NULL,
	idVenta INT NOT NULL,
	idLote INT NOT NULL,
	cantidadPollos INT NOT NULL DEFAULT 1,
	pesoLibras DECIMAL(10,2) NOT NULL,
	precioPorLibra DECIMAL(10,2) NOT NULL,

	CONSTRAINT PK_tblDetalleVentas PRIMARY KEY (id),

	CONSTRAINT FK_tblDetalleVentas_tblVentas FOREIGN KEY (idVenta) REFERENCES tblVentas(id),
	CONSTRAINT FK_tblDetalleVentas_tblLotes FOREIGN KEY (idLote) REFERENCES tblLotes(id),

	CONSTRAINT CK_tblDetalleVentas_cantidadPollos CHECK(cantidadPollos > 0),
	CONSTRAINT CK_tblDetalleVentas_pesoLibras CHECK(pesoLibras > 0),
	CONSTRAINT CK_tblDetalleVentas_precioPorLibra CHECK(precioPorLibra > 0)
);
GO

CREATE INDEX IX_tblDetalleVentas_idVenta ON tblDetalleVentas(idVenta) WITH (FILLFACTOR = 80);
CREATE INDEX IX_tblDetalleVentas_idLote ON tblDetalleVentas(idLote) WITH (FILLFACTOR = 80);
GO

--> Crear tabla LogErrores 
CREATE TABLE tblLogErrores
(
	idLog INT IDENTITY(1,1) NOT NULL,
	mensajeError NVARCHAR(MAX) NOT NULL,
	numeroError INT NULL,
	procedimiento NVARCHAR(150) NULL,
	lineaError INT NULL,
	usuarioApp NVARCHAR(25) NOT NULL,
	fechaError DATETIME NOT NULL DEFAULT GETDATE(),

	CONSTRAINT PK_tblLogErrores PRIMARY KEY (idLog)
);
GO