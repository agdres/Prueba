-- Estructura base de datos
	
	CREATE DATABASE Double_V
	USE Double_V

	CREATE TABLE Personas(
	Identificador INT IDENTITY (1,1) PRIMARY KEY,
	Nombres VARCHAR(100),
	Apellidos VARCHAR(100),
	Numero_Identificacion VARCHAR(20),
	Email VARCHAR(250),
	Tipo_Identificacion VARCHAR(50),
	Fecha_Creacion DATE,
	Identificacion_Completa VARCHAR(80),
	Nombres_Completos VARCHAR(210)
	)

	CREATE TABLE Usuarios(
	Identificador INT IDENTITY (1,1) PRIMARY KEY,
	ID_Persona INT,
	Usuario VARCHAR(250) UNIQUE,
	Pass VARCHAR(250),
	Fecha_Creacion DATE
	FOREIGN KEY (ID_Persona) REFERENCES Personas (Identificador)
	)

-- Procedimiento Almacenado
CREATE PROC spGetPersonas
AS
	BEGIN TRANSACTION
		BEGIN TRY
			SELECT 
				P.Nombres,
				P.Apellidos,
				P.Tipo_Identificacion,
				P.Numero_Identificacion,
				P.Email,
				P.Fecha_Creacion,
				U.Usuario,
				U.Pass
			FROM Personas AS P
			JOIN Usuarios AS U ON P.Identificador = U.ID_Persona 
		COMMIT TRANSACTION
		END TRY
	BEGIN CATCH
	ROLLBACK TRAN

END CATCH


